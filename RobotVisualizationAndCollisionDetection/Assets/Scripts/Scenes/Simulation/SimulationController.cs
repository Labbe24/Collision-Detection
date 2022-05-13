using System.Collections;
using System.Collections.Generic;
using CollisionDetection.Robot.Control;
using UnityEngine;
using System.Linq;
public class SimulationController : MonoBehaviour
{
    public List<RobotController> robots;

    public RobotState getRobotCollisionState(int i){
        // return robots.Aggregate(
        //     new List<CollisionEvent>(),
        //     (acc,item) =>  acc.Concat(item.events).ToList()
        // );
        if(robots.First() == null) Debug.Log("robots.First() == null");
        if(robots.First().collision == null) Debug.Log("robots.First().collision == null");
        return robots.First().collision.robotStates[i];
    }
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
                StartCoroutine(robot.StartTrajectoryExecution());
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
