using System;

public class CollisionEvent : IComparable<CollisionEvent>
{
    public string robotName;
    public string jointName;
    public int jointIndex;
    public double jointPosition;
    public double time;

    // implementation of how the Sort method should compare CollisionEvents in a List<CollisionEvent>
    public int CompareTo(CollisionEvent other)
    {
        return this.jointIndex.CompareTo(other.jointIndex);
    }
}