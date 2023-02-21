using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.CallCentre
{
    public enum CorridorFace
    {
        Back,
        None,
        Front
    }
    public enum CorridorDoors
    {
        One,
        Two
    }
    public class Generation : MonoBehaviour
    {
        [SerializeField] private GameObject[] CCRooms;
        [SerializeField] private GameObject[] CCCoridoors;

        /*
         * Corridors
         * 00 - Back, two left, one right * 0
         * 01 - Back, one left, one right * 1
         * 02 - Back, two left, two right * 2
         * 03 - Back, one left, two right * 3
         * 04 - No back, two left, one right * 4
         * 05 - No back, one left, one right * 5
         * 06 - No back, two left, two right * 6
         * 07 - No back, one left, two right * 7
         * 08 - Front, one left, two right
         * 09 - Front, two left, two right
         * 10 - Front, one left, one right
         * 11 - Front, two left, one right
         * 
         * Rooms (Large)
         * 12 - South * 0
         * 13 - West
         * 14 - North
         * 14 - East
         * 15 - South, East * 1
         * 16 - West, South
         * 17 - North, West
         * 18 - East, North
         * 19 - East, West * 2
         * 20 - North, South
         * 21 - East, South, West * 3
         * 22 - South, West, North
         * 23 - West, North, East
         * 24 - North, East, South
         * 25 - All * 4
         */

        private void GenerateCC(int size)
        {
            int[,] MapLayout = new int[size, size];
        }

        private void ObjectRotate90(GameObject obj)
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        private void ObjectRotate180(GameObject obj)
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        private void ObjectRotate270(GameObject obj)
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
        }
        private GameObject GetCorridorStructure(CorridorFace face, CorridorDoors left, CorridorDoors right)
        {
            GameObject ret = null;
            switch (face)
            {
                case CorridorFace.Back:
                    break;
                case CorridorFace.None:
                    break;
                case CorridorFace.Front:
                    break;
            }
            return ret;
        }
    }
}