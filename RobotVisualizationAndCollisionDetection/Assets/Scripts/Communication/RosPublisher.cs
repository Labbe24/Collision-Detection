using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

/// <summary>
/// 
/// </summary>
public class RosPublisher : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "collision_events";

    // The game object
    public GameObject robot;
    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    void Start()
    {
        // start the ROS connection
        // ros = ROSConnection.GetOrCreateInstance();
        // ros.RegisterPublisher<CollisionMsg>(topicName);
    }

    public void Publish(CollisionEvent collisionEvent)
    {
        Debug.Log("CollisionMsg: " + collisionEvent.jointName
        + " " + collisionEvent.jointPosition);
        // ros.Publish(topicName, collisionMsg);
    }

    // public void PublishCollisions(CollisionMsg[] collisionMsgs)
    // {
    //     ros.Publish(topicName, collisionMsgs)
    // }
}