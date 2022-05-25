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
        /// <summary>
        /// Initialises ros service and creates a robot game object
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="configuration">Configuration containing multiple relevant parameters</param>
        /// <returns>Robot</returns>
        public static GameObject CreateRobot(Transform transform, RobotConfiguration configuration)
        {
            RobotMsgMapper robotMsgMapper = DeserializeRobotMsgMapper(configuration.GetMappingPath());
            GameObject robot = UrdfImporter.LoadUrdf(configuration.GetUrdfPath(), transform, robotMsgMapper.ImmovableLinkName);
            RobotController controller = robot.GetComponent<RobotController>();

            configuration.service.RegisterService<GenerateTrajectoryRequest, GenerateTrajectoryResponse>(configuration.serviceName);
            configuration.service.SendServiceMsg<GenerateTrajectoryRequest,GenerateTrajectoryResponse>(controller.ROSServiceCallback, configuration.serviceName, configuration.GetTrajectoryRequest());

            controller.SetRobotMsgMapper(robotMsgMapper);
            controller.SetRobotStartPosition(configuration.GetRobotStartCoordinates());
            controller.SetRobotStartRotation(configuration.GetRobotStartRotation());

            return robot;
        }

        /// <summary>
        /// Creates RobotMsgMapper from file
        /// </summary>
        /// <param name="path">Path to file from which a RobotMsgMapper is created</param>
        /// <returns>RobotMsgMapper</returns>
        private static RobotMsgMapper DeserializeRobotMsgMapper(string path)
        {
            return JsonConvert.DeserializeObject<RobotMsgMapper>(File.ReadAllText(path));
        }
    }
}
