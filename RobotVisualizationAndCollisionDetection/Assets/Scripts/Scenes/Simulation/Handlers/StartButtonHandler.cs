using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonHandler : MonoBehaviour
{
    public Button button;
    public SimulationController simulationController;

    void Start()
    {
        button.interactable = false;
    }

    void Update()
    {
        if(simulationController.AllReady())
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void StartSimulation()
    {
        simulationController.TryStartSimulation();
    }
}
