
using RosMessageTypes.Sensor;

namespace CollisionDetection.Robot.Startup
{
    public class ObjectCoordinates
    {
        public ObjectCoordinates() { }
        public ObjectCoordinates(float x, float y, float z, float rotationX, float rotationY, float rotationZ)
        {
            X = x;
            Y = y;
            Z = z;
            RotationX = rotationX;
            RotationY = rotationY;
            RotationZ = rotationZ;
        }

        public ObjectCoordinates(JointStateMsg coordinates)
        {
            X = (float)coordinates.position[0];
            Y = (float)coordinates.position[1];
            Z = (float)coordinates.position[2];
            RotationX = (float)coordinates.position[3];
            RotationY = (float)coordinates.position[4];
            RotationZ = (float)coordinates.position[5];
        }
        
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float RotationX { get; set; }
        public float RotationY { get; set; }
        public float RotationZ { get; set; }

    }
}
