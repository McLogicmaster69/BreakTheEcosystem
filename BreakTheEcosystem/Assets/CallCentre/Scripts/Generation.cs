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
        [SerializeField] private int Size = 10;
        [SerializeField] [Range(0, 1)] private float ConnectingChance = 0.45f;
        
        [Header("Blocks")]
        [SerializeField] private GameObject[] CCCoridoors;
        [SerializeField] private GameObject[] CCIntersections;
        [SerializeField] private GameObject[] CCRooms;

        /*
         * Corridors
         * 00 - North, no left, no right (00)
         * 01 - East, no left, no right
         * 02 - South, no left, no right
         * 03 - West, no left, no right
         * 04 - North, left, no right (01)
         * 05 - East, left, no right
         * 06 - South, left, no right
         * 07 - West, left, no right
         * 08 - North, no left, right (02)
         * 09 - East, no left, right
         * 10 - South, no left, right
         * 11 - West, no left, right
         * 12 - North, left, right (03)
         * 13 - East, left, right
         * 14 - South, left, right
         * 15 - West, left, right
         * 16 - North, South, no left, no right (04)
         * 17 - East, West, no left, no right
         * 18 - North, South, left, no right (05)
         * 19 - East, West, left, no right
         * 20 - North, South, no left, right (06)
         * 21 - East, West, no left, right
         * 22 - North, South, left, right (07)
         * 23 - East, West, left, right
         * 
         * Intersections
         * 24 - North, East (00)
         * 25 - East, South
         * 26 - South, West
         * 27 - West, North
         * 28 - North, East, South (01)
         * 29 - East, South, West
         * 30 - South, West, North
         * 31 - West, North, East
         * 32 - North, East, West, South (02)
         * 
         * Rooms
         * 33 - North (00)
         * 34 - East
         * 35 - South
         * 36 - West
         * 37 - North, East (01)
         * 38 - East, South
         * 39 - South, West
         * 40 - West, North
         * 41 - North, South (02)
         * 42 - East, West
         * 43 - North, East, South (03)
         * 44 - East, South, West
         * 45 - South, West, North
         * 46 - West, North, East
         * 47 - North, East, West, South (04)
         */

        private void Start()
        {
            GenerateCC(Size);
        }

        private void GenerateCC(int size)
        {
            TileInfo[,] MapLayout = new TileInfo[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    MapLayout[x, y] = new TileInfo();
                }
            }

            bool[] XIsCorrdidor = new bool[size];
            bool[] YIsCorrdidor = new bool[size];
            for (int i = 0; i < size; i++)
            {
                XIsCorrdidor[i] = false;
                YIsCorrdidor[i] = false;
            }

            List<int> availableX = new List<int>();
            List<int> constructX = new List<int>();
            List<int> availableY = new List<int>();
            List<int> constructY = new List<int>();
            for (int i = 0; i < size; i++)
            {
                availableX.Add(i);
                availableY.Add(i);
            }
            availableY.Remove(0);

            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                int index = Random.Range(0, availableX.Count);
                constructX.Add(availableX[index]);
                int value = availableX[index];
                if (availableX.Contains(availableX[index] + 1))
                    availableX.Remove(availableX[index] + 1);
                if (availableX.Contains(availableX[index] - 1))
                    availableX.Remove(availableX[index] - 1);
                availableX.Remove(value);
            }
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                int index = Random.Range(0, availableY.Count);
                constructY.Add(availableY[index]);
                int value = availableY[index];
                if (availableY.Contains(availableY[index] + 1))
                    availableY.Remove(availableY[index] + 1);
                if (availableY.Contains(availableY[index] - 1))
                    availableY.Remove(availableY[index] - 1);
                availableX.Remove(value);
            }

            foreach (int cx in constructX)
            {
                XIsCorrdidor[cx] = true;
                MapLayout[cx, 0].Connected = true;
                MapLayout[cx, 0].Type = BlockType.Corridor;
                MapLayout[cx, 0].North = true;
                for (int i = 1; i < size - 1; i++)
                {
                    MapLayout[cx, i].Connected = true;
                    MapLayout[cx, i].Type = BlockType.Corridor;
                    MapLayout[cx, i].North = true;
                    MapLayout[cx, i].South = true;
                }
                MapLayout[cx, size - 1].Connected = true;
                MapLayout[cx, size - 1].Type = BlockType.Corridor;
                MapLayout[cx, size - 1].South = true;
            }
            foreach (int cy in constructY)
            {
                YIsCorrdidor[cy] = true;
                MapLayout[0, cy].Connected = true;
                if(MapLayout[0, cy].Type == BlockType.Corridor)
                    MapLayout[0, cy].Type = BlockType.Intersection;
                else
                    MapLayout[0, cy].Type = BlockType.Corridor;
                MapLayout[0, cy].East = true;
                for (int i = 1; i < size - 1; i++)
                {
                    MapLayout[i, cy].Connected = true;
                    if (MapLayout[i, cy].Type == BlockType.Corridor)
                        MapLayout[i, cy].Type = BlockType.Intersection;
                    else
                        MapLayout[i, cy].Type = BlockType.Corridor;
                    MapLayout[i, cy].East = true;
                    MapLayout[i, cy].West = true;
                }
                MapLayout[size - 1, cy].Connected = true;
                if (MapLayout[size - 1, cy].Type == BlockType.Corridor)
                    MapLayout[size - 1, cy].Type = BlockType.Intersection;
                else
                    MapLayout[size - 1, cy].Type = BlockType.Corridor;
                MapLayout[size - 1, cy].West = true;
            }

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if(MapLayout[x, y].Type == BlockType.Room)
                    {

                    }
                }
            }

            BuildCC(MapLayout, size, XIsCorrdidor, YIsCorrdidor);
        }
        private void BuildCC(TileInfo[,] MapLayout, int size, bool[] xs, bool[] ys)
        {
            float currentXPos = 0f;
            for (int x = 0; x < size; x++)
            {
                float currentYPos = 0f;
                currentXPos += xs[x] ? 2f : 6f;
                for (int y = 0; y < size; y++)
                {
                    Debug.Log($"Tile at ({x}, {y}) has ID of {MapLayout[x, y].GetID()}. N: {MapLayout[x, y].North}, E: {MapLayout[x, y].East}, S: {MapLayout[x, y].South}, W: {MapLayout[x, y].West}");

                    currentYPos += ys[y] ? 2f : 6f;
                    GameObject o;
                    if (MapLayout[x, y].GetID() == -1)
                        o = Instantiate(CCRooms[0]);
                    else
                        o = GetObject(MapLayout[x, y].GetID());
                    o.transform.position = new Vector3(currentXPos, 2, currentYPos);
                    currentYPos += ys[y] ? 2f : 6f;
                }
                currentXPos += xs[x] ? 2f : 6f;
            }
        }

        private GameObject ObjectRotate0(GameObject obj)
        {
            GameObject o = Instantiate(obj);
            return o;
        }
        private GameObject ObjectRotate90(GameObject obj)
        {
            GameObject o = Instantiate(obj);
            o.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            return o;
        }
        private GameObject ObjectRotate180(GameObject obj)
        {
            GameObject o = Instantiate(obj);
            o.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            return o;
        }
        private GameObject ObjectRotate270(GameObject obj)
        {
            GameObject o = Instantiate(obj);
            o.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            return o;
        }
        private GameObject GetObject(int id)
        {
            switch (id)
            {
                case 00:
                    return ObjectRotate0(CCCoridoors[0]);
                case 01:
                    return ObjectRotate90(CCCoridoors[0]);
                case 02:
                    return ObjectRotate180(CCCoridoors[0]);
                case 03:
                    return ObjectRotate270(CCCoridoors[0]);
                case 04:
                    return ObjectRotate0(CCCoridoors[1]);
                case 05:
                    return ObjectRotate90(CCCoridoors[1]);
                case 06:
                    return ObjectRotate180(CCCoridoors[1]);
                case 07:
                    return ObjectRotate270(CCCoridoors[1]);
                case 08:
                    return ObjectRotate0(CCCoridoors[2]);
                case 09:
                    return ObjectRotate90(CCCoridoors[2]);
                case 10:
                    return ObjectRotate180(CCCoridoors[2]);
                case 11:
                    return ObjectRotate270(CCCoridoors[2]);
                case 12:
                    return ObjectRotate0(CCCoridoors[3]);
                case 13:
                    return ObjectRotate90(CCCoridoors[3]);
                case 14:
                    return ObjectRotate180(CCCoridoors[3]);
                case 15:
                    return ObjectRotate270(CCCoridoors[3]);
                case 16:
                    return ObjectRotate0(CCCoridoors[4]);
                case 17:
                    return ObjectRotate90(CCCoridoors[4]);
                case 18:
                    return ObjectRotate0(CCCoridoors[5]);
                case 19:
                    return ObjectRotate90(CCCoridoors[5]);
                case 20:
                    return ObjectRotate0(CCCoridoors[6]);
                case 21:
                    return ObjectRotate90(CCCoridoors[6]);
                case 22:
                    return ObjectRotate0(CCCoridoors[7]);
                case 23:
                    return ObjectRotate90(CCCoridoors[7]);
                case 24:
                    return ObjectRotate0(CCIntersections[0]);
                case 25:
                    return ObjectRotate90(CCIntersections[0]);
                case 26:
                    return ObjectRotate180(CCIntersections[0]);
                case 27:
                    return ObjectRotate270(CCIntersections[0]);
                case 28:
                    return ObjectRotate0(CCIntersections[1]);
                case 29:
                    return ObjectRotate90(CCIntersections[1]);
                case 30:
                    return ObjectRotate180(CCIntersections[1]);
                case 31:
                    return ObjectRotate270(CCIntersections[1]);
                case 32:
                    return ObjectRotate0(CCIntersections[2]);
                case 33:
                    return ObjectRotate0(CCRooms[0]);
                case 34:
                    return ObjectRotate90(CCRooms[0]);
                case 35:
                    return ObjectRotate180(CCRooms[0]);
                case 36:
                    return ObjectRotate270(CCRooms[0]);
                case 37:
                    return ObjectRotate0(CCRooms[1]);
                case 38:
                    return ObjectRotate90(CCRooms[1]);
                case 39:
                    return ObjectRotate180(CCRooms[1]);
                case 40:
                    return ObjectRotate270(CCRooms[1]);
                case 41:
                    return ObjectRotate0(CCRooms[2]);
                case 42:
                    return ObjectRotate90(CCRooms[2]);
                case 43:
                    return ObjectRotate0(CCRooms[3]);
                case 44:
                    return ObjectRotate90(CCRooms[3]);
                case 45:
                    return ObjectRotate180(CCRooms[3]);
                case 46:
                    return ObjectRotate270(CCRooms[3]);
                case 47:
                    return ObjectRotate0(CCRooms[4]);
            }
            return null;
        }
    }
}