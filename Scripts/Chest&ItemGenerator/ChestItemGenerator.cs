using DG.Tweening;
using SpriteGlow;
using SRF;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class ChestItemGenerator : MonoBehaviour
{
    public enum eDropItemGrade
    {
        Wood,
        Iron,
        Gold,
        Diamond
    }

    public enum eDropItemType
    {
        Sword,
        Axe,
        Arrow,
        Wand
    }

    public eDropItemGrade dropItemGrade;
    public eDropItemType dropItemType;
    private Dictionary<eDropItemGrade, int> itemCounts = new Dictionary<eDropItemGrade, int>();

    [SerializeField] private GameObject WoodChest;
    [SerializeField] private GameObject IronChest;
    [SerializeField] private GameObject GoldChest;
    [SerializeField] private GameObject DiamondChest;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private GameObject eitherPrefab;
    [SerializeField] private CoinPool pool;

    [SerializeField] private GameObject ChestApear;
    [SerializeField] private GameObject ChestApear2;

    private ShopFactory factory;
    public DungeonInfo dungeonInfo = new DungeonInfo();
    private GameObject go = default;

    private bool isStayInput;

    private List<int> itemPool;
    private int itemCount;
    private int RelicitemCount = 0;

    private AudioSource audioSource;
    private HashSet<Vector2> existingPositions = new HashSet<Vector2>();


    private void Awake()
    {
        itemPool = new List<int> { 0, 1, 2, 3, 4 };
        itemCount = itemPool.Count;
    }

    public void Init()
    {
        this.factory = this.gameObject.GetComponent<ShopFactory>();
        this.audioSource = GetComponent<AudioSource>();

        if (this.pool != null)
            this.pool.Init(this.dropItem);

        Observable.EveryUpdate()
            .Select(_ => Input.GetMouseButton(0))
            .DistinctUntilChanged()
            .Where(isPressed => isPressed)
            .Throttle(TimeSpan.FromSeconds(0.3))
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ =>
            {
                this.StayHandleInput();
            })
            .AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ =>
            {
                this.HandleInput(this.isStayInput);
            })
            .AddTo(this);

        EventDispatcher.Instance.AddListener<Transform, bool>(EventDispatcher.EventName.ChestItemGeneratorMakeChest,
            this.MakeChset);
        EventDispatcher.Instance.AddListener<Transform, string>(EventDispatcher.EventName.ChestItemGeneratorMakeItemForChest,
            this.MakeItemForChest);
        EventDispatcher.Instance.AddListener<string>(EventDispatcher.EventName.ChestItemGeneratorMakeItemForInventory,
            this.MakeItemForInventory);
        EventDispatcher.Instance.AddListener<Vector3>(EventDispatcher.EventName.ChestItemGeneratorMakeFieldCoin,
            this.MakeFieldCoin);
    }

    private void StayHandleInput()
    {
        var screenPos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("item"))
        {
            if (hit.transform.gameObject.name == "Gold") return;
            var item = hit.transform.gameObject.name;
            this.isStayInput = true;
            if (screenPos.y > Camera.main.scaledPixelHeight - 350)
            {
                worldPos.y -= 3f;
            }
            else
            {
                worldPos.y += 3;
            }

            var pos = new Vector3(worldPos.x, worldPos.y, 0);
            var name = hit.transform.gameObject.name;
            EventDispatcher.Instance.Dispatch<string, Vector3>
                (EventDispatcher.EventName.UIFieldItemPopupDirectorUpdatePopup, name, pos);
        }
    }

    private void HandleInput(bool isStayT = false)
    {
        this.RaycastInput(Input.mousePosition, isStayT);
    }

    private void RaycastInput(Vector3 inputPosition, bool isStayT = false)
    {
        if (isStayT)
        {
            this.isStayInput = false;
            EventDispatcher.Instance.Dispatch
                (EventDispatcher.EventName.UIFieldItemPopupDirectorClosePopup);
            return;
        }
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(inputPosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

        if (hit.collider != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("item"))
        {
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_SkillButton, this.audioSource);
            var comp = hit.transform.GetComponent<DropItem>();
            if (comp != null)
            {
                comp.ClickedItemCheck();
            }
            EventDispatcher.Instance.Dispatch
                (EventDispatcher.EventName.UIFieldItemPopupDirectorClosePopup);
        }
    }

    /// <summary>
    /// Make Chest For NormalRooms
    /// </summary>
    /// <param name="callPosition">pos for Chest</param>
    /// <param name="isBoss">isBossRoom?</param>
    private void MakeChset(Transform callPosition, bool isBoss = false) //boss
    {
        Transform chestTrans = callPosition.transform.GetChildren()
            .Where(c => c.gameObject.tag == "ChestPoint")
            .Select(c => c.transform)
            .First();
        var setp = InfoManager.instance.dungeonInfo.currentStepInfo;
        var stageNum = InfoManager.instance.dungeonInfo.CurrentStageInfo;
        //var go = default(GameObject);

        if (!isBoss)
        {
            if (setp < 3)
            {
                go = GameObject.Instantiate(this.WoodChest, chestTrans);
                go.name = go.name.Replace("(Clone)", "");
                go.transform.localPosition = Vector2.zero;
            }
            else if (setp < 6)
            {
                go = GameObject.Instantiate(this.IronChest, chestTrans);
                go.name = go.name.Replace("(Clone)", "");
                go.transform.localPosition = Vector2.zero;
            }
            else if (setp < 16)
            {
                go = GameObject.Instantiate(this.GoldChest, chestTrans);
                go.name = go.name.Replace("(Clone)", "");
                go.transform.localPosition = Vector2.zero;
            }

            var comp = go.GetComponent<NPCController>();
            comp.stepNum = setp;
            comp.stageNum = stageNum;
            Debug.LogFormat("stage:{0},step:{1}", stageNum, setp);
        }
        else if (isBoss)
        {
            go = GameObject.Instantiate(this.DiamondChest, chestTrans);
            go.name = go.name.Replace("(Clone)", "");
            go.transform.localPosition = Vector2.zero;

            var comp = go.GetComponent<NPCController>();
            comp.stepNum = stageNum * 100; //1~15 ==difficulty
        }
        var effect = GameObject.Instantiate(this.ChestApear, chestTrans);
        effect.transform.localPosition = new Vector3(-0.51f, 1.16f, 0);
        Destroy(effect, 0.8f);
        var effect2 = GameObject.Instantiate(this.ChestApear2, chestTrans);
        effect2.transform.localPosition = new Vector3(-0.53f, -0.36f, 0);
        Destroy(effect2, 0.9f);
    }

    /// <summary>
    /// Make Item For Chset(with NPCController)
    /// </summary>
    /// <param name="callPosition">pos for Chest</param>
    /// <param name="chestName">chest name (ex : Wood_Chest)</param>
    /// <param name="chestName">Wood,Iron,Gold : Equip+Gold+Ether, Diamond : Relic+Weapon
    private void MakeItemForChest(Transform callPosition, string chestName) //WayPoint 0~5
    {
        if (chestName == "Wood_Chest" || chestName == "Iron_Chest" || chestName == "Gold_Chest")
        {
            this.MakeCoinInDungeon(callPosition);
            this.MakeEtherInDungeon(callPosition);
            var stepNum = this.go.GetComponent<NPCController>().stepNum;

            var WoodCount = DataManager.Instance.GetReturnChestDicValue(stepNum).Wood;
            var IronCount = DataManager.Instance.GetReturnChestDicValue(stepNum).Iron;
            var GoldCount = DataManager.Instance.GetReturnChestDicValue(stepNum).Gold;
            var DiamondCount = DataManager.Instance.GetReturnChestDicValue(stepNum).Diamond;

            this.itemCounts.Clear();
            this.itemCounts.Add(eDropItemGrade.Wood, WoodCount);
            this.itemCounts.Add(eDropItemGrade.Iron, IronCount);
            this.itemCounts.Add(eDropItemGrade.Gold, GoldCount);
            this.itemCounts.Add(eDropItemGrade.Diamond, DiamondCount);

            foreach (var itemCount in this.itemCounts)
            {
                eDropItemGrade grade = itemCount.Key;
                int count = itemCount.Value;
                for (int i = 0; i < count; i++) //ex) Wood 3, Iron 1
                {
                    var dropItemGo = Instantiate(this.dropItem, callPosition);
                    dropItemGo.layer = 14;
                    this.SetRandomPos(callPosition, dropItemGo);

                    var ranTypeNum = UnityEngine.Random.Range(0, 4);
                    var ranType = (eDropItemType)ranTypeNum;
                    dropItemGo.name = grade + "_" + ranType;

                    var resultSp = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon").GetSprite(dropItemGo.name);
                    dropItemGo.GetComponent<SpriteRenderer>().sprite = resultSp;
                    dropItemGo.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    this.InitGlowEffect(dropItemGo);
                }
            }

            float randomValue = UnityEngine.Random.Range(0f, 1f);
            float randomFoodValue = UnityEngine.Random.Range(0f, 1f);
            if (randomValue <= 0.8f)
            {
                //Debug.LogFormat("Food Drop,{0}",randomValue);
                var dropItemGo = Instantiate(this.dropItem, callPosition);
                dropItemGo.tag = "Food";
                dropItemGo.layer = 14;
                this.SetRandomPos(callPosition, dropItemGo);

                dropItemGo.transform.localScale = new Vector3(1f, 1f, 1f);
                if (randomFoodValue <= 0.7f) //Iron Food //Test 50% Iron, Gold
                {
                    var resultSp = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon").GetSprite("Iron_Food");
                    dropItemGo.GetComponent<SpriteRenderer>().sprite = resultSp;
                    dropItemGo.gameObject.name = "Iron_Food";
                    dropItemGo.transform.localScale = Vector3.one;
                }
                else
                {
                    var resultSp = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon").GetSprite("Gold_Food");
                    dropItemGo.GetComponent<SpriteRenderer>().sprite = resultSp;
                    dropItemGo.gameObject.name = "Gold_Food";
                    dropItemGo.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                }
                this.InitGlowEffect(dropItemGo);
            }

        }
        if (chestName == "Diamond_Chest") //BOSSCHSET
        {
            if (InfoManager.instance.dungeonInfo.CurrentStageInfo != 4)
            {
                var foodItemGo = Instantiate(this.dropItem, callPosition);
                foodItemGo.tag = "Food";
                foodItemGo.layer = 14;
                this.SetRandomPos(callPosition, foodItemGo);
                var foodResultSp = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon").GetSprite("Diamond_Food");
                foodItemGo.GetComponent<SpriteRenderer>().sprite = foodResultSp;
                foodItemGo.gameObject.name = "Diamond_Food";
                foodItemGo.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                this.InitGlowEffect(foodItemGo);

                this.MakeWeaponInDungeon(callPosition);
                this.MakeRelicInDungeon(callPosition);
                this.MakeCoinInDungeon(callPosition);
                this.MakeEtherInDungeon(callPosition);

                var stage = InfoManager.instance.dungeonInfo.CurrentStageInfo;
                var data = DataManager.Instance.GetReturnChestDicValue(stage * 100); //100,200,300,400
                var bossStage = data.difficulty;

                var WoodCount = DataManager.Instance.GetReturnChestDicValue(bossStage).Wood;
                var IronCount = DataManager.Instance.GetReturnChestDicValue(bossStage).Iron;
                var GoldCount = DataManager.Instance.GetReturnChestDicValue(bossStage).Gold;
                var DiamondCount = DataManager.Instance.GetReturnChestDicValue(bossStage).Diamond;

                this.itemCounts.Clear();
                this.itemCounts.Add(eDropItemGrade.Wood, WoodCount);
                this.itemCounts.Add(eDropItemGrade.Iron, IronCount);
                this.itemCounts.Add(eDropItemGrade.Gold, GoldCount);
                this.itemCounts.Add(eDropItemGrade.Diamond, DiamondCount);
                foreach (var itemCount in this.itemCounts)
                {
                    eDropItemGrade grade = itemCount.Key;
                    int count = itemCount.Value;
                    for (int i = 0; i < count; i++) //ex) Wood 3, Iron 1
                    {
                        var dropItemGo = Instantiate(this.dropItem, callPosition);

                        dropItemGo.layer = 14;
                        this.SetRandomPos(callPosition, dropItemGo);


                        var ranTypeNum = UnityEngine.Random.Range(0, 4);
                        var ranType = (eDropItemType)ranTypeNum;
                        dropItemGo.name = grade + "_" + ranType;

                        var resultSp = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon").GetSprite(dropItemGo.name);
                        //Debug.LogFormat("{0}", resultSp);

                        dropItemGo.GetComponent<SpriteRenderer>().sprite = resultSp;
                        dropItemGo.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                        this.InitGlowEffect(dropItemGo);
                    }
                }
            }
            else
            {
                this.MakeCoinInDungeon(callPosition);
                this.MakeEtherInDungeon(callPosition);
            }

        }
        if (chestName == "HiddenWoodChest" || chestName == "HiddenIronChest" || chestName == "HiddenGoldChest") //BOSSCHSET
        {
            //Test
            //this.MakeRelicInDungeon(callPosition);

            var stage = InfoManager.instance.dungeonInfo.CurrentStageInfo;
            var data = DataManager.Instance.GetReturnChestDicValue(stage * 100); //100,200,300,400
            var bossStage = data.difficulty;

            var WoodCount = DataManager.Instance.GetReturnChestDicValue(bossStage).Wood;
            var IronCount = DataManager.Instance.GetReturnChestDicValue(bossStage).Iron;
            var GoldCount = DataManager.Instance.GetReturnChestDicValue(bossStage).Gold;
            var DiamondCount = DataManager.Instance.GetReturnChestDicValue(bossStage).Diamond;

            this.itemCounts.Clear();
            this.itemCounts.Add(eDropItemGrade.Wood, WoodCount);
            this.itemCounts.Add(eDropItemGrade.Iron, IronCount);
            this.itemCounts.Add(eDropItemGrade.Gold, GoldCount);
            this.itemCounts.Add(eDropItemGrade.Diamond, DiamondCount);
            foreach (var itemCount in this.itemCounts)
            {
                eDropItemGrade grade = itemCount.Key;
                int count = itemCount.Value;
                for (int i = 0; i < count; i++) //ex) Wood 3, Iron 1
                {
                    var dropItemGo = Instantiate(this.dropItem, callPosition);

                    dropItemGo.layer = 14;
                    this.SetRandomPos(callPosition, dropItemGo);

                    var ranTypeNum = UnityEngine.Random.Range(0, 4);
                    var ranType = (eDropItemType)ranTypeNum;
                    dropItemGo.name = grade + "_" + ranType;

                    var resultSp = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon").GetSprite(dropItemGo.name);

                    dropItemGo.GetComponent<SpriteRenderer>().sprite = resultSp;
                    dropItemGo.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    this.InitGlowEffect(dropItemGo);

                }
            }
            if (chestName == "HiddenGoldChest")
            {
                this.MakeCoinInDungeon(callPosition);
                this.MakeEtherInDungeon(callPosition);
            }
        }
    }

    /// <summary>
    /// make cold for chest
    /// ran pos with sprite
    /// </summary>
    /// <param name="callPosition"></param>
    private void MakeCoinInDungeon(Transform callPosition)
    {
        var coinGo = Instantiate(this.dropItem, callPosition);
        coinGo.tag = "Coin";
        coinGo.layer = 14;

        this.SetRandomPos(callPosition, coinGo);

        var resultSpriteAtlas = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon");
        var resultSp = resultSpriteAtlas.GetSprite("GoldBag");
        coinGo.GetComponent<SpriteRenderer>().sprite = resultSp;

        coinGo.transform.localScale = new Vector3(1f, 1f, 1f);
        coinGo.name = resultSp.name.Replace("(Clone)", "");

        var step = callPosition.GetComponent<NPCController>().stepNum;
        var stage = callPosition.GetComponent<NPCController>().stageNum;


        if (callPosition.CompareTag("HiddenChest"))
        {
            coinGo.GetComponent<DropItem>().stepNum = this.dungeonInfo.CurrentStageInfo * 100;
        }
        else
        {
            coinGo.GetComponent<DropItem>().stepNum = step;
        }

        this.InitGlowEffect(coinGo);

    }
    /// <summary>
    ///  make Ether for chest
    /// </summary>
    /// <param name="callPosition"></param>
    private void MakeEtherInDungeon(Transform callPosition)
    {
        var etherGo = Instantiate(this.eitherPrefab, callPosition);
        etherGo.tag = "Ether";
        etherGo.layer = 14;

        this.SetRandomPos(callPosition, etherGo);
        etherGo.GetComponent<BoxCollider2D>().size = new Vector2(0.3872886f, 0.31f);

        var resultSpriteAtlas = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon");
        var resultSp = resultSpriteAtlas.GetSprite("Ether");
        etherGo.GetComponent<SpriteRenderer>().sprite = resultSp;
        etherGo.name = resultSp.name.Replace("(Clone)", "");

        var step = callPosition.GetComponent<NPCController>().stepNum;
        var stage = callPosition.GetComponent<NPCController>().stageNum;

        if (callPosition.CompareTag("HiddenChest"))
        {
            etherGo.GetComponent<DropItem>().stepNum = this.dungeonInfo.CurrentStageInfo * 100;
        }
        else
        {
            etherGo.GetComponent<DropItem>().stepNum = step;
        }
    }

    /// <summary>
    /// Make Weapon for DiamondChest
    /// </summary>
    private void MakeWeaponInDungeon(Transform callPosition)
    {
        var weaponGo = Instantiate(this.dropItem, callPosition);
        weaponGo.tag = "Weapon";
        weaponGo.layer = 14;
        this.SetRandomPos(callPosition, weaponGo);
        var weaponSp = weaponGo.GetComponent<SpriteRenderer>().sprite;
        var resultSpriteAtlas = AtlasManager.instance.GetAtlasByName("UIWeaponIcon");
        var goScale = weaponGo.transform.localScale;
        int ran = UnityEngine.Random.Range(0, 4);
        var colSize = weaponGo.GetComponent<BoxCollider2D>().size;
        switch (ran)
        {
            case 0:
                weaponSp = resultSpriteAtlas.GetSprite("AssultRifle");
                goScale = new Vector3(0.07f, 0.089f, 0.07f);
                colSize = new Vector2(7.332444f, 2f);
                break;
            case 1:
                weaponSp = resultSpriteAtlas.GetSprite("ShotGun");
                goScale = new Vector3(0.47f, 0.68f, 0.47f);
                colSize = new Vector2(0.9373323f, 0.3472342f);
                break;
            case 2:
                weaponSp = resultSpriteAtlas.GetSprite("SniperRifle");
                colSize = new Vector2(0.7455674f, 0.21f);
                goScale = new Vector3(0.77f, 0.77f, 0.77f);
                break;
            case 3:
                weaponSp = resultSpriteAtlas.GetSprite("SubmachineGun");
                colSize = new Vector2(5.094956f, 3.167958f);
                goScale = new Vector3(0.05f, 0.05f, 0.05f);
                break;
        }
        weaponGo.GetComponent<BoxCollider2D>().size = colSize;
        weaponGo.transform.localScale = goScale;
        weaponGo.GetComponent<SpriteRenderer>().sprite = weaponSp;
        Debug.LogFormat("GetWeaponName:{0}", weaponSp.name);
        weaponGo.name = weaponSp.name.Replace("(Clone)", "");
        this.InitGlowEffect(weaponGo);
    }

    /// <summary>
    /// make relic for DiamondChest
    /// </summary>
    private void MakeRelicInDungeon(Transform callPosition)
    {
        var relicGo = Instantiate(this.dropItem, callPosition);
        relicGo.tag = "Relic";
        relicGo.layer = 14;
        var colSize = relicGo.GetComponent<BoxCollider2D>().size;

        this.SetRandomPos(callPosition, relicGo);

        var relicSp = relicGo.GetComponent<SpriteRenderer>().sprite;
        var resultSpriteAtlas = AtlasManager.instance.GetAtlasByName("DropItemRelic");
        var relicScale = relicGo.transform.localScale;

        if (itemCount == 0)
        {
            //Debug.Log("모든 Relic 생성됨");
            return;
        }
        else if (this.RelicitemCount >= 3)
        {
            //Debug.Log("Relic 소지 최대,Ether로 대체 생성");
        }

        int randomIndex = UnityEngine.Random.Range(0, itemCount);
        int randomItem = itemPool[randomIndex];
        itemPool.RemoveAt(randomIndex);
        itemCount--;
        this.RelicitemCount++;
        switch (randomItem)
        {
            case 0:
                relicSp = resultSpriteAtlas.GetSprite("LaserLine");
                relicScale = new Vector3(0.3f, 0.4521453f, 0.2460225f);
                colSize = new Vector2(0.97f, 0.3563223f);
                break;
            case 1:
                relicSp = resultSpriteAtlas.GetSprite("DashAttack");
                relicScale = new Vector3(0.35f, 0.35f, 0.35f);
                colSize = new Vector2(0.7875434f, 0.48f);
                break;
            case 2:
                relicSp = resultSpriteAtlas.GetSprite("PoisonBullet");
                relicScale = new Vector3(0.37f, 0.37f, 0.37f);
                relicGo.GetComponent<BoxCollider2D>().offset = new Vector2(0.0009f, 0.002f);
                relicGo.GetComponent<BoxCollider2D>().size = new Vector2(0.73f, 0.45f);
                break;
            case 3:
                relicSp = resultSpriteAtlas.GetSprite("GrenadeLauncher");
                relicScale = new Vector3(0.35f, 0.35f, 0.35f);
                break;
            case 4:
                relicSp = resultSpriteAtlas.GetSprite("DefensiveBullet");
                relicScale = new Vector3(0.35f, 0.35f, 0.35f);
                break;
        }

        Debug.LogFormat("<color=red>RelicRan:{0},randomIndex:{1},RelicitemCount:{2}</color>", randomItem, randomIndex, this.RelicitemCount);

        relicGo.GetComponent<BoxCollider2D>().size = colSize;
        relicGo.transform.localScale = relicScale;
        relicGo.GetComponent<SpriteRenderer>().sprite = relicSp;
        relicGo.name = relicSp.name.Replace("(Clone)", "");
        this.InitGlowEffect(relicGo);
    }


    /// <summary>
    /// *do not use before patch
    /// </summary>
    /// <param name="discaredItemName">name for discarded item</param>
    private void MakeItemForInventory(string discaredItemName)
    {
        var playerTrans = GameObject.FindWithTag("Player").transform;
        var dropGo = Instantiate(this.dropItem, playerTrans.parent);
        dropGo.tag = "Equipment";
        dropGo.layer = 14;
        Vector2 playerPosition = playerTrans.position;
        float radius = 2f;
        float minDistance = 1.8f;
        Vector2 randomPosition;
        do
        {
            float randomAngle = UnityEngine.Random.Range(0f, 360f);
            float randomRadius = UnityEngine.Random.Range(0f, radius);
            Vector2 randomOffset = Quaternion.Euler(0f, 0f, randomAngle) * Vector2.right * randomRadius;
            randomOffset += new Vector2(UnityEngine.Random.Range(-minDistance, minDistance), UnityEngine.Random.Range(-minDistance, minDistance));
            randomPosition = playerPosition + randomOffset + new Vector2(1.5f, 1.5f);
            bool isOverlap = false;
            Collider2D[] itemColliders = Physics2D.OverlapCircleAll(randomPosition, 2f, LayerMask.GetMask("item", "Player"));
            for (int i = 0; i < itemColliders.Length; i++)
            {
                if (itemColliders[i].gameObject != dropGo)
                {
                    float distance = Vector2.Distance(randomPosition, itemColliders[i].transform.position);
                    if (distance < minDistance)
                    {
                        isOverlap = true;
                        break;
                    }
                }
            }
            if (isOverlap)
            {
                continue;
            }
            break;
        } while (true);
        dropGo.transform.position = randomPosition;

        var resultSpriteAtlas = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon");
        var resultSp = resultSpriteAtlas.GetSprite(discaredItemName);
        dropGo.GetComponent<SpriteRenderer>().sprite = resultSp;
        dropGo.transform.localScale = new Vector3(4f, 4f, 4f);
        dropGo.name = resultSp.name.Replace("(Clone)", "");
        this.InitGlowEffect(dropGo);

        var comp = dropGo.GetComponent<DropItem>();
        comp.FloatingEffect(comp.transform.position);
    }

    private void MakeFieldCoin(Vector3 mosterPos)
    {
        var ran = UnityEngine.Random.Range(1, 5);
        if (ran == 1)
        {
            var coin = this.pool.GetObjectFromPool();
            coin.SetActive(true);
            coin.transform.SetParent(this.pool.transform);
            coin.transform.position = mosterPos;
            var comp = coin.GetComponent<DropItem>();
            comp.Init();
        }
        else return;
    }

    private void SetRandomPos(Transform callPosition, GameObject dropItemGo)
    {
        Vector2 chestPosition = callPosition.position;
        float radius = 3.5f;
        float minDistance = 1.5f;

        int maxAttempts = 12;
        int attempts = 0;

        do
        {
            attempts++;

            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle.normalized * radius;
            Vector2 randomPos = chestPosition + randomOffset;

            bool isColliding = false;

            foreach (Vector2 position in existingPositions)
            {
                if (Vector2.Distance(randomPos, position) < minDistance)
                {
                    isColliding = true;
                    break;
                }
            }

            if (!isColliding)
            {
                existingPositions.Add(randomPos);
                break;
            }

            radius += 0.5f;
        }
        while (attempts < maxAttempts);

        if (existingPositions.Count == 0)
        {
            //Debug.LogWarning("위치 찾기 실패");
            return;
        }

        int playerLayer = LayerMask.NameToLayer("Player");
        int itemLayer = LayerMask.NameToLayer("item");
        Physics2D.IgnoreLayerCollision(playerLayer, itemLayer);

        BoxCollider2D collider = dropItemGo.GetComponent<BoxCollider2D>();

        collider.enabled = false;

        Vector2 randomPosition = existingPositions.Last();

        dropItemGo.transform.position = callPosition.position;

        dropItemGo.transform.DOMove(randomPosition, 1f).OnComplete(() =>
        {
            if (dropItemGo.CompareTag("Weapon"))
            {
                Vector3 originalScale = dropItemGo.transform.localScale;
                float assetScalePercent = 1.5f;
                Vector3 targetScale = originalScale * assetScalePercent;

                dropItemGo.transform.DOScale(targetScale, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);

            }
            Physics2D.IgnoreLayerCollision(playerLayer, itemLayer, false);
            collider.enabled = true;

            var comp = dropItemGo.GetComponent<DropItem>();
            comp.FloatingEffect(comp.transform.position);
        });
    }

    private void InitGlowEffect(GameObject dropGo)
    {
        var glowComp = dropGo.GetComponent<SpriteGlowEffect>();
        glowComp.GlowBrightness = 1.3f;
        if (dropGo.name.Contains("Wood")) glowComp.GlowColor = new Color32(147, 59, 0, 255);
        else if (dropGo.name.Contains("Iron") && !dropGo.name.Contains("Food")) glowComp.GlowColor = new Color32(130, 130, 130, 255);
        else if (dropGo.name.Contains("Gold") && !dropGo.name.Contains("Bag") && !dropGo.name.Contains("Food")) glowComp.GlowColor = Color.yellow;
        else if (dropGo.name.Contains("Diamond") && !dropGo.name.Contains("Food")) glowComp.GlowColor = Color.cyan;
        else
        {
            if (dropGo.name == "LaserLine") glowComp.DrawOutside = true;
            if (dropGo.name == "SubmachineGun") glowComp.OutlineWidth = 4;
            glowComp.GlowBrightness = 1.3f;
            glowComp.GlowColor = Color.white;
        }
    }
}

