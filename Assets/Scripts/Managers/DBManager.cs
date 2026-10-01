using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static DBManager;

public class DBManager : ManagerBase
{
    FirebaseAuth authentication;
    FirebaseUser user;
    DatabaseReference rootReference;
    public UserData resultData { get;  set; }
    TextMeshProUGUI idText;
    
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(InitializeFireBase);
        yield return null;
    }
    

    protected override void OnDisconnected()
    {
       
    }

    void InitializeFireBase(Task<DependencyStatus> task)
    {
        try
        {
            if (task.Result == DependencyStatus.Available)
            {
                authentication = FirebaseAuth.DefaultInstance;
                user = authentication.CurrentUser;
                Debug.Log(FirebaseDatabase.DefaultInstance);
                rootReference = FirebaseDatabase.DefaultInstance.RootReference;

                GuestLogin();

            }
            else
            {
                Debug.LogError($"firebase 실패 : {task.Exception}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

    }

    public async void GuestLogin()
    {
        if (authentication is null) return;

        if (user is not null)
        {
            resultData = await ReadDataAsync<UserData>(
                "users",
                "userData",
                user.UserId
            );

            if (resultData is not null)
            {
                Debug.Log($"resultnickname : {resultData.nickname}");

                WriteData(
                    resultData,
                    "users",
                    "userData",
                    user.UserId
                );

                UpdateRewardUI();
            }
            else
            {
                resultData = NewUserData("GongBack");

                WriteData(
                    resultData,
                    "users",
                    "userData",
                    user.UserId
                );

                UpdateRewardUI();
            }

            return;
        }

        await authentication.SignInAnonymouslyAsync()
            .ContinueWithOnMainThread(OnLoginResult);
    }


    void OnLoginResult(Task<AuthResult> task)
    {
        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError($"로그인 실패 : {task.Exception}");
            return;
        }

        user = task.Result.User;

        Debug.Log($"새로 로그인된 Firebase UserId : {user.UserId}");

        resultData = NewUserData("GongBack");

        WriteData(
            resultData,
            "users",
            "userData",
            user.UserId
        );

        UpdateRewardUI();

        Debug.Log($"user.userid : {user.UserId}");
    }


    [Serializable]
    public class UserData
    {
        public int attendtime;
        public int cash;
        public DateTime assignDate;
        public string nickname;
        public int userlevel;
        public int coin;
        public int fame;
    }
    [Serializable]
    public class TroubleCustomerNameData
    {
        public string troubleCustomerName;
    }
    public UserData NewUserData(string wantNickname)
    {
        Debug.Log($"wantnickname : {wantNickname}");
        return new()
        {
            nickname = wantNickname,
            assignDate = DateTime.Today,
            userlevel = 1,
            cash = 0,
            coin = 0,
            fame = 0,
            attendtime = 1
        };
    }
    public TroubleCustomerNameData NewTroubleCustomerNameData(string wantName)
    {
        return new()
        {
            troubleCustomerName = wantName,
        };
    
    }
    public DatabaseReference GetFindDirectory(DatabaseReference root, params string[] directory)
    {
        if (directory is null || directory.Length == 0) return root;
        DatabaseReference currentReference = root;
        foreach (string currentChild in directory)
        {
            currentReference = currentReference.Child(currentChild);
        }
        return currentReference;
    }
   

    public void WriteData(object wantData, params string[] directory)
    {

        if (rootReference is null || wantData is null) return;
        string jsonData = JsonUtility.ToJson(wantData);
        GetFindDirectory(rootReference, directory).SetRawJsonValueAsync(jsonData).ContinueWithOnMainThread(OnTaskResult);
    }
    public void WriteData(Dictionary<string, object> changes, params string[] directory)
    {
        if (rootReference is null || changes is null) return;
        GetFindDirectory(rootReference, directory).UpdateChildrenAsync(changes).ContinueWithOnMainThread(OnTaskResult);
    }
    
    public void ReadData(Action<Task<DataSnapshot>> OnReadData, params string[] directory)
    {
        GetFindDirectory(rootReference, directory).GetValueAsync().ContinueWithOnMainThread(OnReadData);
    }

    public IEnumerator ReadDataCoroutine(Action<Task<DataSnapshot>> OnReadData, params string[] directory)
    {
        Task<DataSnapshot> readTask = GetFindDirectory(rootReference, directory).GetValueAsync();
        yield return readTask.WaitForTask();
        OnReadData?.Invoke(readTask);
            
    }

    public async Task<T> ReadDataAsync<T>(params string[] directory)
    {
        DataSnapshot currentTask = await GetFindDirectory(rootReference, directory).GetValueAsync();

        if (currentTask == null || !currentTask.Exists)
            return default;

        try
        {
            return JsonUtility.FromJson<T>(currentTask.GetRawJsonValue());
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return default;
        }
    }

    private void OnTaskResult(Task task)
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
        }

    public void NickNameChange(string nickName)
    {
        resultData.nickname = nickName;

        WriteData(
            resultData,
            "users",
            "userData",
            user.UserId
        );
    }

    public void SaveTroubleCustomerName(string customerName)
    {
        TroubleCustomerNameData data = NewTroubleCustomerNameData(customerName);

        WriteData(
            data,
            "users",
            "troubleCustomerData",
            user.UserId
        );
    }
    public void SaveRewardData(int coin, int fame)
    {
        resultData.coin = coin;
        resultData.fame = fame;

        WriteData(
            resultData,
            "users",
            "userData",
            user.UserId
        );
    }

    public void UpdateRewardUI()
    {
        GameObject coinPanel = GameObject.Find("MyCoinCount");
        GameObject famePanel = GameObject.Find("MyFameCount");

        if (coinPanel != null)
        {
            TMP_Text coinText = coinPanel.GetComponentInChildren<TMP_Text>();

            if (coinText != null)
            {
                coinText.text = resultData.coin.ToString();
            }
        }

        if (famePanel != null)
        {
            TMP_Text fameText = famePanel.GetComponentInChildren<TMP_Text>();

            if (fameText != null)
            {
                fameText.text = resultData.fame.ToString();
            }
        }


    }
    
}
