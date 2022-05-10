using System.Collections;
using System.Collections.Generic;
using CollisionDetection.Robot.Control;
using UnityEngine;
using UnityEngine.Assertions;

public class RobotCollisionDetectionController : MonoBehaviour
{
    // Start is called before the first frame update
    private RobotController _robotController;
    private MeshCollider _collider;
    void Start()
    {
        _collider = this.GetComponent<MeshCollider>();
        _robotController = this.GetComponentInParent<RobotController>();

        Assert.IsNotNull(_collider);
        Assert.IsNotNull(_robotController);
        _collider.convex = true;
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
        Debug.Log("Joint index: " + articulationBody.index);
        Debug.Log("Joint name: " + articulationBody.name);

        if (articulationBody.jointPosition.dofCount == 1)
        {
            Debug.Log("Joint position: " + articulationBody.jointPosition[0]);
            var collisionEvent = new CollisionEvent();

            collisionEvent.robotName = _collider.transform.root.name;
            collisionEvent.jointName = articulationBody.name;
            collisionEvent.jointIndex = articulationBody.index;
            collisionEvent.jointPosition = articulationBody.jointPosition[0];
            collisionEvent.time = _robotController.GetElapsedTime();
            _robotController.events.Add(collisionEvent);
        }
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
