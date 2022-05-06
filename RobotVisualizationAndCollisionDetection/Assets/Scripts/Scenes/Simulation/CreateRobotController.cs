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
        StartCoroutine(createRobot(configs,simulationController));
    }

    IEnumerator createRobot(RobotConfiguration configs,SimulationController simulationController){
        // if(configs.GetTrajectoryRequest().move_group=="iiwa7_arm"){
        //     yield return new WaitForSeconds(1);
        // }
        simulationController.robots.Add(RobotFactory.CreateRobot(transform, configs).GetComponent<RobotController>());
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
