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
        if(simulationController.isSimulating)
        {
            button.gameObject.SetActive(false);
        }
        else
        {
            button.gameObject.SetActive(true);
        }

        if(simulationController.AllReady())
        {
            Debug.Log("All ready");
            button.interactable = true;
        }
        else
        {
            Debug.Log("All not ready");
            button.interactable = false;
        }
    }

    public void StartSimulation()
    {
        simulationController.TryStartSimulation();
    }
}
