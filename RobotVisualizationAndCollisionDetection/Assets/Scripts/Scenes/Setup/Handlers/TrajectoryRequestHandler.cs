using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CollisionDetection.Robot.Startup
{
    public class TrajectoryRequestHandler : MonoBehaviour
    {
        public Text resultPath;
        public Text warningMessage;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public async void OpenFolderBrowser()
        {
            warningMessage.text = "";
            resultPath.text = EditorUtility.OpenFolderPanel("Select trajectory request directory", "", "");

            if (resultPath.text != string.Empty)
            {
                List<string> files = new List<string>(Directory.GetFiles(resultPath.text));

                List<string> jsonFiles = new List<string>();

                foreach (string file in files)
                {
                    if (file.EndsWith(".json"))
                    {
                        jsonFiles.Add(file);
                    }
                }

                switch (jsonFiles.Count())
                {
                    case 0:
                        warningMessage.text += "Directory do not contain any json files. ";
                        break;
                    case 1:
                        if (Path.GetFileName(jsonFiles[0]) == "trajectoryRequestRobot1.json" || Path.GetFileName(jsonFiles[0]) == "trajectoryRequestRobot2.json")
                        {
                        }
                        else
                        {
                            warningMessage.text += "The trajectory request file does not have the naming convention \"trajectoryRequest[number].json\". ";
                        }
                        break;
                    case 2:
                        if (Path.GetFileName(jsonFiles[0]) != "trajectoryRequestRobot1.json" || Path.GetFileName(jsonFiles[1]) != "trajectoryRequestRobot2.json")
                        {
                            warningMessage.text += "One of the trajectory request files does not have the naming convention \"trajectoryRequest[number].json\". ";
                        }
                        break;
                    case int n when n > 2:
                        warningMessage.text += "Directory contains more than one json file. ";
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
