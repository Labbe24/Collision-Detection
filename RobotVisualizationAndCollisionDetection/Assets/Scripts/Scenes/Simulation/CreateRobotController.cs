using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollisionDetection.Robot.Configuration;
using CollisionDetection.Robot.Model;
using CollisionDetection.Robot.Control;

public class CreateRobotController : MonoBehaviour
{
    public GameObject simulator;
    public int robotStoreIndex;
    
    /// <summary>
    /// Creates a robot, gets it's RobotController and sets it in the SimulationController.
    /// </summary>
    void Start()
    {
        var configs = RobotConfigStore.configurations[robotStoreIndex];
        var simulationController = simulator.GetComponent<SimulationController>();
        simulationController.robots.Add(RobotFactory.CreateRobot(transform, configs).GetComponent<RobotController>());
    }

    void Update()
    {

    }
}
