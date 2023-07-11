using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GPGSManager
{
    public enum eErrorType
    {
        SAVE_IS_ALREADY_IN_PROGRESS,
        LOAD_IS_ALREADY_IN_PROGRESS,
        FAILED_TO_WRITE_SAVED_GAME,
        SAVED_GAME_IS_NOT_OPEN_YET,
        FAILED_TO_OPEN_SAVED_GAME,
        FILED_TO_READ_SAVED_GAME_DATA,
        FILED_TO_REPORT_ACHIEVEMENT_PROGRESS,
        FILED_TO_REPORT_HIGH_SCORE_LEADERBOARD,
        FAILED_TO_CREATE_NEW_SAVED_GAME
    }

    public Action<bool> onAuthenticate;
    public Action<bool> onSavedDataToCloud;
    public Action<string> onLoadDataFromCloud;
    public Action<eErrorType> onErrorHandler;

    public static readonly GPGSManager instance = new GPGSManager();

    private const string SAVE_NAME = "game_info";
    private bool isSaving = false;
    private bool isLoading = false;

    private GPGSManager() { }

    public void Authenticate()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    private void ProcessAuthentication(SignInStatus status)
    {
        Debug.LogFormat("ProcessAuthentication status : {0}", status);
        if (status == SignInStatus.Success)
        {   
            // Continue with Play Games Services            
            Debug.Log("Continue with Play Games Services");
            this.onAuthenticate(true);
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button            
            // to ask users to sign-in. Clicking it should call            
            PlayGamesPlatform.Instance.ManuallyAuthenticate(inStatus =>
            {
                Debug.LogFormat("inStatus: {0}", inStatus);
                if (inStatus == SignInStatus.Success) onAuthenticate(true);
                else onAuthenticate(false);
            });
        }
    }
    //public void SaveDataToCloud(string json)
    //{
    //    SaveDataToCloudASync();
    //}
    //public void LoadDataFromCloud()
    //{
    //    this.LoadDataFromCloudASync();
    //}
    private async Task SaveDataToCloudASync()
    {
        await this.SaveToCloudAsync();
    }
    private async Task SaveToCloudAsync()
    {
        if (isSaving)
        {
            Debug.Log("A save is already in progress.");
            this.onErrorHandler(eErrorType.SAVE_IS_ALREADY_IN_PROGRESS);
            return;
        }
        isSaving = true;
        ISavedGameMetadata savedGameMetadata = await GetSavedGameMetadataAsync();
        //if there is no Saved Data, Make new Save Data 
        if (savedGameMetadata == null)
        {
            this.CreateNewSavedGame();
        }
        byte[] data = Encoding.UTF8.GetBytes("Hello, world!");
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        var savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.CommitUpdate(savedGameMetadata, updatedMetadata, data, OnSavedGameWritten);
        isSaving = false;
        Debug.Log("save data complete");
        if (onSavedDataToCloud != null)
            onSavedDataToCloud(true);
        else
            Debug.Log("onSavedDataToCloud is null");
    }

    private void CreateNewSavedGame()
    {
        byte[] data = Encoding.UTF8.GetBytes("Hello, world!");        
        // 새로운 게임 메타데이터 생성  
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()            
            .WithUpdatedDescription("Saved game at " + DateTime.Now);        
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        // Saved Game 저장      
        var savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        //access        
        savedGameClient.OpenWithAutomaticConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, (status, metadata) =>       
            {           
            if (status == SavedGameRequestStatus.Success)        
                {               
                    // 저장된 게임이 없는 경우 새로운 게임을 생성합니다.              
                     savedGameClient.CommitUpdate(metadata, updatedMetadata, data, OnSavedGameWritten);   
                    Debug.Log("New saved game created.");           
                    Firebase.Analytics.FirebaseAnalytics.LogEvent(EnumManager.eAnalyticsEventType.save_to_cloud.ToString());         
                }           
                else       
                {              
                    Debug.LogError("Failed to create new saved game: " + status.ToString());            
                    this.onErrorHandler(eErrorType.FAILED_TO_CREATE_NEW_SAVED_GAME);          
                }      
            });   
    }

    private void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Saved game written: " + metadata.Description);
        }
        else
        {
            Debug.LogError("Failed to write saved game: " + status.ToString());
            this.onErrorHandler(eErrorType.FAILED_TO_WRITE_SAVED_GAME);
        }
    }
    private async Task<ISavedGameMetadata> GetSavedGameMetadataAsync()
    {
        var savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        var taskCompletionSource = new TaskCompletionSource<ISavedGameMetadata>();
        savedGameClient.OpenWithAutomaticConflictResolution(SAVE_NAME, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, (status, metadata) =>
            {
                Debug.LogFormat("metadata: {0}", metadata); //null
                if (status == SavedGameRequestStatus.Success)
                {
                    Debug.Log("Opened saved game: " + metadata.Description);
                    if (metadata.IsOpen)
                    {
                        Debug.Log("Saved game is already open.");
                    }
                    else
                    {
                        Debug.Log("Saved game is not open yet, opening...");
                        this.onErrorHandler(eErrorType.SAVED_GAME_IS_NOT_OPEN_YET);
                    }
                    taskCompletionSource.SetResult(metadata);
                }
                else
                {
                    Debug.LogError("Failed to open saved game: " + status.ToString());
                    this.onErrorHandler(eErrorType.FAILED_TO_OPEN_SAVED_GAME);
                    taskCompletionSource.SetResult(null);
                }
            });
        return await taskCompletionSource.Task;
    }
    private async Task LoadDataFromCloudASync()
    {
        await this.LoadFromCloudAsync();
    }
    private async Task LoadFromCloudAsync()
    {
        if (isLoading)
        {
            Debug.Log("A load is already in progress.");
            this.onErrorHandler(eErrorType.LOAD_IS_ALREADY_IN_PROGRESS);
            return;
        }
        isLoading = true;
        ISavedGameMetadata savedGameMetadata = await GetSavedGameMetadataAsync();
        if (savedGameMetadata == null)
        {
            isLoading = false;
            return;
        }
        var savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(savedGameMetadata, (status, data) =>
        {
            isLoading = false;
            if (status == SavedGameRequestStatus.Success)
            {
                string savedData = Encoding.UTF8.GetString(data);
                Debug.Log("Loaded saved game data: " + savedData);
                if (onLoadDataFromCloud != null)
                    this.onLoadDataFromCloud(savedData);
                else
                    Debug.Log("onLoadDataFromCloud is null");
            }
            else
            {
                Debug.LogError("Failed to read saved game data: " + status.ToString());
                this.onErrorHandler(eErrorType.FILED_TO_READ_SAVED_GAME_DATA);
            }
        });
    }
    public void ReportAchievement(string achievementID, double progress)
    {
        PlayGamesPlatform.Instance.ReportProgress(achievementID, progress, (bool success) =>
        {
            // handle success or failure            
            if (success)
            {
                Debug.LogFormat("{0} {1} {2}", achievementID, progress, success);
            }
            else
            {
                this.onErrorHandler(eErrorType.FILED_TO_REPORT_ACHIEVEMENT_PROGRESS);
            }
        });
    }
    public void ShowAchievementUI()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
    public void ReportHighScore(long score, string board)
    {
        PlayGamesPlatform.Instance.ReportScore(score, board,
            (success) =>
            {
                if (success)
                {
                    Debug.LogFormat("{0} {1} {2}", score, board, success);
                }
                else
                {
                    this.onErrorHandler(eErrorType.FILED_TO_REPORT_HIGH_SCORE_LEADERBOARD);
                }
            });
    }
    public void ShowLeaderboardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }
    public bool IsAuthenticate()
    {
        return PlayGamesPlatform.Instance.localUser.authenticated;
    }
}
