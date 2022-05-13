using System.Collections;
using System.Collections.Generic;
using CollisionDetection.Robot.Control;
using UnityEngine;
using System.Linq;
public class SimulationController : MonoBehaviour
{
    public List<RobotController> robots;
    private List<IEnumerator> coroutines;
    private List<RobotTrajectoryPoint> last_commands_before_collision;
    private List<CollisionEvent> collision_states;
    public void setRobotCollisionState(RobotController robot,CollisionEvent state){
        collision_states[robots.IndexOf(robot)]=state;
    }
    public CollisionEvent getRobotCollisionState(int i){
        return collision_states[i];
    }
    public RobotTrajectoryPoint getRobotLastState(int i){
        return last_commands_before_collision[i];
    }
    public double getColissionTime(){
        foreach(var collision in collision_states){
            if(collision!=null){
                return collision.time;
            }
        }
        return 0.0;
    }
    void Start()
    {
        coroutines = new List<IEnumerator>();
        last_commands_before_collision = new List<RobotTrajectoryPoint>(){null, null};
        collision_states = new List<CollisionEvent>(){null, null};
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
    public void StopSimulation(){
            foreach (var coroutine in coroutines){
                StopCoroutine(coroutine);
            }
            //capture states
            for(int i=0;i<robots.Count;i++){
                last_commands_before_collision[i]=robots[i].LastCommand;
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
