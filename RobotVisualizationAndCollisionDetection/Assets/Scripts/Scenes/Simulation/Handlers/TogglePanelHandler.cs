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
        if(simulationController.collisionStates.TrueForAll(c => c == null))
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
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
