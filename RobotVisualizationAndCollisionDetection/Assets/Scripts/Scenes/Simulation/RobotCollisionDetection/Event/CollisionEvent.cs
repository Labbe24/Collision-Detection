using System;
using System.Collections.Generic;
using RosJointTrajectoryPoint = RosMessageTypes.Trajectory.JointTrajectoryPointMsg;
public class RobotState
{
    public string name;
    public List<string> jointNames = new List<string>();
    public List<string> jointPositions = new List<string>();
}

public class RobotTrajectoryPoint
{
    public string[] joint_names;
    public RosJointTrajectoryPoint point;

}

public class CollisionEvent
{
    public RobotState robotState;
    public double time;
    public string collidedJoint;
}
