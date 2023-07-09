using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AstarLogic;

public class MapGenerator : MonoBehaviour
{
    public int maxRow;
    public int maxCol;
    public int mapDist = 64;

    public List<GameObject> normalRoomList = new List<GameObject>();
    public List<GameObject> wholeRoomList = new List<GameObject>();

    [SerializeField] private GameObject[] forrestNormalRoomArr;
    [SerializeField] private GameObject[] graveNormalRoomArr;
    [SerializeField] private GameObject[] templeNormalRoomArr;
    [SerializeField] private GameObject[] heartOfDevilNormalRoomArr;
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject[] BossRoomArr;
    [SerializeField] private GameObject safeHouseRoom;
    [SerializeField] private GameObject hiddemRoom;

    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject bossPortal;
    [SerializeField] private GameObject safeRoomPortal;

    public GameObject nextStagePortal;

    string[] stage1Arr = { "2X3", "2X4", "2X5", "3X2", "3X3", "3X4", "4X2", "4X3", "5X2" };
    string[] stage2Arr = { "2X4", "2x5", "3X3", "3X4", "3X5", "4X2", "4X3", "5X2", "5X3" };
    string[] stage3Arr = { "2X5", "2X6", "3X3", "3X4", "3X5", "4X3", "4X4", "5X2", "5X3" };
    string[] stage4Arr = { "2X6", "3X4", "3X5", "4X3", "4X4", "4X5", "5X3", "5X4", "6X2" };

    private int[,] mapLocation;
    private Vector2[,] mapVectorPos;
    public Vector2 startRoomPos;
    public Vector2 BossRoomPos;
    public Vector2 safeHousePos;
    public Vector2 hiddemRoomPos;

    //private StringBuilder sb = new StringBuilder();

    public void Init()
    {
        // Select random room array for each stage
        this.SetRolCol();

        // Declare a 2D array to store map information
        this.mapLocation = new int[this.maxRow, this.maxCol];

        // Create an array to store vector information of the map
        this.mapVectorPos = new Vector2[this.maxRow, this.maxCol];

        // Create an array to store the entire map
        this.wholeRoomList.Clear();

        // Create a list to store only normal rooms
        this.normalRoomList.Clear();

        // Create a vector list
        this.MakeVectorList();

        // Create random points
        this.MakeRanLocation();

        // Save the created random points in a temporary list
        var tempList = this.MakeTempList();

        // Find the two points with the furthest distance in the list and create a Vector2 list in descending order
        var furthestPoints = this.FindFurthestPoints(tempList);

        // Determine the positions of the start room, boss room, safe house, and hidden room
        this.MakeMajorRoomsPos(furthestPoints);

        // Create and allocate major rooms
        this.MakeMajorRooms();

        // Initialize the temporary list
        tempList.Clear();
        tempList.Add(this.startRoomPos);
        tempList.Add(this.BossRoomPos);
        tempList.Add(this.safeHousePos);

        // Initialize the 2D map array with 1 for the rooms that exist and 0 for others
        this.InitNoneExistRooms(tempList);

        int startY = 0;
        int startX = 0;

        int bossY = 0;
        int bossX = 0;

        int safeY = 0;
        int safeX = 0;

        for (int i = 0; i < this.maxRow; i++)
        {
            for (int j = 0; j < this.maxCol; j++)
            {
                if (this.mapVectorPos[i, j] == this.startRoomPos)
                {
                    this.mapLocation[i, j] = 2;
                    startY = i;
                    startX = j;
                }
                else if (this.mapVectorPos[i, j] == this.BossRoomPos)
                {
                    this.mapLocation[i, j] = 4;
                    bossY = i;
                    bossX = j;
                }
                else if (this.mapVectorPos[i, j] == this.safeHousePos)
                {
                    this.mapLocation[i, j] = 3;
                    safeY = i;
                    safeX = j;
                }
            }
        }

        // Create an instance of the AstarLogic class
        AstarLogic astarLogic = new AstarLogic();
        // Create a list to store the path to the boss room
        List<AStarNode> toBossList = new List<AStarNode>();
        astarLogic.Init(this.maxRow, this.maxCol, startX, startY, bossX, bossY, out toBossList);
        List<AStarNode> toSafeHouseList = new List<AStarNode>();
        astarLogic.Init(this.maxRow, this.maxCol, safeX, safeY, startX, startY, out toSafeHouseList);


        // Create the maximum random number for normal room generation based on the current stage information
        var currentStage = InfoManager.instance.dungeonInfo.CurrentStageInfo;
        var maxRanNum = this.MakeMaxNumForNoramlRoom(currentStage);

        // Create normal rooms on the path to the boss room
        foreach (AStarNode node in toBossList)
        {
            var checkVec = new Vector2(node.x * this.mapDist, node.y * -this.mapDist);
            if (checkVec == this.startRoomPos || checkVec == this.BossRoomPos || checkVec == this.safeHousePos) continue;
            else
            {
                var go = default(GameObject);
                var ramdom =  new System.Random();
                var ran = ramdom.Next(0, maxRanNum);
                // Change the room list based on the current stage
                if (currentStage == 1) { go = GameObject.Instantiate(this.forrestNormalRoomArr[ran]); }
                else if (currentStage == 2) { go = GameObject.Instantiate(this.graveNormalRoomArr[ran]); }
                else if (currentStage == 3) { go = GameObject.Instantiate(this.templeNormalRoomArr[ran]); }
                else if (currentStage == 4) { go = GameObject.Instantiate(this.heartOfDevilNormalRoomArr[ran]); }
                // Set the position
                go.transform.position = new Vector3(node.x * this.mapDist, node.y * -this.mapDist, 1);
                // Mark the room in the 2D map array
                this.mapLocation[node.y, node.x] = 1;
                // Add the room to the normal room list
                this.normalRoomList.Add(go);
                this.wholeRoomList.Add(go);
            }
        }

        // Create normal rooms on the path from the safe house to the start room, excluding the start room
        foreach (AStarNode node in toSafeHouseList)
        {
            var checkVec = new Vector2(node.x * this.mapDist, node.y * -this.mapDist);
            if (this.mapLocation[node.y, node.x] != 0 || checkVec == this.startRoomPos || checkVec == this.BossRoomPos || checkVec == this.safeHousePos) continue;
            else
            {
                var go = default(GameObject);
                var ramdom = new System.Random();
                var ran = ramdom.Next(0, maxRanNum);
                // Change the room list based on the current stage
                if (currentStage == 1) { go = GameObject.Instantiate(this.forrestNormalRoomArr[ran]); }
                else if (currentStage == 2) { go = GameObject.Instantiate(this.graveNormalRoomArr[ran]); }
                else if (currentStage == 3) { go = GameObject.Instantiate(this.templeNormalRoomArr[ran]); }
                else if (currentStage == 4) { go = GameObject.Instantiate(this.heartOfDevilNormalRoomArr[ran]); }
                // Set the position
                go.transform.position = new Vector3(node.x * this.mapDist, node.y * -this.mapDist, 1);
                // Mark the room in the 2D map array
                this.mapLocation[node.y, node.x] = 1;
                // Add the room to the normal room list
                this.normalRoomList.Add(go);
                this.wholeRoomList.Add(go);
            }
        }
        // Create hidden rooms
        this.MakeHiddenRoom();
        // Create portals in the generated rooms
        this.MakePortal();
        //// Deactivate all rooms except the start room (comment this out for testing purposes)
        //this.DeActivePortalsAndRooms();
    }



    private void SetRolCol()
    {
        var stageNum = InfoManager.instance.dungeonInfo.CurrentStageInfo;
        var ranIndex = default(int);
        var rowCol = default(int[]);
        var random  = new System.Random();  
        switch (stageNum)
        {
            case 1:
                ranIndex = random.Next(0, this.stage1Arr.Length - 1);
                rowCol = Array.ConvertAll(this.stage1Arr[ranIndex].Split("X"), int.Parse);
                this.maxRow = rowCol[0];
                this.maxCol = rowCol[1];
                break;
            case 2:
                ranIndex = random.Next(0, this.stage2Arr.Length - 1);
                rowCol = Array.ConvertAll(this.stage1Arr[ranIndex].Split("X"), int.Parse);
                this.maxRow = rowCol[0];
                this.maxCol = rowCol[1];
                break;
            case 3:
                ranIndex = random.Next(0, this.stage3Arr.Length - 1);
                rowCol = Array.ConvertAll(this.stage1Arr[ranIndex].Split("X"), int.Parse);
                this.maxRow = rowCol[0];
                this.maxCol = rowCol[1];
                break;
            case 4:
                ranIndex = random.Next(0, this.stage4Arr.Length - 1);
                rowCol = Array.ConvertAll(this.stage1Arr[ranIndex].Split("X"), int.Parse);
                this.maxRow = rowCol[0];
                this.maxCol = rowCol[1];
                break;
        }
    }

    private void MakeVectorList()
    {
        int x = 0;
        int y = 0;
        for (int i = 0; i < this.maxRow; i++)
        {
            for (int j = 0; j < this.maxCol; j++)
            {
                this.mapVectorPos[i, j] = new Vector2(x, y);
                x += this.mapDist;
            }
            y -= this.mapDist;
            x = 0;
        }
    }

    private void MakeRanLocation()
    {
        int count = 0;
        int maxCount = (this.maxCol * this.maxRow + 1) / 2;
        bool[,] selectedLocations = new bool[this.maxRow, this.maxCol];
        var randomX = new System.Random();
        var randomY = new System.Random();  
        while (count < maxCount)
        {
            int y = randomX.Next(0, this.maxRow);
            int x = randomY.Next(0, this.maxCol);

            if (!selectedLocations[y, x])
            {
                selectedLocations[y, x] = true;
                this.mapLocation[y, x] = 1;
                count++;
            }
        }
    }

    private List<Vector2> MakeTempList()
    {
        List<Vector2> temp = new List<Vector2>();

        for (int i = 0; i < this.maxRow; i++)
        {
            for (int j = 0; j < this.maxCol; j++)
            {
                if (this.mapLocation[i, j] == 1)
                {
                    temp.Add(new Vector2(j * this.mapDist, i * -this.mapDist));
                }

            }
        }
        return temp;
    }

    private List<Vector2> FindFurthestPoints(List<Vector2> points)
    {
        // Generate all possible pairs of points from the list
        var pairs = from p1 in points
                    from p2 in points
                    where p1 != p2
                    select new { p1, p2 };

        // Calculate the distance for each pair
        var distances = pairs.Select(pair => new { pair.p1, pair.p2, distance = Vector2.Distance(pair.p1, pair.p2) });

        // Find the two points with the furthest distance
        var furthestPoints = distances.OrderByDescending(pair => pair.distance).First();
        var top2List = new List<Vector2> { furthestPoints.p1, furthestPoints.p2 };

        // Create a new list
        var sortedList = new List<Vector2>();
        sortedList.Add(top2List[0]);
        sortedList.Add(top2List[1]);
        sortedList.AddRange(points.Except(top2List).OrderBy(p => Vector2.Distance(top2List[0], p)));
        return sortedList;
    }


    private void MakeMajorRoomsPos(List<Vector2> FurtherList)
    {
        // Start room Vector2 position
        this.startRoomPos = FurtherList[0];

        // Boss room Vector2 position
        this.BossRoomPos = FurtherList[1];

        // Safe house Vector2 position
        if (this.maxCol * this.maxRow <= 6)
        {
            this.safeHousePos = FurtherList[FurtherList.Count - 1];
        }
        else
        {
            this.safeHousePos = FurtherList[FurtherList.Count - 2];
        }
    }

    private void MakeMajorRooms()
    {
        // Create the start room
        var startRoomGo = GameObject.Instantiate(this.startRoom);
        startRoomGo.transform.position = new Vector3(this.startRoomPos.x, this.startRoomPos.y, 1);
        this.wholeRoomList.Add(startRoomGo);

        // Create the boss room
        var bossRoomIndex = InfoManager.instance.dungeonInfo.CurrentStageInfo - 1;

        var bossRoomGo = GameObject.Instantiate(this.BossRoomArr[bossRoomIndex]);
        bossRoomGo.transform.position = new Vector3(this.BossRoomPos.x, this.BossRoomPos.y, 1);
        this.wholeRoomList.Add(bossRoomGo);

        var bossComp = bossRoomGo.GetComponentInChildren<IDungeonBossHandler>();
        bossComp.Intializing();

        // Create the safe house
        var safeHouseGo = GameObject.Instantiate(this.safeHouseRoom);
        safeHouseGo.transform.position = new Vector3(this.safeHousePos.x, this.safeHousePos.y, 1);
        this.wholeRoomList.Add(safeHouseGo);
    }

    private void InitNoneExistRooms(List<Vector2> tempList)
    {
        for (int i = 0; i < this.maxRow; i++)
        {
            for (int j = 0; j < this.maxCol; j++)
            {
                if (this.mapLocation[i, j] == 1)
                {
                    if (!tempList.Contains(new Vector2(j * this.mapDist, i * -this.mapDist)))
                    {
                        this.mapLocation[i, j] = 0;
                    }
                }
            }
        }
    }

    private int MakeMaxNumForNoramlRoom(int currentStage)
    {
        var num = default(int);
        if (currentStage == 1) num = this.forrestNormalRoomArr.Length;
        else if (currentStage == 2) num = this.graveNormalRoomArr.Length;
        else if (currentStage == 3) num = this.templeNormalRoomArr.Length;
        else if (currentStage == 4) num = this.heartOfDevilNormalRoomArr.Length;
        return num;
    }

    private void MakeHiddenRoom()
    {
        // Create the hidden room
        var hiddenGO = UnityEngine.Object.Instantiate(this.hiddemRoom);
        // Add the hidden room to the whole room list
        this.wholeRoomList.Add(hiddenGO);
        // Assign the start room position and the furthest normal room position to the hidden room position variable
        this.hiddemRoomPos = this.FindFurtherNormalRoomFromStart(this.startRoomPos, this.normalRoomList);
        // Set the position of the hidden room
        hiddenGO.transform.position = new Vector3(this.hiddemRoomPos.x, this.hiddemRoomPos.y, 1);
        // Destroy the normal room that was in the original position (find it in the normal room list)
        GameObject targetNoramlRoom = this.normalRoomList.Find(x => (Vector2)x.transform.position == this.hiddemRoomPos);
        // Destroy the found normal room
        GameObject.Destroy(targetNoramlRoom);
        // Remove the game object instance from the list
        this.normalRoomList.Remove(targetNoramlRoom);
        this.wholeRoomList.Remove(targetNoramlRoom);
    }

    private Vector2 FindFurtherNormalRoomFromStart(Vector2 start, List<GameObject> normalRoomList)
    {
        float maxDistance = 0f;
        Vector2 maxDistanceVector = Vector2.zero;

        for (int i = 0; i < normalRoomList.Count; i++)
        {
            float distance = Vector2.Distance(start, normalRoomList[i].transform.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                maxDistanceVector = normalRoomList[i].transform.position;
            }
        }
        return maxDistanceVector;
    }

    private void DeActivePortalsAndRooms()
    {
        for (int i = 0; i < this.wholeRoomList.Count; i++)
        {
            if (this.wholeRoomList[i].name.Contains("Start")) continue;
            else if (this.wholeRoomList[i].name.Contains("Hidden") || this.wholeRoomList[i].name.Contains("Safe"))
            {
                this.wholeRoomList[i].gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
            }
            else if (this.wholeRoomList[i].name.Contains("Boss"))
            {
                this.wholeRoomList[i].transform.GetChild(0).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                this.wholeRoomList[i].transform.GetChild(1).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                this.wholeRoomList[i].transform.GetChild(2).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                this.wholeRoomList[i].transform.GetChild(3).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                this.wholeRoomList[i].transform.GetChild(4).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                this.wholeRoomList[i].gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
            }
            else
            {
                this.wholeRoomList[i].transform.GetChild(0).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                this.wholeRoomList[i].transform.GetChild(1).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                this.wholeRoomList[i].transform.GetChild(2).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                this.wholeRoomList[i].transform.GetChild(3).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                this.wholeRoomList[i].gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
            }
        }
    }

    public void DeActivePortalsAndRoomsForTest()
    {
        for (int i = 0; i < this.wholeRoomList.Count; i++)
        {
            var go = this.wholeRoomList[i].transform.GetComponentInChildren<PlayerShell>();
            if (go == null)
            {
                if (this.wholeRoomList[i].name.Contains("Start")) continue;
                else if (this.wholeRoomList[i].name.Contains("Hidden") || this.wholeRoomList[i].name.Contains("Safe"))
                {
                    this.wholeRoomList[i].gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                }
                else if (this.wholeRoomList[i].name.Contains("Boss"))
                {
                    this.wholeRoomList[i].transform.GetChild(0).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                    this.wholeRoomList[i].transform.GetChild(1).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                    this.wholeRoomList[i].transform.GetChild(2).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                    this.wholeRoomList[i].transform.GetChild(3).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                    this.wholeRoomList[i].transform.GetChild(4).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                    this.wholeRoomList[i].gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                }
                else
                {
                    var go2 = this.wholeRoomList[i].transform.GetComponent<MonsterGenerator>();
                    if (!go2.isFinished)
                    {
                        this.wholeRoomList[i].transform.GetChild(0).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                        this.wholeRoomList[i].transform.GetChild(1).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                        this.wholeRoomList[i].transform.GetChild(2).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                        this.wholeRoomList[i].transform.GetChild(3).gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                        this.wholeRoomList[i].gameObject.SetActive(!this.wholeRoomList[i].activeSelf);
                    }
                }
            }
        }
    }


    private void MakePortal()
    {
        for (int i = 0; i < maxRow; i++)
        {
            for (int j = 0; j < maxCol; j++)
            {
                if (mapLocation[i, j] != 0)
                {
                    var room = this.wholeRoomList.Find((x) => (Vector2)x.transform.position == new Vector2(j * this.mapDist, i * -this.mapDist));
                    Transform[] roomChildren = { room.transform.GetChild(0), room.transform.GetChild(2), room.transform.GetChild(1), room.transform.GetChild(3) };
                    for (int k = 0; k < roomChildren.Length; k++)
                    {
                        Transform roomChild = roomChildren[k];
                        Vector2 roomPos = (Vector2)room.transform.position;
                        int top = i - 1 >= 0 ? mapLocation[i - 1, j] : -1;
                        int bottom = i + 1 < maxRow ? mapLocation[i + 1, j] : -1;
                        int right = j + 1 < maxCol ? mapLocation[i, j + 1] : -1;
                        int left = j - 1 >= 0 ? mapLocation[i, j - 1] : -1;
                        int[] roomLocations = { top, bottom, right, left };
                        int portalType = -1;

                        for (int l = 0; l < roomLocations.Length; l++)
                        {

                            int location = roomLocations[l];
                            if (location == -1) continue;
                            else
                            {
                                if (location == 4 && l == k) { portalType = 0; break; }
                                if (location == 3 && l == k) { portalType = 1; break; }
                                if (location != 0 && l == k) { portalType = 2; break; }
                            }
                        }

                        if (portalType == 0)
                        {
                            var go = GameObject.Instantiate(this.bossPortal, roomChild);
                            go.transform.localPosition = Vector3.forward;
                            if (roomChild.gameObject.name.Contains('0')) go.transform.Rotate(new Vector3(0, 0, 90));
                            if (roomChild.gameObject.name.Contains('2')) go.transform.Rotate(new Vector3(0, 0, -90));
                            if (roomChild.gameObject.name.Contains('3')) go.transform.Rotate(new Vector3(0, 0, -180));
                        }
                        else if (portalType == 1)
                        {
                            var go = GameObject.Instantiate(this.safeRoomPortal, roomChild);
                            go.transform.localPosition = Vector3.forward;
                            if (roomChild.gameObject.name.Contains('0')) go.transform.Rotate(new Vector3(0, 0, 90));
                            if (roomChild.gameObject.name.Contains('2')) go.transform.Rotate(new Vector3(0, 0, -90));
                            if (roomChild.gameObject.name.Contains('3')) go.transform.Rotate(new Vector3(0, 0, -180));
                        }
                        else if (portalType == 2)
                        {
                            var go = GameObject.Instantiate(this.portal, roomChild);
                            go.transform.localPosition = Vector3.forward;
                            if (roomChild.gameObject.name.Contains('0')) go.transform.Rotate(new Vector3(0, 0, 90));
                            if (roomChild.gameObject.name.Contains('2')) go.transform.Rotate(new Vector3(0, 0, -90));
                            if (roomChild.gameObject.name.Contains('3')) go.transform.Rotate(new Vector3(0, 0, -180));
                        }
                    }
                }
            }
        }
        this.nextStagePortal = this.wholeRoomList.Find(x => x.name.Contains("Boss")).transform.GetChild(4).gameObject;
    }
}