using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RobotNameHandler : MonoBehaviour
{
    public SimulationController simulationController;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        gameObject.GetComponent<Text>().text = GetCollisionText();
    }

    /// <summary>
    /// Gets title text for UI panel
    /// </summary>
    /// <returns>Collision text</returns>
    public string GetCollisionText(){
        var robot1Collision = simulationController.GetRobotCollisionState(0);
        var robot2Collision = simulationController.GetRobotCollisionState(1);

        if(robot1Collision!=null&&robot2Collision!=null)
        {
                return robot1Collision.robotState.name+" "+robot1Collision.collidedJoint+" collided with "+
                robot2Collision.robotState.name+" "+robot2Collision.collidedJoint;
        }
        else if(robot1Collision!=null)
        {
                return robot1Collision.robotState.name+" "+robot1Collision.collidedJoint+" collided with "+
                "environment";
        }
        else if(robot2Collision!=null)
        {
            return robot2Collision.robotState.name+" "+robot2Collision.collidedJoint+" collided with "+
            "environment";
        }
        else
        {
            return "No collisions detected.";
        }
    }
}
