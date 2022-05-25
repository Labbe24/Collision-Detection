using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using CollisionDetection.Robot.Configuration;

namespace CollisionDetection.Robot.Startup
{
    public class SetupController : MonoBehaviour
    {
        public FileExplorerHandler robot1Path;
        public FileExplorerHandler robot2Path;
        public TrajectoryRequestHandler trajectoryRequestHandler1;
        public TrajectoryRequestHandler trajectoryRequestHandler2;
        public Button button;
        void Start()
        {
            robot1Path = GameObject.FindWithTag("robotPath1").GetComponent<FileExplorerHandler>();
            robot2Path = GameObject.FindWithTag("robotPath2").GetComponent<FileExplorerHandler>();
            trajectoryRequestHandler1 = GameObject.FindWithTag("trajectoryRequestHandler1").GetComponent<TrajectoryRequestHandler>();
            trajectoryRequestHandler2 = GameObject.FindWithTag("trajectoryRequestHandler2").GetComponent<TrajectoryRequestHandler>();
            button = GameObject.FindWithTag("startButton").GetComponent<Button>();
            button.interactable = false;
        }

        void Update()
        {
            if (robot1Path.resultPath.text != ""
                && robot2Path.resultPath.text != ""
                && robot1Path.warningMessage.text == ""
                && robot2Path.warningMessage.text == ""
                && trajectoryRequestHandler1.resultPath.text != ""
                && trajectoryRequestHandler2.resultPath.text != ""
                && trajectoryRequestHandler1.warningMessage.text == ""
                && trajectoryRequestHandler2.warningMessage.text == ""
                )
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }

        /// <summary>
        /// Loads simulation environment scene
        /// </summary>
        public void GoToNextScene()
        {
            RobotConfigStore.configurations.Insert(
                0,
                new RobotConfiguration
                {
                    path = robot1Path.resultPath.text,
                    subscribeTopic = "robot_1_trajectory",
                    serviceName = "generate_trajectory_srv",
                    trajectoryRequestPath = trajectoryRequestHandler1.resultPath.text,
                    trajectoryRequestName = "trajectoryRequestRobot1.json",
                });
            RobotConfigStore.configurations.Insert(
                1,
                new RobotConfiguration
                {
                    path = robot2Path.resultPath.text,
                    subscribeTopic = "default",
                    serviceName = "generate_trajectory_srv1",
                    trajectoryRequestPath = trajectoryRequestHandler2.resultPath.text,
                    trajectoryRequestName = "trajectoryRequestRobot2.json",

                });
            SceneManager.LoadScene(1);
        }
    }
}
