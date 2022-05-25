using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastCommandListViewHandler : MonoBehaviour
{
    public int robotIndex;
    public GameObject listItemPrefab;
    public SimulationController simulationController;

    void Start()
    {
        
    }

    /// <summary>
    /// Clears UI view and sets new states on UI view
    /// </summary>
    void OnEnable()
    {
        ClearList();
        var state=simulationController.GetRobotLastState(robotIndex);
        if(state!=null){
            AddStates(state);
        }
    }

    /// <summary>
    /// Adds state to view
    /// </summary>
    /// <param name="robotState"></param>
    public void AddStates(RobotTrajectoryPoint robotState)
    {
        for(int i = 0; i < robotState.joint_names.Length; i++)
        {
            AddListItem(robotState.joint_names[i], robotState.point.positions[i].ToString());
        }
    }

    /// <summary>
    /// Adds on view item to list view
    /// </summary>
    /// <param name="jointName"></param>
    /// <param name="jointPosition"></param>
    public void AddListItem(string jointName, string jointPosition)
    {
        GameObject listItem = Instantiate(listItemPrefab);
        listItem.transform.SetParent(this.gameObject.transform, false);
        listItem.transform.GetChild(0).GetComponent<Text>().text = jointName;
        listItem.transform.GetChild(1).GetComponent<Text>().text = jointPosition;
    }

    /// <summary>
    /// Clears view
    /// </summary>
    public void ClearList()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
