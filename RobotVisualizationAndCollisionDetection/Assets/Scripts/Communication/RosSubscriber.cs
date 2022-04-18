using Unity.Robotics.ROSTCPConnector;
using CollisionDetection.Communication;
using System.IO;
using System;

public class RosSubscriber : ISubscriber
{
    public void Subscribe<T>(Action<T> callback, string topic) where T : Unity.Robotics.ROSTCPConnector.MessageGeneration.Message{
        ROSConnection.GetOrCreateInstance().Subscribe<T>(topic, callback);
    }
}
