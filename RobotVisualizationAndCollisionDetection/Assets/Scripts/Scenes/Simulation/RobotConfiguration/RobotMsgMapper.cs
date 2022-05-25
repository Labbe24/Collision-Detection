
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CollisionDetection.Robot.Control
{
    public class RobotMsgMapper
    {
        public string ImmovableLinkName { get; set; }
        public List<string> Joints { get; set; }

        /// <summary>
        /// Gets index of joint
        /// </summary>
        /// <param name="jointName">Name of joint</param>
        /// <returns>Index of specified joint</returns>
        public int GetJointIndex(string jointName)
        {
            return Joints.IndexOf(jointName);
        }

        /// <summary>
        /// Checks for joint by name
        /// </summary>
        /// <param name="jointName">Name of joint</param>
        /// <returns>True if joint is present. Otherwise false</returns>
        public bool RobotContainsJoint(string jointName)
        {
            return Joints.Contains(jointName);
        }
    }
}