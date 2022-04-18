# Robot visualization and collision detection in Unity
This project was developed in Unity Editor version 2020.3.11f1.

The following repositories were of great use.
- URDF Importer: https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/main/tutorials/urdf_importer/urdf_tutorial.md.
- ROS-Unity Integration: https://github.com/Unity-Technologies/Unity-Robotics-Hub/tree/main/tutorials/ros_unity_integration.

## URDF
A URDF format of the robot and the URDF Importer package was used to import the robot into a unity scene.

## ROS-Unity Communication
The communication between Unity and ROS was established by utilising the tools from the ROS TCP Connector package.

### ROS Messages
With the MessageGeneration plugin, availbale from the ROS TCP Connector package, C# classes from .msg files were generated.

### Subscriber
A Subscriber in unity recieves messages with information on how the joints of the robot are positioned.

## Robot Control
The robot is controlled by using unity's 3D Physics engine and building an articulation corresponding to the robot. The articulation is automatically build, when importing the robot defined in the URDF format, by the URDF Importer. 

