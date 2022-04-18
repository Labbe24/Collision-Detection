using System;
using UnityEngine;

namespace CollisionDetection.Robot.Startup
{
    public class CoordinatesHandler : MonoBehaviour
    {
        public ObjectCoordinates robot1Coordinates, robot2Coordinates;

        public CoordinatesHandler()
        {
            robot1Coordinates = new ObjectCoordinates(0, 0, 0);
            robot2Coordinates = new ObjectCoordinates(1, 0, 0);
        }

        public void robot1XCoordinateOnChange(string coordinate)
        {
            robot1Coordinates.X = (float)Convert.ToDouble(validateString(coordinate));
        }
        public void robot1YCoordinateOnChange(string coordinate)
        {
            robot1Coordinates.Y = (float)Convert.ToDouble(validateString(coordinate)); ;
        }
        public void robot1ZCoordinateOnChange(string coordinate)
        {
            robot1Coordinates.Z = (float)Convert.ToDouble(validateString(coordinate)); ;
        }
        public void robot2XCoordinateOnChange(string coordinate)
        {
            robot2Coordinates.X = (float)Convert.ToDouble(validateString(coordinate)); ;
        }
        public void robot2YCoordinateOnChange(string coordinate)
        {
            robot2Coordinates.Y = (float)Convert.ToDouble(validateString(coordinate)); ;
        }
        public void robot2ZCoordinateOnChange(string coordinate)
        {
            robot2Coordinates.Z = (float)Convert.ToDouble(validateString(coordinate)); ;
        }

        private string validateString(string coordinate)
        {
            string temp = "";

            switch (coordinate)
            {
                case "":
                    temp = "0";
                    break;
                case "-":
                    temp = "-0";
                    break;
                default:
                    temp = coordinate;
                    break;
            }
            temp = temp.Replace('.', ',');

            return temp;

        }
    }
}