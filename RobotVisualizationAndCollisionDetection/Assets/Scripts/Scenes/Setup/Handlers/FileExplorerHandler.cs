using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CollisionDetection.Robot.Startup
{
    public class FileExplorerHandler : MonoBehaviour
    {
        public Text resultPath;
        public Text warningMessage;

        void Start()
        {
        }

        void Update()
        {
        }

        /// <summary>
        /// Opens file explorer
        /// </summary>
        /// <returns></returns>
        public async void OpenFolderBrowser()
        {
            warningMessage.text = "";
            resultPath.text = EditorUtility.OpenFolderPanel("Select mapper directory", "", "");

            if (resultPath.text != string.Empty)
            {
                List<string> files = new List<string>(Directory.GetFiles(resultPath.text));

                List<string> jsonFiles = new List<string>();
                List<string> urdfFiles = new List<string>();

                foreach (string file in files)
                {
                    if (file.EndsWith(".json"))
                    {
                        jsonFiles.Add(file);
                    }

                    if (file.EndsWith(".urdf"))
                    {
                        urdfFiles.Add(file);
                    }
                }

                switch (jsonFiles.Count())
                {
                    case 0:
                        warningMessage.text += "Directory do not contain any json files. ";
                        break;
                    case 1:

                        if (Path.GetFileName(jsonFiles[0]) != "mapping.json")
                        {
                            warningMessage.text += "The mapper-file does not have the naming convention \"mapping.json\". ";
                        }
                        break;
                    case int n when n > 1:
                        warningMessage.text += "Directory contains more than one json file. ";
                        break;

                    default:
                        break;
                }

                switch (urdfFiles.Count())
                {
                    case 0:
                        warningMessage.text += "Directory do not contain any urdf files. ";
                        break;
                    case 1:
                        if (Path.GetFileName(urdfFiles[0]) != "model.urdf")
                        {
                            warningMessage.text += "The urdf-file does not have the naming convention \"model.urdf\". ";
                        }
                        break;
                    case int n when n > 1:
                        warningMessage.text += "Directory contains more than one urdf file. ";
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
