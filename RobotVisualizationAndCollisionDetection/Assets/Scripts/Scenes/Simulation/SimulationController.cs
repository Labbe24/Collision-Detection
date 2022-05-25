using System.Collections;
using System.Collections.Generic;
using CollisionDetection.Robot.Control;
using UnityEngine;
using System.Linq;
public class SimulationController : MonoBehaviour
{
    public List<RobotController> robots;
    private List<IEnumerator> coroutines;
    private List<RobotTrajectoryPoint> lastCommandsBeforeCollision;
    private List<CollisionEvent> collisionStates;

    /// <summary>
    /// Sets the collision event
    /// </summary>
    /// <param name="robot">Robot that handled the collision</param>
    /// <param name="collision">Containsinfomartion about the collision</param>
    public void SetRobotCollisionState(RobotController robot,CollisionEvent collision)
    {
        collisionStates[robots.IndexOf(robot)] = collision;
    }

    /// <summary>
    /// Gets a collision event
    /// </summary>
    /// <param name="i">What robot to get the collison from</param>
    /// <returns>Collision event from the chosen robot</returns>
    public CollisionEvent GetRobotCollisionState(int i)
    {
        return collisionStates[i];
    }

    /// <summary>
    /// Gets the last executed trajectory before the collision occured
    /// </summary>
    /// <param name="i">What robot to get the last executed trajectory from</param>
    /// <returns>Trajectory from the chosen robot</returns>
    public RobotTrajectoryPoint GetRobotLastState(int i)
    {
        return lastCommandsBeforeCollision[i];
    }

    /// <summary>
    /// Gets the time when the collision occured
    /// </summary>
    /// <returns>Time of collision</returns>
    public double GetColissionTime()
    {
        foreach(var collision in collisionStates)
        {
            if(collision!=null){
                return collision.time;
            }
        }
        return 0.0;
    }

    void Start()
    {
        coroutines = new List<IEnumerator>();
        lastCommandsBeforeCollision = new List<RobotTrajectoryPoint>(){null, null};
        collisionStates = new List<CollisionEvent>(){null, null};
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

    /// <summary>
    /// Starts the simulation if all robots are ready
    /// </summary>
    public void TryStartSimulation()
    {
        if (AllReady())
        {
            foreach (var robot in robots)
            {
                coroutines.Add(robot.StartTrajectoryExecution());
            }
            foreach (var coroutine in coroutines){
                StartCoroutine(coroutine);
            }
        }
        else
        {
            Debug.Log("Simulation not started. Not all robots have recieved trajectories.");
        }    
    }

    /// <summary>
    /// Stops the simulation
    /// </summary>
    public void StopSimulation()
    {
            foreach (var coroutine in coroutines){
                StopCoroutine(coroutine);
            }
            //capture states
            for(int i=0;i<robots.Count;i++){
                lastCommandsBeforeCollision[i]=robots[i].LastCommand;
            }
    }

    /// <summary>
    /// Checks if all robots have recieved trajectories to be simulated
    /// </summary>
    /// <returns>False if one or more robots dont have a trajectory. Otherwise true</returns>
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
