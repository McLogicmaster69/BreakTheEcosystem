using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.BDLC.CallCentre
{
    public enum BlockType
    {
        Corridor,
        Intersection,
        Room
    }
    public class TileInfo
    {
        // General
        public bool Connected = false;
        public BlockType Type = BlockType.Room;
        public bool North = false;
        public bool East = false;
        public bool South = false;
        public bool West = false;
        
        // Corridor
        public bool Left = false;
        public bool Right = false;

        public int GetID()
        {
            switch (Type)
            {
                case BlockType.Corridor:
                    return CorridorID();

                case BlockType.Intersection:
                    return IntersectionID();

                case BlockType.Room:
                    return RoomID();
            }
            return -1;
        }

        private int CorridorID()
        {
            if (Left)
            {
                if (Right)
                    return CorridorLeftRight();
                else
                    return CorridorLeftNoRight();
            }
            else
            {
                if (Right)
                    return CorridorNoLeftRight();
                else
                    return CorridorNoLeftNoRight();
            }
        }
        private int CorridorNoLeftNoRight()
        {
            if (North)
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return 32;
                        }
                        else
                        {
                            return 28;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 31;
                        }
                        else
                        {
                            return 24;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return 30;
                        }
                        else
                        {
                            return 16;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 27;
                        }
                        else
                        {
                            return 00;
                        }
                    }
                }
            }
            else
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return 29;
                        }
                        else
                        {
                            return 25;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 17;
                        }
                        else
                        {
                            return 01;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return 26;
                        }
                        else
                        {
                            return 02;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 03;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
        }
        private int CorridorLeftNoRight()
        {
            if (North)
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return 18;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return 04;
                        }
                    }
                }
            }
            else
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 19;
                        }
                        else
                        {
                            return 05;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return 06;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 07;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
        }
        private int CorridorNoLeftRight()
        {
            if (North)
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return 20;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return 08;
                        }
                    }
                }
            }
            else
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 21;
                        }
                        else
                        {
                            return 09;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return 10;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 11;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
        }
        private int CorridorLeftRight()
        {
            if (North)
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return 22;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return 12;
                        }
                    }
                }
            }
            else
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 23;
                        }
                        else
                        {
                            return 13;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return -1;
                        }
                        else
                        {
                            return 14;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 15;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
        }
        private int RoomID()
        {
            if (North)
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return 47;
                        }
                        else
                        {
                            return 43;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 46;
                        }
                        else
                        {
                            return 37;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return 45;
                        }
                        else
                        {
                            return 41;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 40;
                        }
                        else
                        {
                            return 33;
                        }
                    }
                }
            }
            else
            {
                if (East)
                {
                    if (South)
                    {
                        if (West)
                        {
                            return 44;
                        }
                        else
                        {
                            return 38;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 42;
                        }
                        else
                        {
                            return 34;
                        }
                    }
                }
                else
                {
                    if (South)
                    {
                        if (West)
                        {
                            return 39;
                        }
                        else
                        {
                            return 35;
                        }
                    }
                    else
                    {
                        if (West)
                        {
                            return 36;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
        }
        private int IntersectionID()
        {
            return CorridorNoLeftNoRight();
        }
    }
}