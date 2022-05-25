using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeHandler : MonoBehaviour
{
    public SimulationController simulationController;

    void Start()
    {
        
    }

    void Update()
    {    
    }

    /// <summary>
    /// Gets simulation time and sets it in UI panel
    /// </summary>
    void OnEnable()
    {
        var time = simulationController.GetColissionTime();
        if(time!=0.0){
            gameObject.GetComponent<Text>().text = "At simulation time: "+ time;
        }else{
            gameObject.GetComponent<Text>().text="";
        }
    }
}
