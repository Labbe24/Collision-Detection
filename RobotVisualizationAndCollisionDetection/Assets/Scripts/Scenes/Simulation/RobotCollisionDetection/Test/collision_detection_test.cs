using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_detection_test : MonoBehaviour
{
    public float m_Speed;
    private Rigidbody _rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //Change the GameObject's movement in the X axis
        float translationX = Input.GetAxis("Horizontal") * m_Speed;

        //Move the GameObject
        transform.Translate(new Vector3(translationX, 0, 0));

        //Press the space key to switch the collision detection mode
        if (Input.GetKeyDown(KeyCode.Space))
            SwitchCollisionDetectionMode();
    }

    //Detect when there is a collision starting
    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("OnCollisionEnter");
        //Ouput the Collision to the console
        //Debug.Log("Collision :" + collision.gameObject.name);

        // Print how many points are colliding with this transform
        // Debug.Log("Points colliding: " + collision.contacts.Length);

        // Draw a different colored ray for every normal in the collision
        // for (int i = 0; i < collision.contacts.Length; i++)
        // {
            // Debug.Log("Point: " + collision.contacts[i].point);
            // Debug.Log("Normal of point " + i + ": " + collision.contacts[i].normal);
            // Vector3 start = collision.contacts[i].point;
            // Vector3 dir = collision.contacts[i].normal;
            // Debug.Log("start: " + start);
            // Debug.Log("dir: " + dir);
            // Debug.DrawRay(start, dir*100, Color.red, 1, false);

        // }

        // Log joint index, joint name and joint position
        // ArticulationBody articulationBody = collision.collider.attachedArticulationBody;
        // Debug.Log("Joint index: " + articulationBody.index);
        // Debug.Log("Joint name: " + articulationBody.name);
        // for (int j = 0; j < articulationBody.jointPosition.dofCount; j++)
        // {
        //     Debug.Log("Joint position: " + articulationBody.jointPosition[j]);
        // }
    }

    //Detect when there is are ongoing Collisions
    void OnCollisionStay(Collision collision)
    {
        //Output the Collision to the console
        // Debug.Log("Stay :" + collision.gameObject.name);
    }

    void OnCollisionExit(Collision collision)
    {

    }

    private void SwitchCollisionDetectionMode()
    {
        switch (_rigidBody.collisionDetectionMode)
        {
            //If the current mode is continuous, switch it to continuous dynamic mode
            case CollisionDetectionMode.Continuous:
                _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                Debug.Log("ContinuousDynamic");
                break;
            //If the current mode is continuous dynamic, switch it to continuous speculative
            case CollisionDetectionMode.ContinuousDynamic:
                _rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                Debug.Log("ContinuousSpeculative");
                break;

            // If the curren mode is continuous speculative, switch it to discrete mode
            case CollisionDetectionMode.ContinuousSpeculative:
                _rigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
                Debug.Log("Discrete");
                break;

            //If the current mode is discrete, switch it to continuous mode
            case CollisionDetectionMode.Discrete:
                _rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                Debug.Log("Continuous");
                break;
        }
    }
}
