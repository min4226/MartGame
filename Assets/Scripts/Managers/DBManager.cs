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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DBManager : ManagerBase
{
    FirebaseAuth authentication;
    FirebaseUser user;
    DatabaseReference rootReference;

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(InitializeFireBase);
        ;
        
        yield return null;
    }

    protected override void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    void InitializeFireBase(Task<DependencyStatus> task)
    {
        if (task.Result == DependencyStatus.Available)
        {
            authentication = FirebaseAuth.DefaultInstance;
            user = authentication.CurrentUser;
            rootReference = FirebaseDatabase.DefaultInstance.RootReference;

            GuestLogin();
            
            Debug.Log("firebase 성공");
        }
        else

        {
            Debug.LogError($"firebase 실패 : {task.Exception}");
        }
    }
    public TMPro.TMP_InputField nickNameInput;
    public void MakeUserData(string newUserName)
    {
        WriteData(NewUserData(nickNameInput.text), "users", "userData", user.UserId);
    }

    public async void GuestLogin()

    {
        // 인증기가 존재하지 않을 경우
        if (authentication is null) return;
        if (user is not null)
        {
            Debug.LogError($"이미 있는 로그인입니다.({user.IsValid()}, {user.UserId})");
            UserData resultData = await ReadDataAsync<UserData>("users", "userData" , user.UserId);
            if (resultData is not null)
            {
                Debug.Log(resultData.nickname);
            }
            else
            {
                WriteData(NewUserData("GongBack"), "users", "userData", user.UserId);
            }
                return;
        }

        await authentication.SignInAnonymouslyAsync().ContinueWithOnMainThread(OnLoginResult);

    }

    void OnLoginResult(Task<AuthResult> task)
    {
        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError($"로그인 실패 : {task.Exception}");
            return;
        }

        user = task.Result.User;

        WriteData(
            NewUserData("GongBack"),
            "users",
            "userData",
            user.UserId     
        );

        Debug.Log($"로그인 결과 : {user.UserId}");
    }
    [Serializable]
    public class UserData
    {
        public int attendtime;
        public int money;
        public DateTime assignDate;
        public string nickname;
        public int userlevel;
    }

    public UserData NewUserData (string wantNickname) => new()
    {
        nickname = wantNickname,

        assignDate = DateTime.Today,
        userlevel = 1,
        money = 5000,
        attendtime = 1
    };

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

    public async Task<T>  ReadDataAsync<T>(params string[] directory)
    {
        DataSnapshot currentTask = await GetFindDirectory(rootReference, directory).GetValueAsync();
        if(currentTask is null) return default;
        if(!currentTask.Exists) return default;
        try
        {
            if (currentTask.HasChildren)
            {
            JsonUtility.FromJson<T>(currentTask.GetRawJsonValue());
            }
            return (T)System.Convert.ChangeType(currentTask.Value, typeof(T));
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
    }
