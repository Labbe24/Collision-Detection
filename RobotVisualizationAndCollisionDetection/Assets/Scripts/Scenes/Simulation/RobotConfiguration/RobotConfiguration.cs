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

        private string GetPath(string file)
        {
            return Path.GetFullPath(Path.Combine(path, file));
        }
        public string GetUrdfPath()
        {
            return GetPath(urdfFileName);
        }
        public string GetMappingPath()
        {
            return GetPath(mappingFileName);
        }
        public GenerateTrajectoryRequest GetTrajectoryRequest()
        {
            GenerateTrajectoryRequest generatedTrajectoryRequest = new GenerateTrajectoryRequest();
            trajectoryRequest = JsonConvert.DeserializeObject<TrajectoryRequest>(File.ReadAllText(Path.GetFullPath(Path.Combine(trajectoryRequestPath, trajectoryRequestName))));
            if (trajectoryRequest.jointStateMsg.Count == 2)
            {
                generatedTrajectoryRequest.move_group = trajectoryRequest.moveGroup;
                generatedTrajectoryRequest.start_state = trajectoryRequest.jointStateMsg[0];
                generatedTrajectoryRequest.end_state = trajectoryRequest.jointStateMsg[1];
            }

            return generatedTrajectoryRequest;
        }
        
        public Vector3 GetRobotStartCoordinates()
        {
            return new Vector3(trajectoryRequest.baseCoordinates.X, trajectoryRequest.baseCoordinates.Y, trajectoryRequest.baseCoordinates.Z);
        }
        public Vector3 GetRobotStartRotation()
        {
            return new Vector3(trajectoryRequest.baseCoordinates.RotationX, trajectoryRequest.baseCoordinates.RotationY, trajectoryRequest.baseCoordinates.RotationZ);
        }

        public ISubscriber subscriber = new RosSubscriber();
        public RosService service = new RosService();
    }
}