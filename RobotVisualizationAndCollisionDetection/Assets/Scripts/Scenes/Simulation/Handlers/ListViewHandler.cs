using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListViewHandler : MonoBehaviour
{
    public int robotIndex;
    public GameObject listItemPrefab;
    public SimulationController simulationController;

    void Start()
    {
        
    }

    /// <summary>
    /// Gets collision state of robots
    /// </summary>
    void OnEnable()
    {
        ClearList();
        var state=simulationController.GetRobotCollisionState(robotIndex);
        if(state!=null){
            AddCollisions(state.robotState);
        }
    }

    /// <summary>
    /// Adds collision to UI view
    /// </summary>
    /// <param name="robotState"></param>
    public void AddCollisions(RobotState robotState)
    {
        for(int i = 0; i < robotState.jointNames.Count; i++)
        {
            AddListItem(robotState.jointNames[i], robotState.jointPositions[i]);
        }
    }

    /// <summary>
    /// Adds collision item to view
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
    /// Clears UI view
    /// </summary>
    public void ClearList()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    
    void Update()
    {

    }
}
