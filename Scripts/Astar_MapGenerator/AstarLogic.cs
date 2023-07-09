using System.Collections.Generic;
using UnityEngine;

public class AstarLogic
{
    public class AStarNode
    {
        public AStarNode ParentNode;

        public int x, y, G, H;

        public int F
        {
            get
            {
                return G + H;
            }
        }

        public AStarNode(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
    }

    private AStarNode[,] nodes;
    private AStarNode startNode;
    private AStarNode endNode;
    private AStarNode currentNode;
    private List<AStarNode> openList;
    private List<AStarNode> closeList;

    //private StringBuilder sb = new StringBuilder();

    public void Init(int maxRaw, int maxCol, int startX, int startY, int targetX, int targetY, out List<AStarNode> toTargetList)
    {
        this.openList = new List<AStarNode>();
        this.closeList = new List<AStarNode>();
        //Debug.LogFormat("Index : {0},{1}", targetY, targetX);

        this.CreateNode(maxRaw, maxCol);
        this.SetTargetLocation(startX, startY, targetX, targetY);
        this.PathFinding();
        toTargetList = this.closeList;
    }

    //Creates a node array based on the size of x and y.
    public void CreateNode(int maxRaw, int maxCol)
    {
        this.nodes = new AStarNode[maxRaw, maxCol];

        for (int y = 0; y < maxRaw; y++)
        {
            for (int x = 0; x < maxCol; x++)
            {
                this.nodes[y, x] = new AStarNode(y, x);
                //this.sb.Append(string.Format("<color=white>{0},{1}\t</color>", this.nodes[y, x].y, this.nodes[y, x].x));
            }
        }
        //Debug.Log("====Created Node List====");
        //Debug.Log(this.sb.ToString());
        //this.sb.Clear();
    }

    // Sets the start and target locations.
    public void SetTargetLocation(int startX, int startY, int targetX, int targetY)
    {
        this.startNode = this.nodes[startY, startX];
        this.endNode = this.nodes[targetY, targetX];
    }

    //Pathfinding algorithm.
    public List<AStarNode> PathFinding()
    {
        this.openList.Clear();
        this.closeList.Clear();
        this.openList.Add(this.startNode);

        while (this.openList.Count > 0)
        {
            this.currentNode = this.openList[0];
            for (int i = 1; i < this.openList.Count; ++i)
            {
                // Find the path with the highest weight among the available paths.
                if (this.openList[i].F >= this.currentNode.F && this.openList[i].H < this.currentNode.H)
                {
                    this.currentNode = this.openList[i];
                }
            }

            this.openList.Remove(this.currentNode);
            this.closeList.Add(this.currentNode);

            if (this.currentNode == this.endNode)
            {
                return this.closeList;
            }

            OpenListAdd(this.currentNode.x, this.currentNode.y + 1);
            OpenListAdd(this.currentNode.x + 1, this.currentNode.y);
            OpenListAdd(this.currentNode.x, this.currentNode.y - 1);
            OpenListAdd(this.currentNode.x - 1, this.currentNode.y);
        }
        return this.closeList;
    }

    void OpenListAdd(int checkX, int checkY)
    {
        if (checkX < 0 || checkX >= this.nodes.GetLength(1) || checkY < 0 || checkY >= this.nodes.GetLength(0))
            return;

        if (this.closeList.Contains(this.nodes[checkY, checkX]))
            return;

        AStarNode NeighborNode = this.nodes[checkY, checkX];
        int MoveCost = this.currentNode.G + (this.currentNode.x - checkX == 0 || this.currentNode.y - checkY == 0 ? 10 : 14);


        if (MoveCost < NeighborNode.G || !this.openList.Contains(NeighborNode))
        {
            NeighborNode.G = MoveCost;
            NeighborNode.H = (Mathf.Abs(NeighborNode.x - this.endNode.x) + Mathf.Abs(NeighborNode.y - this.endNode.y)) * 10;
            NeighborNode.ParentNode = this.currentNode;

            this.openList.Add(NeighborNode);
        }
    }
}