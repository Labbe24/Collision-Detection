using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastCommandListViewHandler : MonoBehaviour
{
    public int robotIndex;
    public GameObject listItemPrefab;
    public SimulationController simulationController;
    // Start is called before the first frame update
    async void Start()
    {
        
    }

    void OnEnable()
    {
        ClearList();
        var state=simulationController.getRobotLastState(robotIndex);
        if(state!=null){
            AddStates(state);
        }
    }
    public void AddStates(RobotTrajectoryPoint robotState)
    {
        for(int i = 0; i < robotState.joint_names.Length; i++)
        {
            AddListItem(robotState.joint_names[i], robotState.point.positions[i].ToString());
        }
    }

    public void AddListItem(string jointName, string jointPosition)
    {
        GameObject listItem = Instantiate(listItemPrefab);
        listItem.transform.SetParent(this.gameObject.transform, false);
        listItem.transform.GetChild(0).GetComponent<Text>().text = jointName;
        listItem.transform.GetChild(1).GetComponent<Text>().text = jointPosition;
    }

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
