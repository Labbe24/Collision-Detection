using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeHandler : MonoBehaviour
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
        var time = simulationController.getColissionTime();
        if(time!=0.0){
            gameObject.GetComponent<Text>().text = "At simulation time: "+time;
        }else{
            gameObject.GetComponent<Text>().text="";
        }
    }
}
