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
            GenerateTrajectoryRequest trajectoryRequest = new GenerateTrajectoryRequest();
            TrajectoryRequest trajectory = JsonConvert.DeserializeObject<TrajectoryRequest>(File.ReadAllText(Path.GetFullPath(Path.Combine(trajectoryRequestPath, trajectoryRequestName))));
            if (trajectory.jointStateMsg.Count == 2)
            {
                trajectoryRequest.move_group = trajectory.moveGroup;
                trajectoryRequest.start_state = trajectory.jointStateMsg[0];
                trajectoryRequest.end_state = trajectory.jointStateMsg[1];
            }

            return trajectoryRequest;
        }
        public Vector3 GetRobotStartCoordinates()
        {
            TrajectoryRequest trajectory = JsonConvert.DeserializeObject<TrajectoryRequest>(File.ReadAllText(Path.GetFullPath(Path.Combine(trajectoryRequestPath, trajectoryRequestName))));
            return new Vector3(trajectory.baseCoordinates.X, trajectory.baseCoordinates.Y, trajectory.baseCoordinates.Z);
        }

        public ISubscriber subscriber = new RosSubscriber();
        public RosService service = new RosService();
    }
}