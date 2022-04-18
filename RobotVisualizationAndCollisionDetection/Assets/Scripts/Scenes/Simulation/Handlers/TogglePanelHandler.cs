using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanelHandler : MonoBehaviour
{
    public GameObject panel;
    private bool show = false;
    // Start is called before the first frame update
    void Start()
    {
        panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {     
    }

    public void Toggle()
    {
        this.show = !this.show;
        panel.gameObject.SetActive(this.show);
    }
}
