using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CollisionDetection.Robot.Control;
using UnityEngine;
using UnityEngine.Assertions;

public class RobotCollisionDetectionController : MonoBehaviour
{
    private RobotController _robotController;
    private MeshCollider _collider;
    private SimulationController _simulationController;

    void Start()
    {
        _collider = this.GetComponent<MeshCollider>();
        _robotController = this.GetComponentInParent<RobotController>();
        _simulationController = GameObject.FindWithTag("SimulationController").GetComponent<SimulationController>();
        Assert.IsNotNull(_collider);
        Assert.IsNotNull(_robotController);
        _collider.convex = true;
    }

    void Update()
    {
    }

    /// <summary>
    /// Detects when Collison begins. Collision of the rigibody this collider is colliding with. Not used in the below implementation.
    /// The below implementation is furthermore heavily dependent upon the knowledge that joints of the articulationBody only have one degree of freedom.
    /// Hence, hardcoded to target position 0 of jointPosition array.
    /// </summary>
    /// <param name="collision">Object containg information about the detected collision</param>
        void OnCollisionEnter(Collision collision)
    {
        _simulationController.StopSimulation();

        ArticulationBody articulationBody = _collider.attachedArticulationBody;
        List<ArticulationBody> bodies = GetRelatedArticulationChain(articulationBody);
        var robotState = GetRobotState(articulationBody, bodies);

        Debug.Log("Robots collided " + articulationBody.transform.root.name);

        double elapsedTime = _robotController.GetElapsedTime();
        var collisionEvent = GetCollisionEvent(elapsedTime, robotState, articulationBody.name);

        _simulationController.SetRobotCollisionState(_robotController, collisionEvent);
    }

    /// <summary>
    /// Creates a CollisionEvent
    /// </summary>
    /// <param name="time">Elapsed time since simulation began</param>
    /// <param name="robotState">State of the robot at collision</param>
    /// <param name="jointName">Name of the joint that triggered the collision</param>
    /// <returns></returns>
    CollisionEvent GetCollisionEvent(double time, RobotState robotState, string jointName)
    {
        var collisionEvent = new CollisionEvent();
        collisionEvent.time = _robotController.GetElapsedTime();
        collisionEvent.robotState = robotState;
        collisionEvent.collidedJoint = jointName;
        return collisionEvent;
    }
    
    /// <summary>
    /// Get's all the rlated articulation bodies in the articulation chain
    /// </summary>
    /// <param name="articulationBody"></param>
    /// <returns>List of all related articulation bodies</returns>
    List<ArticulationBody> GetRelatedArticulationChain(ArticulationBody articulationBody)
    {
        List<ArticulationBody> bodies = new List<ArticulationBody>();
        bodies.AddRange(articulationBody.GetComponentsInChildren<ArticulationBody>());
        bodies.AddRange(articulationBody.GetComponentsInParent<ArticulationBody>());
        List<ArticulationBody> distinctBodies = bodies.GroupBy(x => x.name).Select(y => y.First()).ToList();
        return distinctBodies;
    }

    /// <summary>
    /// Get's the state of the robots when the collision occured
    /// </summary>
    /// <param name="rootBody">Parent to all other articulation bodies</param>
    /// <param name="bodies">All articulation bodies</param>
    /// <returns>State of the robot at collision</returns>
    RobotState GetRobotState(ArticulationBody rootBody, List<ArticulationBody> bodies)
    {
        var robotState = new RobotState();
        robotState.name = rootBody.transform.root.name;
        
        foreach(var ab in bodies)
        {
            if (ab.jointPosition.dofCount == 1)
            {
                robotState.jointNames.Add(ab.name);
                robotState.jointPositions.Add(ab.jointPosition[0].ToString());
            }
        }
        return robotState;
    }
}
