using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
    public class Generation : MonoBehaviour
    {
        [Header("Call Centre")]
        [Header("Stats")]
        [SerializeField] private int Size = 10;
        [SerializeField] [Range(0, 1)] private float ConnectingChance = 0.45f;
        
        [Header("Blocks")]
        [SerializeField] private GameObject[] CCCoridoors;
        [SerializeField] private GameObject[] CCIntersections;
        [SerializeField] private GameObject[] CCRooms;
        [SerializeField] private GameObject[] CCOutsideWalls;

        [Header("Other")]
        [SerializeField] private NavMeshSurface NavMesh;
        [SerializeField] private Material[] WallColors;
        [SerializeField] private Material BossRoomColor;
        [SerializeField] private Material CorridorColor;
        [SerializeField] private GameObject EndTrigger;

        [Header("People")]
        [SerializeField] private GameObject[] Bystanders;

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
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                SceneManager.LoadScene(2);
        }

        private void GenerateCC(int size)
        {
            TileInfo[,] MapLayout = new TileInfo[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    MapLayout[x, y] = new TileInfo();
                    MapLayout[x, y].X = x;
                    MapLayout[x, y].Y = y;
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
            availableY.Remove(size - 1);
            availableX.Remove(0);
            availableX.Remove(size - 1);

            int bossRoomX = availableX[Random.Range(0, availableX.Count)];
            int bossRoomY = availableY[Random.Range(0, availableY.Count)];
            availableX.Remove(bossRoomX);
            availableY.Remove(bossRoomY);

            for (int i = 0; i < 2; i++)
            {
                int index = Random.Range(0, availableX.Count);
                constructX.Add(availableX[index]);
                int value = availableX[index];
                if (availableX.Contains(value + 1))
                    availableX.Remove(value + 1);
                if (availableX.Contains(value - 1))
                    availableX.Remove(value - 1);
                availableX.Remove(value);
            }
            for (int i = 0; i < 2; i++)
            {
                int index = Random.Range(0, availableY.Count);
                constructY.Add(availableY[index]);
                int value = availableY[index];
                if (availableY.Contains(value + 1))
                    availableY.Remove(value + 1);
                if (availableY.Contains(value - 1))
                    availableY.Remove(value - 1);
                availableY.Remove(value);
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
                MapLayout[0, cy].Type = BlockType.Corridor;
                /*
                if (MapLayout[0, cy].Type == BlockType.Corridor)
                    MapLayout[0, cy].Type = BlockType.Intersection;
                else
                    MapLayout[0, cy].Type = BlockType.Corridor;
                */
                MapLayout[0, cy].East = true;
                for (int i = 1; i < size - 1; i++)
                {
                    MapLayout[i, cy].Connected = true;
                    MapLayout[i, cy].Type = BlockType.Corridor;
                    /*
                    if (MapLayout[i, cy].Type == BlockType.Corridor)
                        MapLayout[i, cy].Type = BlockType.Intersection;
                    else
                        MapLayout[i, cy].Type = BlockType.Corridor;
                    */
                    MapLayout[i, cy].East = true;
                    MapLayout[i, cy].West = true;
                }
                MapLayout[size - 1, cy].Connected = true;
                MapLayout[size - 1, cy].Type = BlockType.Corridor;
                /*
                if (MapLayout[size - 1, cy].Type == BlockType.Corridor)
                    MapLayout[size - 1, cy].Type = BlockType.Intersection;
                else
                    MapLayout[size - 1, cy].Type = BlockType.Corridor;
                */
                MapLayout[size - 1, cy].West = true;
            }

            Debug.Log($"{bossRoomX}, {bossRoomY}");
            MapLayout[bossRoomX, bossRoomY].Ignore = true;
            List<Direction> bossDoors = new List<Direction>();
            if (bossRoomX > 0)
                bossDoors.Add(Direction.West);
            if (bossRoomX < size - 1)
                bossDoors.Add(Direction.East);
            if (bossRoomY > 0)
                bossDoors.Add(Direction.South);
            if (bossRoomY < size - 1)
                bossDoors.Add(Direction.North);
            Direction doorDirection = bossDoors[Random.Range(0, bossDoors.Count)];
            switch (doorDirection)
            {
                case Direction.North:
                    MapLayout[bossRoomX, bossRoomY].North = true;
                    if (MapLayout[bossRoomX, bossRoomY + 1].Type == BlockType.Corridor)
                        MapLayout[bossRoomX, bossRoomY + 1].Right = true;
                    else
                        MapLayout[bossRoomX, bossRoomY + 1].South = true;
                    break;
                case Direction.East:
                    MapLayout[bossRoomX, bossRoomY].East = true;
                    if (MapLayout[bossRoomX + 1, bossRoomY].Type == BlockType.Corridor)
                        MapLayout[bossRoomX + 1, bossRoomY].Left = true;
                    else
                        MapLayout[bossRoomX + 1, bossRoomY].West = true;
                    break;
                case Direction.South:
                    MapLayout[bossRoomX, bossRoomY].South = true;
                    if (MapLayout[bossRoomX, bossRoomY - 1].Type == BlockType.Corridor)
                        MapLayout[bossRoomX, bossRoomY - 1].Left = true;
                    else
                        MapLayout[bossRoomX, bossRoomY - 1].North = true;
                    break;
                case Direction.West:
                    MapLayout[bossRoomX, bossRoomY].West = true;
                    if (MapLayout[bossRoomX - 1, bossRoomY].Type == BlockType.Corridor)
                        MapLayout[bossRoomX - 1, bossRoomY].Right = true;
                    else
                        MapLayout[bossRoomX - 1, bossRoomY].East = true;
                    break;
            }

            Prims(size, ref MapLayout);
            RandomizeConnections(size, ref MapLayout);
            AddEntrance(size, ref MapLayout, out int ex);
            BuildCC(MapLayout, size, XIsCorrdidor, YIsCorrdidor, ex);

            NavMesh.BuildNavMesh();

            SpawnPeople(size, XIsCorrdidor, YIsCorrdidor);
        }
        private void Prims(int size, ref TileInfo[,] MapLayout)
        {
            List<TileInfo> tilesToWork = new List<TileInfo>();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (ConnectedInRange(size, MapLayout, x, y) && MapLayout[x, y].Connected == false && MapLayout[x, y].Ignore == false)
                        tilesToWork.Add(MapLayout[x, y]);
                }
            }

            while(tilesToWork.Count > 0)
            {
                TileInfo currentTile = tilesToWork[Random.Range(0, tilesToWork.Count)];
                List<Direction> connectedNeighbours = new List<Direction>();
                if (currentTile.X != 0)
                    if (MapLayout[currentTile.X - 1, currentTile.Y].Connected)
                        connectedNeighbours.Add(Direction.West);
                if (currentTile.X != size - 1)
                    if (MapLayout[currentTile.X + 1, currentTile.Y].Connected)
                        connectedNeighbours.Add(Direction.East);
                if (currentTile.Y != 0)
                    if (MapLayout[currentTile.X, currentTile.Y - 1].Connected)
                        connectedNeighbours.Add(Direction.South);
                if (currentTile.Y != size - 1)
                    if (MapLayout[currentTile.X, currentTile.Y + 1].Connected)
                        connectedNeighbours.Add(Direction.North);

                int x = currentTile.X;
                int y = currentTile.Y;
                switch (connectedNeighbours[Random.Range(0, connectedNeighbours.Count)])
                {
                    case Direction.North:
                        currentTile.North = true;
                        if (MapLayout[x, y + 1].Type == BlockType.Corridor)
                            MapLayout[x, y + 1].Right = true;
                        else
                            MapLayout[x, y + 1].South = true;
                        break;
                    case Direction.East:
                        currentTile.East = true;
                        if (MapLayout[x + 1, y].Type == BlockType.Corridor)
                            MapLayout[x + 1, y].Left = true;
                        else
                            MapLayout[x + 1, y].West = true;
                        break;
                    case Direction.South:
                        currentTile.South = true;
                        if (MapLayout[x, y - 1].Type == BlockType.Corridor)
                            MapLayout[x, y - 1].Left = true;
                        else
                            MapLayout[x, y - 1].North = true;
                        break;
                    case Direction.West:
                        currentTile.West = true;
                        if (MapLayout[x - 1, y].Type == BlockType.Corridor)
                            MapLayout[x - 1, y].Right = true;
                        else
                            MapLayout[x - 1, y].East = true;
                        break;
                }
                currentTile.Connected = true;

                if (currentTile.X != 0)
                    if (MapLayout[currentTile.X - 1, currentTile.Y].Connected == false && MapLayout[currentTile.X - 1, currentTile.Y].Ignore == false && tilesToWork.Contains(MapLayout[currentTile.X - 1, currentTile.Y]) == false)
                        tilesToWork.Add(MapLayout[currentTile.X - 1, currentTile.Y]);
                if (currentTile.X != size - 1)
                    if (MapLayout[currentTile.X + 1, currentTile.Y].Connected == false && MapLayout[currentTile.X + 1, currentTile.Y].Ignore == false && tilesToWork.Contains(MapLayout[currentTile.X + 1, currentTile.Y]) == false)
                        tilesToWork.Add(MapLayout[currentTile.X + 1, currentTile.Y]);
                if (currentTile.Y != 0)
                    if (MapLayout[currentTile.X, currentTile.Y - 1].Connected == false && MapLayout[currentTile.X, currentTile.Y - 1].Ignore == false && tilesToWork.Contains(MapLayout[currentTile.X, currentTile.Y - 1]) == false)
                        tilesToWork.Add(MapLayout[currentTile.X, currentTile.Y - 1]);
                if (currentTile.Y != size - 1)
                    if (MapLayout[currentTile.X, currentTile.Y + 1].Connected == false && MapLayout[currentTile.X, currentTile.Y + 1].Ignore == false && tilesToWork.Contains(MapLayout[currentTile.X, currentTile.Y + 1]) == false)
                        tilesToWork.Add(MapLayout[currentTile.X, currentTile.Y + 1]);

                tilesToWork.Remove(currentTile);
            }
        }
        private bool ConnectedInRange(int size, TileInfo[,] MapLayout, int x, int y)
        {
            if(x != 0)
                if (MapLayout[x - 1, y].Connected)
                    return true;
            if (x != size - 1)
                if (MapLayout[x + 1, y].Connected)
                    return true;
            if (y != 0)
                if (MapLayout[x, y - 1].Connected)
                    return true;
            if (y != size - 1)
                if (MapLayout[x, y + 1].Connected)
                    return true;
            return false;
        }
        private void RandomizeConnections(int size, ref TileInfo[,] MapLayout)
        {

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (MapLayout[x, y].Type == BlockType.Room && MapLayout[x, y].Ignore == false)
                    {
                        if (y != 0
                            && Random.Range(0, 1000) <= ConnectingChance * 1000)
                        {
                            if (!MapLayout[x, y - 1].Ignore)
                            {
                                MapLayout[x, y].South = true;
                                if (MapLayout[x, y - 1].Type == BlockType.Corridor)
                                    MapLayout[x, y - 1].Left = true;
                                else
                                    MapLayout[x, y - 1].North = true;
                                if (MapLayout[x, y].Connected || MapLayout[x, y - 1].Connected)
                                {
                                    MapLayout[x, y].Connected = true;
                                    MapLayout[x, y - 1].Connected = true;
                                }
                            }
                        }
                        if (y != size - 1
                            && Random.Range(0, 1000) <= ConnectingChance * 1000)
                        {
                            if (!MapLayout[x, y + 1].Ignore)
                            {
                                MapLayout[x, y].North = true;
                                if (MapLayout[x, y + 1].Type == BlockType.Corridor)
                                    MapLayout[x, y + 1].Right = true;
                                else
                                    MapLayout[x, y + 1].South = true;
                                if (MapLayout[x, y].Connected || MapLayout[x, y + 1].Connected)
                                {
                                    MapLayout[x, y].Connected = true;
                                    MapLayout[x, y + 1].Connected = true;
                                }
                            }
                        }
                        if (x != 0
                            && Random.Range(0, 1000) <= ConnectingChance * 1000)
                        {
                            if (!MapLayout[x - 1, y].Ignore)
                            {
                                MapLayout[x, y].West = true;
                                if (MapLayout[x - 1, y].Type == BlockType.Corridor)
                                    MapLayout[x - 1, y].Right = true;
                                else
                                    MapLayout[x - 1, y].East = true;
                                if (MapLayout[x, y].Connected || MapLayout[x - 1, y].Connected)
                                {
                                    MapLayout[x, y].Connected = true;
                                    MapLayout[x - 1, y].Connected = true;
                                }
                            }
                        }
                        if (x != size - 1
                            && Random.Range(0, 1000) <= ConnectingChance * 1000)
                        {
                            if (!MapLayout[x + 1, y].Ignore)
                            { 
                                MapLayout[x, y].East = true;
                            if (MapLayout[x + 1, y].Type == BlockType.Corridor)
                                MapLayout[x + 1, y].Left = true;
                            else
                                MapLayout[x + 1, y].West = true;
                                if (MapLayout[x, y].Connected || MapLayout[x + 1, y].Connected)
                                {
                                    MapLayout[x, y].Connected = true;
                                    MapLayout[x + 1, y].Connected = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void AddEntrance(int size, ref TileInfo[,] MapLayout, out int x)
        {
            List<int> positions = new List<int>();
            for (int i = 0; i < size; i++)
            {
                if (MapLayout[i, 0].Type == BlockType.Room)
                    positions.Add(i);
            }
            int pos = positions[Random.Range(0, positions.Count)];
            MapLayout[pos, 0].South = true;
            x = pos;
        }
        private void BuildCC(TileInfo[,] MapLayout, int size, bool[] xs, bool[] ys, int ex)
        {
            float currentXPos = -34f;
            for (int x = 0; x < size; x++)
            {
                float currentYPos = 5f;
                currentXPos += xs[x] ? 2f : 6f;
                for (int y = 0; y < size; y++)
                {
                    MapLayout[x, y].GetID(); // This somehow fixes a bug?

                    //Debug.Log($"Tile at ({x}, {y}) is type {MapLayout[x, y].Type} has ID of {MapLayout[x, y].AbsoluteGetID()}. L: {MapLayout[x, y].Left}, R: {MapLayout[x, y].Right}. N: {MapLayout[x, y].North}, E: {MapLayout[x, y].East}, S: {MapLayout[x, y].South}, W: {MapLayout[x, y].West}");

                    // Create tile

                    currentYPos += ys[y] ? 2f : 6f;
                    GameObject o;
                    if (MapLayout[x, y].GetID() == -1)
                        o = Instantiate(CCRooms[0]);
                    else
                        o = GetObject(MapLayout[x, y].GetID());
                    o.transform.position = new Vector3(currentXPos, 2f, currentYPos);
                    if (MapLayout[x, y].Ignore)
                        o.GetComponent<Renderer>().material = BossRoomColor;
                    else if(MapLayout[x, y].Type == BlockType.Corridor || MapLayout[x, y].Type == BlockType.Intersection)
                        o.GetComponent<Renderer>().material = CorridorColor;
                    else
                        o.GetComponent<Renderer>().material = WallColors[Random.Range(0, WallColors.Length)];

                    if(x == ex && y == 0)
                    {
                        GameObject end = Instantiate(EndTrigger);
                        end.transform.position = new Vector3(currentXPos, 1.5f, currentYPos - 6f);
                        GameObject wall = Instantiate(CCOutsideWalls[1]);
                        wall.transform.position = new Vector3(currentXPos, 2f, currentYPos - 6.05f);
                    }
                    else if(y == 0)
                    {
                        if(MapLayout[x, y].Type == BlockType.Room)
                        {
                            GameObject wall = Instantiate(CCOutsideWalls[0]);
                            wall.transform.position = new Vector3(currentXPos, 2f, currentYPos - 6.05f);
                        }
                        else
                        {
                            GameObject wall = Instantiate(CCOutsideWalls[2]);
                            wall.transform.position = new Vector3(currentXPos, 2f, currentYPos - 6.05f);
                        }
                    }
                    else if(y == size - 1)
                    {
                        if (MapLayout[x, y].Type == BlockType.Room)
                        {
                            GameObject wall = Instantiate(CCOutsideWalls[0]);
                            wall.transform.position = new Vector3(currentXPos, 2f, currentYPos + 6.05f);
                        }
                        else
                        {
                            GameObject wall = Instantiate(CCOutsideWalls[2]);
                            wall.transform.position = new Vector3(currentXPos, 2f, currentYPos + 6.05f);
                        }
                    }

                    if(x == 0)
                    {
                        if (MapLayout[x, y].Type == BlockType.Room)
                        {
                            GameObject wall = Instantiate(CCOutsideWalls[0]);
                            wall.transform.position = new Vector3(currentXPos - 6.05f, 2f, currentYPos);
                            wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
                        }
                        else
                        {
                            GameObject wall = Instantiate(CCOutsideWalls[2]);
                            wall.transform.position = new Vector3(currentXPos - 6.05f, 2f, currentYPos);
                            wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
                        }
                    }
                    else if (x == size - 1)
                    {
                        if (MapLayout[x, y].Type == BlockType.Room)
                        {
                            GameObject wall = Instantiate(CCOutsideWalls[0]);
                            wall.transform.position = new Vector3(currentXPos + 6.05f, 2f, currentYPos);
                            wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
                        }
                        else
                        {
                            GameObject wall = Instantiate(CCOutsideWalls[2]);
                            wall.transform.position = new Vector3(currentXPos + 6.05f, 2f, currentYPos);
                            wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
                        }
                    }

                    currentYPos += ys[y] ? 2f : 6f;
                }
                currentXPos += xs[x] ? 2f : 6f;
            }
        }
        private void SpawnPeople(int size, bool[] xs, bool[] ys)
        {
            float currentXPos = -34f;
            for (int x = 0; x < size; x++)
            {
                float currentYPos = 5f;
                currentXPos += xs[x] ? 2f : 6f;
                for (int y = 0; y < size; y++)
                {
                    currentYPos += ys[y] ? 2f : 6f;

                    // Spawn people

                    if (ys[y])
                    {
                        SpawnBystander(new Vector3(currentXPos, 1.5f, currentYPos));
                    }
                    else
                    {
                        SpawnBystander(new Vector3(currentXPos, 1.5f, currentYPos));
                    }

                    currentYPos += ys[y] ? 2f : 6f;
                }
                currentXPos += xs[x] ? 2f : 6f;
            }
        }
        private void SpawnBystander(Vector3 position)
        {
            GameObject o = Instantiate(Bystanders[Random.Range(0, Bystanders.Length)]);
            o.transform.position = position;
        }

        private GameObject ObjectRotate0(GameObject obj)
        {
            GameObject o = Instantiate(obj, transform);
            return o;
        }
        private GameObject ObjectRotate90(GameObject obj)
        {
            GameObject o = Instantiate(obj, transform);
            o.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            return o;
        }
        private GameObject ObjectRotate180(GameObject obj)
        {
            GameObject o = Instantiate(obj, transform);
            o.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            return o;
        }
        private GameObject ObjectRotate270(GameObject obj)
        {
            GameObject o = Instantiate(obj, transform);
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