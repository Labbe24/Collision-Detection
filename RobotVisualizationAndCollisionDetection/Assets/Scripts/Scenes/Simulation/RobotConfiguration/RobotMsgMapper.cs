
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CollisionDetection.Robot.Control
{
    public class RobotMsgMapper
    {
        public string ImmovableLinkName { get; set; }
        public List<string> Joints { get; set; }

        public int GetJointIndex(string jointName)
        {
            return Joints.IndexOf(jointName);
        }
        public bool RobotContainsJoint(string jointName)
        {
            return Joints.Contains(jointName);
        }

        public static explicit operator RobotMsgMapper(JToken v)
        {
            throw new NotImplementedException();
        }
    }
}