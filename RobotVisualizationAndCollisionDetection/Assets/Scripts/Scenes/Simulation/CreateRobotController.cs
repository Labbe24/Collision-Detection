using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CollisionDetection.Robot.Configuration;
using CollisionDetection.Robot.Model;
using CollisionDetection.Robot.Control;

public class CreateRobotController : MonoBehaviour
{
    public GameObject simulator;
    public int robot_store_index;
    // Start is called before the first frame update
    void Start()
    {
        var configs = RobotConfigStore.configurations[robot_store_index];

        var simulationController = simulator.GetComponent<SimulationController>();
        simulationController.robots.Add(RobotFactory.CreateRobot(transform, configs).GetComponent<RobotController>());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
