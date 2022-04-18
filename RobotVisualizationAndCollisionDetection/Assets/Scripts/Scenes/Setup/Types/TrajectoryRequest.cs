using System.Collections.Generic;
using RosMessageTypes.Sensor;

namespace CollisionDetection.Robot.Startup
{
    public class TrajectoryRequest
    {
        public ObjectCoordinates baseCoordinates { get; set; }
        public List<JointStateMsg> jointStateMsg { get; set; }

        public TrajectoryRequest() {}
        public TrajectoryRequest(ObjectCoordinates objectCoordinates, List<JointStateMsg> jointStateMsgs) {
            this.baseCoordinates = objectCoordinates;
            this.jointStateMsg = jointStateMsgs;
        }
    }
}