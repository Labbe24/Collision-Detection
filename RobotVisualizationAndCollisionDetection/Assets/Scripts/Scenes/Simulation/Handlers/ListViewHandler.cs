using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListViewHandler : MonoBehaviour
{
    public GameObject listItemPrefab;

    // Start is called before the first frame update
    async void Start()
    {
    }

    public void AddCollisions(List<CollisionEvent> events)
    {
        foreach (var colloisionEvent in events)
        {
            AddListItem(colloisionEvent.jointIndex.ToString(), colloisionEvent.robotName, colloisionEvent.jointName, colloisionEvent.jointPosition.ToString(), colloisionEvent.time.ToString());
        }
    }

    public void AddListItem(string id, string robot, string jointName, string position, string time)
    {
        GameObject listItem = Instantiate(listItemPrefab);
        listItem.transform.SetParent(this.gameObject.transform, false);
        listItem.transform.GetChild(0).GetComponent<Text>().text = id;
        listItem.transform.GetChild(1).GetComponent<Text>().text = robot;
        listItem.transform.GetChild(2).GetComponent<Text>().text = jointName;
        listItem.transform.GetChild(3).GetComponent<Text>().text = position;
        listItem.transform.GetChild(4).GetComponent<Text>().text = time;
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
