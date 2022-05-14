using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RobotNameHandler : MonoBehaviour
{
    public SimulationController simulationController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        gameObject.GetComponent<Text>().text = getCollisionText();
    }
    string getCollisionText(){
        var robot1Collision = simulationController.getRobotCollisionState(0);
        var robot2Collision = simulationController.getRobotCollisionState(1);
        if(robot1Collision!=null&&robot2Collision!=null){
                return robot1Collision.robotState.name+" "+robot1Collision.collided_joint+" collided with "+
                robot2Collision.robotState.name+" "+robot2Collision.collided_joint;
        }else if(robot1Collision!=null){
                return robot1Collision.robotState.name+" "+robot1Collision.collided_joint+" collided with "+
                "environment";
        }else if(robot2Collision!=null){
            return robot2Collision.robotState.name+" "+robot2Collision.collided_joint+" collided with "+
            "environment";
        }
        else{
            return "No collisions detected.";
        }

    }
}
