
using RosMessageTypes.Sensor;

namespace CollisionDetection.Robot.Startup
{
    public class ObjectCoordinates
    {
        public ObjectCoordinates() { }
        public ObjectCoordinates(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public ObjectCoordinates(JointStateMsg coordinates)
        {
            X = (float)coordinates.position[0];
            Y = (float)coordinates.position[1];
            Z = (float)coordinates.position[2];
        }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
