using System;

namespace CollisionDetection.Communication{
    public interface ISubscriber{
        void Subscribe<T>(Action<T> callback, string topic) where T : Unity.Robotics.ROSTCPConnector.MessageGeneration.Message;
    }
}