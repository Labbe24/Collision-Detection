using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;
using RosJointTrajectory = RosMessageTypes.Trajectory.JointTrajectoryMsg;
using RosJointTrajectoryPoint = RosMessageTypes.Trajectory.JointTrajectoryPointMsg;
using System.Collections;
using RosMessageTypes.CollisionDetection;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace CollisionDetection.Robot.Control
{
    public enum RotationDirection { None = 0, Positive = 1, Negative = -1 };
    public enum ControlType { PositionControl };
    public class RobotController : MonoBehaviour
    {
        private float elapsedTime;
        public RosJointTrajectory Trajectory { get; set; }
        private ArticulationBody[] _articulationChain;
        // private RosPublisher _rosPublisher;
        private string publisher_tag = "ros_publisher";
        private string content_tag = "collisions_list";
        public ControlType control = ControlType.PositionControl;
        public float stiffness;
        public float damping;
        public float forceLimit;
        public float speed = 5f; // Units: degree/s
        public float torque = 100f; // Units: Nm or N
        public float acceleration = 5f;// Units: m/s^2 / degree/s^2
        public List<CollisionEvent> events;
        public RobotMsgMapper robotMsgMapper = new RobotMsgMapper();
        public Vector3 startPosition;

        private ListViewHandler _listView;
        private double deltaTime;

        public void SetRobotMsgMapper(RobotMsgMapper msgMapper)
        {
            robotMsgMapper = msgMapper;
        }
        public void SetRobotStartPosition(Vector3 vector)
        {
            startPosition = vector;
        }

        public void ROSServiceCallback(GenerateTrajectoryResponse jointTrajectory)
        {
            Trajectory = jointTrajectory.res;
            Debug.Log("callback for "+ Trajectory.joint_names[0]);
            StartTrajectoryExecution();
        }
        public void ReceivedTrajectory(RosJointTrajectory trajectoryMsg)
        {
            Trajectory = trajectoryMsg;
        }


        void Start()
        {
            //_listView = GameObject.FindWithTag(content_tag).GetComponent<ListView>();

            events = new List<CollisionEvent>();

            // Add collision detection controller to components in children with MeshCollider
            foreach (Transform child in GetComponentsInChildren<Transform>())
            {
                if (child.gameObject.GetComponent<MeshCollider>() != null)
                {
                    child.gameObject.AddComponent<RobotCollisionDetectionController>();
                }
            }

            // this.gameObject.AddComponent<ForwardKinematicsRobot>(); PROBABLY NOT USED!
            _articulationChain = this.GetComponentsInChildren<ArticulationBody>();
            int defDyanmicVal = 10;
            foreach (ArticulationBody joint in _articulationChain)
            {
                joint.gameObject.AddComponent<RobotJointControl>();
                joint.jointFriction = defDyanmicVal;
                joint.angularDamping = defDyanmicVal;
                ArticulationDrive currentDrive = joint.xDrive;
                currentDrive.forceLimit = forceLimit;
                joint.xDrive = currentDrive;

                if (joint.isRoot)
                {
                    joint.TeleportRoot(startPosition, Quaternion.identity);
                }
            }

            Assert.IsNotNull(_articulationChain);
        }
        IEnumerator ExecuteTrajectory()
        {
            StartTimer();

            for (int i = 0; i < Trajectory.points.Length; i++)
            {
                var next_msg_timestamp = (float)Trajectory.points[i].time_from_start.sec + (float)Trajectory.points[i].time_from_start.nanosec / (Math.Pow(10f, 9f));
                yield return new WaitUntil(() => next_msg_timestamp >= elapsedTime);
                UpdateRobotPosition(Trajectory.points[i], Trajectory.joint_names);
            }
        }
        public IEnumerator StartTrajectoryExecution()
        {
            elapsedTime = 0.0f;
            yield return StartCoroutine(ExecuteTrajectory());

            Debug.Log("Trajectory execution finished!");

            // list.ClearList();
            GameObject.FindWithTag(content_tag).GetComponent<ListViewHandler>().AddCollisions(events);
        }
        void Update()
        {
            elapsedTime += Time.deltaTime;

            // used for testing
            if (Input.GetKeyDown(KeyCode.L))
            {
                // list.ClearList();
                GameObject.FindWithTag(content_tag).GetComponent<ListViewHandler>().AddCollisions(events);
            }
        }

        public void UpdateRobotPosition(RosJointTrajectoryPoint point, string[] names)
        {
            for (int i = 0; i < point.positions.Length; i++)
            {
                UpdateJoint((float)(point.positions[i] * 180 / System.Math.PI), names[i]);
            }
        }

        /// <summary>
        /// Moves the joint corresponding to the selected index in the articulation chain
        /// </summary>
        /// <param name="selectedIndex">Index of the link selected in the Articulation Chain</param>
        public void UpdateJoint(float newTarget, string name)
        {
            if (!robotMsgMapper.RobotContainsJoint(name))
            {
                return;
            }
            int selectedIndex = robotMsgMapper.GetJointIndex(name);
            ArticulationDrive currentDrive = _articulationChain[selectedIndex].xDrive;
            currentDrive.forceLimit = forceLimit;

            // Only move joints with one ore more degrees of freedom
            if (_articulationChain[selectedIndex].jointPosition.dofCount >= 1)
            {
                currentDrive.target = newTarget;
                _articulationChain[selectedIndex].xDrive = currentDrive;
            }
        }

        /// <summary>
        /// Sets stifness etc on every joint
        /// </summary>
        /// <param name="joint"></param>
        public void UpdateControlType(RobotJointControl joint)
        {
            joint.controltype = control;
            if (control == ControlType.PositionControl)
            {
                ArticulationDrive drive = joint.joint.xDrive;
                drive.stiffness = stiffness;
                drive.damping = damping;
                joint.joint.xDrive = drive;
            }
        }

        public void StartTimer()
        {
            deltaTime = Time.timeAsDouble;
        }

        public double GetElapsedTime()
        {
            return Time.timeAsDouble - deltaTime;
        }
    }
}

