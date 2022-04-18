using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollisionDetection.Communication;
using CollisionDetection.Robot.Model;
using CollisionDetection.Robot.Control;
using CollisionDetection.Robot.Configuration;
using RosJointTrajectory = RosMessageTypes.Trajectory.JointTrajectoryMsg;
using RosJointTrajectoryPoint = RosMessageTypes.Trajectory.JointTrajectoryPointMsg;
using Newtonsoft.Json;
using System.IO;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;
using RosMessageTypes.CollisionDetection;

namespace CollisionDetection.Robot.Model
{
    public static class RobotFactory
    {
        public static GameObject CreateRobot(Transform transform, RobotConfiguration configuration)
        {
            RobotMsgMapper robotMsgMapper = DeserializeRobotMsgMapper(configuration.GetMappingPath());

            GameObject robot = UrdfImporter.LoadUrdf(configuration.GetUrdfPath(), transform, robotMsgMapper.ImmovableLinkName);
            robot.AddComponent<Rigidbody>();
            RobotController controller = robot.GetComponent<RobotController>();

            // Service - New
            configuration.service.RegisterService<GenerateTrajectoryResponse, RosJointTrajectory>(controller.ROSServiceCallback, configuration.serviceName);
            configuration.service.SendServiceMsg<GenerateTrajectoryRequest>(controller.ROSServiceCallback, configuration.serviceName, configuration.GetTrajectoryRequest());

            // Subscribe - Old
            configuration.subscriber.Subscribe<RosJointTrajectory>(controller.ReceivedTrajectory, configuration.subscribeTopic);

            controller.SetRobotMsgMapper(robotMsgMapper);
            controller.SetRobotStartPosition(configuration.GetRobotStartCoordinates());

            return robot;
        }

        private static RobotMsgMapper DeserializeRobotMsgMapper(string path)
        {
            return JsonConvert.DeserializeObject<RobotMsgMapper>(File.ReadAllText(path));
        }
    }
}
