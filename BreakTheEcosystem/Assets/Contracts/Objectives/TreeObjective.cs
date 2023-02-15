using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTE.Contracts
{
    public class TreeObjective : Objective
    {
        public int Trees { get; private set; }
        public TreeObjective(int trees) : base(ObjectiveType.Tree)
        {
            Trees = trees;
        }
    }
}