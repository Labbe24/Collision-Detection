using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.UrdfImporter;
using Unity.Robotics.UrdfImporter.Control;
using CollisionDetection.Robot.Control;
namespace CollisionDetection.Robot.Model
{
    public static class UrdfImporter
    {
        public static bool useVHACD = true; // this is not yet fully functional in runtime.
        public static bool clearOnLoad = true;
        // The values below are tested to work with the niryo_one URDF:
        private static float controllerStiffness = 10000;
        private static float controllerDamping = 100;
        private static float controllerForceLimit = 1000;
        private static float controllerSpeed = 30;
        private static float controllerAcceleration = 10;
        public static bool setImmovableLink = true;
        //public static string immovableLinkName = "base_link";

        /// <summary>
        /// Loads a specified urdf file
        /// </summary>
        /// <param name="urdfFilepath">Path to urdf file</param>
        /// <param name="transform"><Robots transform/param>
        /// <param name="immovableLinkName">Name of the robots root</param>
        /// <returns>Robot as GameObject</returns>
        public static GameObject LoadUrdf(string urdfFilepath, Transform transform, string immovableLinkName)
        {
            if (string.IsNullOrEmpty(urdfFilepath))
            {
                return null;
            }

            ImportSettings settings = new ImportSettings
            {
                chosenAxis = ImportSettings.axisType.yAxis,
                convexMethod = useVHACD ? ImportSettings.convexDecomposer.vHACD : ImportSettings.convexDecomposer.unity,
            };

            GameObject robotObject = null;

            robotObject = UrdfRobotExtensions.CreateRuntime(urdfFilepath, settings);

            if (robotObject != null && robotObject.transform != null)
            {
                robotObject.transform.SetParent(transform);
                SetControllerParameters(robotObject, immovableLinkName);
                Debug.Log("Successfully Loaded URDF" + robotObject.name);
            }
            return robotObject;
        }

        /// <summary>
        /// Sets parameters of RobotController
        /// </summary>
        /// <param name="robot">Robot to add RobotController to</param>
        /// <param name="immovableLinkName">Name of the robots root</param>
        private static void SetControllerParameters(GameObject robot, string immovableLinkName)
        {
            Transform baseNode = FirstChildByName(robot.transform, immovableLinkName);
            if (baseNode && baseNode.TryGetComponent<ArticulationBody>(out ArticulationBody baseNodeAB))
            {
                baseNodeAB.immovable = true;
            }

            if (robot.TryGetComponent<Controller>(out Controller c))
            {
                GameObject.Destroy(c);
            }
            RobotController controller = robot.AddComponent(typeof(RobotController)) as RobotController;
            controller.stiffness = controllerStiffness;
            controller.damping = controllerDamping;
            controller.forceLimit = controllerForceLimit;
            controller.speed = controllerSpeed;
            controller.acceleration = controllerAcceleration;
        }

        /// <summary>
        /// Gets the first child by name
        /// </summary>
        /// <param name="parent">Parent of the hierarchy</param>
        /// <param name="name">Name to match child by</param>
        /// <returns>Transform of found child or null</returns>
        private static Transform FirstChildByName(Transform parent, string name)
        {
            if (parent.childCount == 0)
            {
                return null;
            }

            Transform result = null;
            for (int i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                if (child.name == name)
                {
                    return child;
                }
                result = FirstChildByName(child, name);
            }
            return result;
        }
    }
}