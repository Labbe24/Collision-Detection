using System.Collections;
using System.Collections.Generic;
using CollisionDetection.Robot.Control;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    public List<RobotController> robots;

    void Start()
    {
    }

    void Update()
    {
        // used for testing
        if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Starting simulation.");
            TryStartSimulation();
        }
    }
    public void TryStartSimulation()
    {
        if (AllReady())
        {
            foreach (var robot in robots)
            {
                robot.StartTrajectoryExecution();
            }
        }
        else
        {
            Debug.Log("Simulation not started. Not all robots have recieved trajectories.");
        }    
    }
    private bool AllReady()
    {
        foreach (var robot in robots)
        {
            if (robot.Trajectory == null)
            {
                return false;
            }
        }
        return true;
    }
}
