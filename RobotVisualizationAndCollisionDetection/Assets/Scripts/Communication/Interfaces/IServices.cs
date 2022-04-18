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
        void RegisterService<T, U>(Action<T> callback, string serviceName) where T : Message where U : Message;
        void SendServiceMsg<T>(Action<Message> callback, string serviceName, T serviceRequest) where T : Message;
    }
}