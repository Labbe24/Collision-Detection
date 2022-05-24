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
        public RobotMsgMapper robotMsgMapper = new RobotMsgMapper();
        public Vector3 startPosition;
        public Vector3 startRotation;
        public RobotTrajectoryPoint LastCommand { get; private set; }
        private double deltaTime;

        public void SetRobotMsgMapper(RobotMsgMapper msgMapper)
        {
            robotMsgMapper = msgMapper;
        }
        public void SetRobotStartPosition(Vector3 vector)
        {
            startPosition = vector;
        }
        public void SetRobotStartRotation(Vector3 vector)
        {
            startRotation = vector;
        }

        public void ROSServiceCallback(GenerateTrajectoryResponse jointTrajectory)
        {
            ReceivedTrajectory(jointTrajectory.res);
            Debug.Log("callback for " + Trajectory.joint_names[0]);
        }
        public void ReceivedTrajectory(RosJointTrajectory trajectoryMsg)
        {
            Trajectory = trajectoryMsg;
        }


        void Start()
        {
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
                    joint.TeleportRoot(startPosition, Quaternion.Euler(startRotation.x, startRotation.y, startRotation.z));
                }
            }

            Assert.IsNotNull(_articulationChain);
        }
        public IEnumerator StartTrajectoryExecution()
        {
            StartTimer();

            for (int i = 0; i < Trajectory.points.Length; i++)
            {
                var next_msg_timestamp = (float)Trajectory.points[i].time_from_start.sec + (float)Trajectory.points[i].time_from_start.nanosec / (Math.Pow(10f, 9f));
                if (next_msg_timestamp > GetElapsedTime())
                {
                    yield return new WaitUntil(() => next_msg_timestamp <= GetElapsedTime());
                }
                UpdateRobotPosition(Trajectory.points[i], Trajectory.joint_names);
                LastCommand = new RobotTrajectoryPoint() { joint_names = Trajectory.joint_names, point = Trajectory.points[i] };
            }

        }
        void Update()
        {
        }

        public void UpdateRobotPosition(RosJointTrajectoryPoint point, string[] names)
        {
            for (int i = 0; i < point.positions.Length; i++)
            {
                UpdateJoint((float)(point.positions[i] * 180 / System.Math.PI), (float)(point.velocities[i] * 180 / System.Math.PI), names[i]);
            }
        }

        /// <summary>
        /// Moves the joint corresponding to the selected index in the articulation chain
        /// </summary>
        /// <param name="selectedIndex">Index of the link selected in the Articulation Chain</param>
        public void UpdateJoint(float newTarget, float newVelocityTarget, string name)
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
                currentDrive.targetVelocity = newVelocityTarget;
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

