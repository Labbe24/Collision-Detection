using Unity.Robotics.ROSTCPConnector;
using CollisionDetection.Communication;
using System;
using RosMessageTypes.CollisionDetection;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

public class RosService : IService
{
    public void RegisterService<RequestType, ResponseType>(string serviceName) where RequestType : Message where ResponseType : Message
    {
        ROSConnection.GetOrCreateInstance().RegisterRosService<RequestType, ResponseType>(serviceName);
    }

    public void SendServiceMsg<RequestType,ResponseType>(Action<ResponseType> callback, string serviceName, RequestType serviceRequest) where RequestType : Message where ResponseType : Message,new()
    {
        ROSConnection.GetOrCreateInstance().SendServiceMessage<ResponseType>(serviceName, serviceRequest, callback);
    }
}
