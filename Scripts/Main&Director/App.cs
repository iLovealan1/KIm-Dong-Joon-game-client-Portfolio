using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneArgs
{
    public string nextSceneType;
    public bool isFromTitle;
}

public class App : MonoBehaviour
{
    public enum eSceneType
    {
        AppScene, TitleScene, LoadingScene, SanctuaryScene, DungeonScene
    }

    private TitleSceneMain titleSceneMain;

    private void Awake()
    {
        //SRDebug.Init();  
        if (!this.NewbieCheck())
        {
            InfoManager.instance.LoadGameInfo();
            InfoManager.instance.LoadStatInfo();
            InfoManager.instance.LoadInventoryInfo();
            InfoManager.instance.LoadpossessionGoodsInfo();
            InfoManager.instance.LoadSettingInfo();

            //*Info Initializing
            //InfoManager.instance.SaveGameInfo();
            //InfoManager.instance.SaveStatInfo();
            //InfoManager.instance.SaveInventoryInfo();
            //InfoManager.instance.SavepossessionGoodsInfo();
            //InfoManager.instance.SaveSettingInfo();
            Debug.Log("Existing User");
        }
        else
        {
            InfoManager.instance.SaveGameInfo();
            InfoManager.instance.SaveStatInfo();
            InfoManager.instance.SaveInventoryInfo();
            InfoManager.instance.SavepossessionGoodsInfo();
            InfoManager.instance.SaveSettingInfo();
            Debug.Log("New User");
        }
        DataManager.Instance.LoadAllDatas();

        DontDestroyOnLoad(this.gameObject);
        //GPGSManager.instance.onAuthenticate = (x) =>
        //{
        //    Debug.LogFormat("=================onAuthenticate=============== : {0}", x);
        //    PlayGamesPlatform.Instance.RequestServerSideAccess(true, (token) =>
        //    {

        //        Debug.Log("********* token *********");
        //        Debug.Log(token);
        //        Debug.Log("*************************");

        //        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        //        Credential credential = PlayGamesAuthProvider.GetCredential(token);
        //        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        //        {
        //            if (task.IsCanceled)
        //            {
        //                Debug.LogFormat("******** task.IsCanceled ********");
        //                return;
        //            }

        //            if (task.IsFaulted)
        //            {
        //                Debug.LogFormat("******** task.IsFaulted ********");
        //                return;
        //            }

        //            FirebaseUser newUser = task.Result;
        //            if (newUser != null)
        //            {
        //                Debug.LogFormat("[newUser] DisplayName: {0}, UserId: {1}", newUser.DisplayName, newUser.UserId);
        //            }
        //            else
        //            {
        //                Debug.Log("newUser is null");
        //            }


        //            FirebaseUser currentUser = auth.CurrentUser;

        //            if (currentUser != null)
        //            {
        //                Debug.LogFormat("[currentUser] DisplayName: {0}, UserId: {1}", currentUser.DisplayName, currentUser.UserId);
        //            }
        //            else
        //            {
        //                Debug.LogFormat("currentUser is null");
        //            }
        //        });

        //    });
        //};
        //GPGSManager.instance.Authenticate();
    }

    private int targetFrame = 60;
    private void Start()
    {
        Debug.Log("App start");
        Application.targetFrameRate = this.targetFrame;
        this.ChangeScene(eSceneType.TitleScene);
    }

    public void ChangeScene(eSceneType sceneType, SceneArgs args = null)
    {
        Debug.LogFormat("ChangeScene: {0}", sceneType);
        var oper = SceneManager.LoadSceneAsync(sceneType.ToString());
        AudioManager.instance.BGMusicControl(AudioManager.eBGMusicPlayList.NONE, true);
        AudioManager.instance.SceneBGMusicSetting(sceneType);
        switch (sceneType)
        {
            case eSceneType.TitleScene:
                oper.completed += (obj) =>
                {
                    var arg = new SceneArgs() { nextSceneType = "SanctuaryScene", isFromTitle = true };
                    this.titleSceneMain = GameObject.FindObjectOfType<TitleSceneMain>();
                    this.titleSceneMain.uiTitleDirector.onClick = () =>
                    {
                        this.ChangeScene(eSceneType.LoadingScene, arg);
                    };
                    this.titleSceneMain.Init();
                };
                break;

            case eSceneType.LoadingScene:
                oper.completed += (obj) =>
                {
                    var loadingMain = GameObject.FindObjectOfType<LoadingSceneMain>();
                    loadingMain.onComplete = () =>
                    {
                        if (args.nextSceneType == "SanctuaryScene") this.ChangeScene(eSceneType.SanctuaryScene);
                        else if (args.nextSceneType == "DungeonScene") this.ChangeScene(eSceneType.DungeonScene);
                    };
                    loadingMain.Init(args);
                };
                break;

            case eSceneType.SanctuaryScene:
                oper.completed += (obj) =>
                {
                    var sanctuaryMain = GameObject.FindObjectOfType<SanctuarySceneMain>();
                    var arg = new SceneArgs() { nextSceneType = "DungeonScene" };
                    sanctuaryMain.onintotheDungeon = () =>
                    {
                        this.ChangeScene(eSceneType.LoadingScene, arg);
                    };
                    sanctuaryMain.Init();
                };
                break;

            case eSceneType.DungeonScene:
                oper.completed += (obj) =>
                {
                    var dungeonMain = GameObject.FindObjectOfType<DungeonSceneMain>();
                    var arg = new SceneArgs() { nextSceneType = "SanctuaryScene" };
                    dungeonMain.goBackToTheSanctuary = () =>
                    {
                        this.ChangeScene(eSceneType.LoadingScene, arg);
                    };
                    dungeonMain.Init();
                };
                break;
        }
    }

    private bool NewbieCheck()
    {
        bool isFNG = false;
        string filePath = Path.Combine(Application.persistentDataPath, "game_info.json");
        if (!File.Exists(filePath))
        {
            isFNG = true;
        }
        return isFNG;
    }

    private UIPauseDirector uiPauseDirector;
    // called when ApplicationPause
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            this.uiPauseDirector = GameObject.FindObjectOfType<UIPauseDirector>();

            if (this.uiPauseDirector != null && !this.uiPauseDirector.uiPauseMenu.gameObject.activeSelf)
            {
                this.uiPauseDirector.ActivePauseUI();
                this.uiPauseDirector.onPushPause(this.uiPauseDirector.uiPauseMenu.gameObject);
            }
        }
    }
}