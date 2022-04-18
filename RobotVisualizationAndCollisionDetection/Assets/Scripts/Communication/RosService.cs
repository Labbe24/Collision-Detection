using Unity.Robotics.ROSTCPConnector;
using CollisionDetection.Communication;
using System;
using RosMessageTypes.CollisionDetection;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

public class RosService : IService
{
    public void RegisterService<T, U>(Action<T> callback, string serviceName) where T : Message where U : Message
    {
        ROSConnection.GetOrCreateInstance().RegisterRosService<U, T>(serviceName);
    }

    public void SendServiceMsg<T>(Action<Message> callback, string serviceName, T serviceRequest) where T : Message
    {
        ROSConnection.GetOrCreateInstance().SendServiceMessage<Message>(serviceName, serviceRequest, callback);
    }
}
