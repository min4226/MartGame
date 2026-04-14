using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
public class DataManager : ManagerBase
{
    static Dictionary<System.Type, Dictionary<string, Object>> dataDictionary = new();
    public override int LoadingCount
    {
        get 
        {
            var task = Addressables.LoadResourceLocationsAsync("Global");
            var result = task.WaitForCompletion();
            int count = result.Count;
            task.Release();
            return count;
        }
    }
   
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        

        UIBase loading = UIManager.ClaimGetUI(UIType.Loading);
        IProgress<int> progressUI = loading as IProgress<int>;
        IStatus<string> statusUI = loading as IStatus<string>;

        int loaded = 0;
        int total = LoadingCount;
        string loadString = "Load Data";

        System.Action ProgressOnLoad = () => 
        {
            loaded ++;
            progressUI?.AddCurrent(1);
            statusUI?.SetCurrentStatus($"{loadString}{loaded}/{total} 로딩 중입니다");
        
        };
        loadString = "Load Game Objects";
        yield return LoadAllFromAssetBundle<GameObject>("Global", ProgressOnLoad).WaitForTask();
        loadString = "Load Pool Requests";                                       
        yield return LoadAllFromAssetBundle<PoolRequest>("Global", ProgressOnLoad).WaitForTask();

        /*GameObject prefab = LoadDataFile<GameObject>("Square");
        Instantiate(prefab, Random.insideUnitCircle * 3.0f, Random.rotation);*/


        // LoadFileFromAssetBundle<GameObject>("Classic/Prefabs/Square.Prefab");


        if (TryGetFileFromResources("Prefabs/Square", out Sprite sprite))
        {

        }
        yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    bool TryGetFileFromResources<T>(string path, out T result) where T : Object
    {
        result = Resources.Load<T>(path);
        return result != null;
    }

    public static void SaveDataFile<T>(T target) where T : Object
    {
        if (target == null) return;
        Dictionary<string, Object> innerDictionary;

        if (!dataDictionary.TryGetValue(typeof(T), out innerDictionary))
        {
            innerDictionary = new();
            dataDictionary.Add(typeof(T), innerDictionary);
        }
        innerDictionary.TryAdd(target.name.ToLower(), target);
        
    }

    public static T GetDataFromDictionary<T>(string fileName) where T : Object
    {
        if (string.IsNullOrEmpty(fileName)) return null;

        fileName = fileName.ToLower();
        if (dataDictionary.TryGetValue(typeof(T), out Dictionary<string, Object> innerDictinary))
        {
            if (innerDictinary.TryGetValue(fileName, out Object result))
            {
                return result as T;
            }
        }
        return null;
    }

    public static T LoadDataFile<T>(string fileName) where T : Object
    {
        T result =  GetDataFromDictionary<T>(fileName);
        if(!result) UIManager.ClaimErrorPopup(SystemMessage.FileNameNotFound(fileName));
        return result;
    }
    public static bool TryLoadDataFile<T>(string fileName, out T result) where T : Object
    {
        result = GetDataFromDictionary<T>(fileName);
        return result;
    }

    public async Task LoadAllFromAssetBundle<T>(string label, System.Action actionForEachLoad) where T : Object
    {
        var finder = Addressables.LoadAssetsAsync<T>(label, (T loaded) => 
        {
            SaveDataFile(loaded);
            actionForEachLoad();
        
        }); // 람다식
        Task result = finder.Task;
        await result;
        finder.Release();

    }
    

    async void LoadFileFromAssetBundle<T>(string address) where T : Object
    {
        var finder = Addressables.LoadAssetAsync<T>(address);
        await finder.Task; // 작업이 끝날 때까지 기다리기
        SaveDataFile(finder.Result);
        
    }
    
}
