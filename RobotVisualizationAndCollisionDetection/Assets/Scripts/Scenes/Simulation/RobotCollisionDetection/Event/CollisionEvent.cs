using System;
using System.Collections.Generic;

public class RobotState
{
    public string name;
    public List<string> jointNames = new List<string>();
    public List<string> jointPositions = new List<string>();

}

public class CollisionEvent // : IComparable<CollisionEvent>
{
    public List<RobotState> robotStates = new List<RobotState>();
    public double time;

    // implementation of how the Sort method should compare CollisionEvents in a List<CollisionEvent>
    // public int CompareTo(CollisionEvent other)
    // {
        // return this.jointIndex.CompareTo(other.jointIndex);
    // }
}
