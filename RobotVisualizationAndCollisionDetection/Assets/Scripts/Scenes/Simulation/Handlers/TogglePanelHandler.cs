using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanelHandler : MonoBehaviour
{
    public GameObject panel;
    private bool show = false;

    void Start()
    {
        panel.gameObject.SetActive(false);
    }

    void Update()
    {     
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
