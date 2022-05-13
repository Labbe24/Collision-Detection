using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CollisionDetection.Robot.Control;
using UnityEngine;
using UnityEngine.Assertions;

public class RobotCollisionDetectionController : MonoBehaviour
{
    // Start is called before the first frame update
    private RobotController _robotController;
    private Collider _collider;
    void Start()
    {
        _collider = this.GetComponent<MeshCollider>();
        _robotController = this.GetComponentInParent<RobotController>();

        Assert.IsNotNull(_collider);
        Assert.IsNotNull(_robotController);
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Detects when Collison begins. 
    /// </summary>
    /// <param name="collision">Collision of the rigibody this collider is colliding with. Not used in the below implementation.
    /// The below implementation is furthermore heavily dependent upon the knowledge that joints of the articulationBody only have one degree of freedom.
    /// Hence, hardcoded to target position 0 of jointPosition array.</param>
    void OnCollisionEnter(Collision collision)
    {
        // Log joint index, joint name and joint position
        ArticulationBody articulationBody = _collider.attachedArticulationBody;
        List<ArticulationBody> bodies = new List<ArticulationBody>();
        bodies.AddRange(articulationBody.GetComponentsInChildren<ArticulationBody>());
        bodies.AddRange(articulationBody.GetComponentsInParent<ArticulationBody>());
        List<ArticulationBody> distinctBodies = bodies.GroupBy(x => x.name).Select(y => y.First()).ToList();

        ArticulationBody otherArticulationBody = collision.collider.attachedArticulationBody;
        List<ArticulationBody> otherBodies = new List<ArticulationBody>();
        bodies.AddRange(articulationBody.GetComponentsInChildren<ArticulationBody>());
        bodies.AddRange(articulationBody.GetComponentsInParent<ArticulationBody>());
        List<ArticulationBody> otherDistinctBodies = bodies.GroupBy(x => x.name).Select(y => y.First()).ToList();
        
        var collisionEvent = new CollisionEvent();
        collisionEvent.time = _robotController.GetElapsedTime();
        var robotOneState = new RobotState();
        var robotTwoState = new RobotState();
        robotOneState.name = articulationBody.transform.root.name;
        robotTwoState.name = otherArticulationBody.transform.root.name;
        
        foreach(var ab in distinctBodies)
        {
            if (ab.jointPosition.dofCount == 1)
            {
                robotOneState.jointNames.Add(ab.name);
                robotOneState.jointPositions.Add(ab.jointPosition[0].ToString());
            }
        }

        foreach(var ab in otherDistinctBodies)
        {
            if (ab.jointPosition.dofCount == 1)
            {
                robotTwoState.jointNames.Add(ab.name);
                robotTwoState.jointPositions.Add(ab.jointPosition[0].ToString());     
            }
        }
        Debug.Log("Robots collided " + articulationBody.transform.root.name);
        collisionEvent.robotStates.Add(robotOneState);
        collisionEvent.robotStates.Add(robotTwoState);
        _robotController.collision = collisionEvent;
    }

    //Detect when there are ongoing Collisions
    void OnCollisionStay(Collision collision)
    {
    }

    //Detect when Collisions exit
    void OnCollisionExit(Collision collision)
    {
    }
}
