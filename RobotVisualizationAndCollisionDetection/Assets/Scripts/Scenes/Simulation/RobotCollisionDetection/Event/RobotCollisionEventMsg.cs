
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

public class RobotCollisionEventMsg : Message
{
    public RobotCollisionEventMsg(){}
    public RobotCollisionEventMsg(string jointName, float jointPosition, int sec)
    {
        this.jointName = jointName;
        this.jointPosition = jointPosition;
        this.sec = sec;
    }

    public RobotCollisionEventMsg(string jointName, float jointPosition, uint nanosec)
    {
        this.jointName = jointName;
        this.jointPosition = jointPosition;
        this.nanosec = nanosec;
    }

    public string jointName;
    public float jointPosition;
    public int sec;
    public uint nanosec;

}