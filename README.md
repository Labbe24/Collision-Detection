# Introduction 
This repository contains the development of a platform for simulation of robots and collision detection of colloaborating robots. 

# Why
Simulation is a powerfoll tool in the context of robotics. It is the aim of this project to build a platform capable of simulating different kinds of botos and perform collision detection between collaborating robots.

# How
Two repositories are being used to accomplish this goal. 
1. This repository
2. https://github.com/Labbe24/Collision-Detection-Ros

This repository is responsible for simulating & visualizing robotic arms. As well as detecting collisions between these. The Collision-Detection-Ros repository is used for generating trajectories with use of the ROS2 framework. These trajectories are used in the application of this repository.


# Run
## Unity application
To run the simulation application of this repository, you must have the unity editor v 2020.3.11f1 installed, along with unity hub.

To open the application in the editor, open Unity hub, click "open" and navigate to the RobotVisualizationAndCollisionDetection directory of this repository, and click "Add project".

Then click the play button in the Unity editor to run the application.
Note that the ROS environment at https://github.com/Labbe24/Collision-Detection-Ros should be running, to start any simulation.

## Selecting robot models.

The robot setup interface (first screen of the unity application) has four file selectors, two for each robot.
### Model
The top most for each is the robot model, which is a model of the robot in urdf-format. As a demo, the Models/ur5e can be chosen for robot1 and Models/kuka_iiwa_7 for robot2, along with the TrajectoryPositions/ folder to supply both robots with trajectories.

### Trajectory
The bottom selector for each robots needs to select the file containing a description of the trajectory the robot should perform. This needs to contain the following in json-format:
```
{
	"baseCoordinates":{},
	"moveGroup": ,
	"jointStateMsg":[]
}
```
Where baseCoordinates describe position of robot in the virtual environment.
MoveGroup is the name of the move group in the Ros environment to generate the trajectory for.
jointStateMsg is a list of joint states the robot should visit in this trajectory.

For an example see 
TrajectoryPositions/trajectoryRequestRobot1.json

## ROS environment
See Readme at https://github.com/Labbe24/Collision-Detection-Ros

# Demo
Start Unity application and ros environment.
Use following files in the setup:

Select model for robot 1 : Models/ur5e
Select Trajectory Request for robot 1 : TrajectoryPositions
Select model for robot 2 : Models/kuka_iiwa_7
Select Trajectory Request for robot 2 : TrajectoryPositions

Press "Start".