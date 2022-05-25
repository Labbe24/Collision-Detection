using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionDetection.Robot.Control
{
    public class RobotJointControl : MonoBehaviour
    {
        RobotController controller;
        public RotationDirection direction;
        public ControlType controltype;
        public float speed;
        public float torque;
        public float acceleration;
        public ArticulationBody joint;

        void Start()
        {
            direction = 0;
            controller = (RobotController)this.GetComponentInParent(typeof(RobotController));
            joint = this.GetComponent<ArticulationBody>();
            controller.UpdateControlType(this);
            speed = controller.speed;
            torque = controller.torque;
            acceleration = controller.acceleration;
        }

        /// <summary>
        /// 
        /// </summary>
        void FixedUpdate()
        {
            speed = controller.speed;
            torque = controller.torque;
            acceleration = controller.acceleration;

            if (joint.jointType != ArticulationJointType.FixedJoint)
            {
                if (controltype == ControlType.PositionControl)
                {
                    ArticulationDrive currentDrive = joint.xDrive;
                    float newTargetDelta = (int)direction * Time.fixedDeltaTime * speed;

                    if (joint.jointType == ArticulationJointType.RevoluteJoint)
                    {
                        if (joint.twistLock == ArticulationDofLock.LimitedMotion)
                        {
                            if (newTargetDelta + currentDrive.target > currentDrive.upperLimit)
                            {
                                currentDrive.target = currentDrive.upperLimit;
                            }
                            else if (newTargetDelta + currentDrive.target < currentDrive.lowerLimit)
                            {
                                currentDrive.target = currentDrive.lowerLimit;
                            }
                            else
                            {
                                currentDrive.target += newTargetDelta;
                            }
                        }
                        else
                        {
                            currentDrive.target += newTargetDelta;

                        }
                    }

                    else if (joint.jointType == ArticulationJointType.PrismaticJoint)
                    {
                        if (joint.linearLockX == ArticulationDofLock.LimitedMotion)
                        {
                            if (newTargetDelta + currentDrive.target > currentDrive.upperLimit)
                            {
                                currentDrive.target = currentDrive.upperLimit;
                            }
                            else if (newTargetDelta + currentDrive.target < currentDrive.lowerLimit)
                            {
                                currentDrive.target = currentDrive.lowerLimit;
                            }
                            else
                            {
                                currentDrive.target += newTargetDelta;
                            }
                        }
                        else
                        {
                            currentDrive.target += newTargetDelta;

                        }
                    }

                    joint.xDrive = currentDrive;

                }
            }
        }
    }
}
