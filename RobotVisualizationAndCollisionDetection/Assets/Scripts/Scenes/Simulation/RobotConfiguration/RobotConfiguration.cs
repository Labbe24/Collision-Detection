using CollisionDetection.Communication;
using CollisionDetection.Robot.Startup;
using Newtonsoft.Json;
using RosMessageTypes.CollisionDetection;
using RosMessageTypes.Sensor;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CollisionDetection.Robot.Configuration
{
    public class RobotConfiguration
    {
        public string path;
        public string trajectoryRequestPath;
        public string subscribeTopic;
        public string serviceName;
        public string urdfFileName = "model.urdf";
        public string mappingFileName = "mapping.json";
        public string trajectoryRequestName;
        public TrajectoryRequest trajectoryRequest;
        public ISubscriber subscriber = new RosSubscriber();
        public RosService service = new RosService();

        /// <summary>
        /// Gets path to file
        /// </summary>
        /// <param name="file">Name of file to get path to</param>
        /// <returns>Path to file</returns>
        private string GetPath(string file)
        {
            return Path.GetFullPath(Path.Combine(path, file));
        }

        /// <summary>
        /// Gets path to urdf file
        /// </summary>
        /// <returns></returns>
        public string GetUrdfPath()
        {
            return GetPath(urdfFileName);
        }

        /// <summary>
        /// Gets path to mapping file
        /// </summary>
        /// <returns></returns>
        public string GetMappingPath()
        {
            return GetPath(mappingFileName);
        }

        /// <summary>
        /// Gets request for points to generate trajectory for
        /// </summary>
        /// <returns><Request/returns>
        public GenerateTrajectoryRequest GetTrajectoryRequest()
        {
            GenerateTrajectoryRequest generatedTrajectoryRequest = new GenerateTrajectoryRequest();
            trajectoryRequest = JsonConvert.DeserializeObject<TrajectoryRequest>(File.ReadAllText(Path.GetFullPath(Path.Combine(trajectoryRequestPath, trajectoryRequestName))));
            if (trajectoryRequest.jointStateMsg.Count >= 2)
            {
                generatedTrajectoryRequest.move_group = trajectoryRequest.moveGroup;
                generatedTrajectoryRequest.states = trajectoryRequest.jointStateMsg.ToArray();
            }

            return generatedTrajectoryRequest;
        }

        /// <summary>
        /// Gets start coordinates of robot
        /// </summary>
        /// <returns>Vector</returns>
        public Vector3 GetRobotStartCoordinates()
        {
            return new Vector3(trajectoryRequest.baseCoordinates.X, trajectoryRequest.baseCoordinates.Y, trajectoryRequest.baseCoordinates.Z);
        }

        /// <summary>
        /// Gets start rotation of robot
        /// </summary>
        /// <returns>Vector</returns>
        public Vector3 GetRobotStartRotation()
        {
            return new Vector3(trajectoryRequest.baseCoordinates.RotationX, trajectoryRequest.baseCoordinates.RotationY, trajectoryRequest.baseCoordinates.RotationZ);
        }
    }
}