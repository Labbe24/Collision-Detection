using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePanelHandler : MonoBehaviour
{
    public GameObject panel;
    public Button button;
    public SimulationController simulationController;
    private bool show = false;

    void Start()
    {
        panel.gameObject.SetActive(false);
    }

    void Update()
    {
        if(simulationController.collisionStates.Count > 0)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    /// <summary>
    /// Toggles UI panel
    /// </summary>
    public void Toggle()
    {
        this.show = !this.show;
        panel.gameObject.SetActive(this.show);
    }
}
