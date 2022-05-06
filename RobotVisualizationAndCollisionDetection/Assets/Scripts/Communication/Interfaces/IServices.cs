using System;
using RosJointTrajectory = RosMessageTypes.Trajectory.JointTrajectoryMsg;
using RosJointTrajectoryPoint = RosMessageTypes.Trajectory.JointTrajectoryPointMsg;
using RosJointStateMsg = RosMessageTypes.Sensor.JointStateMsg;
using System.Collections.Generic;
using RosMessageTypes.CollisionDetection;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace CollisionDetection.Communication
{
    public interface IService
    {
        void RegisterService<RequestType, ResponseType>(string serviceName) where RequestType : Message where ResponseType : Message;
        void SendServiceMsg<RequestType, ResponseType>(Action<ResponseType> callback, string serviceName, RequestType serviceRequest) where RequestType : Message where ResponseType : Message,new();
    }
}