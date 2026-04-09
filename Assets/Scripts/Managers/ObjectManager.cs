using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

// using System.Runtime.InteropServices.WindowsRuntime;
// using Unity.Mathematics;
using UnityEngine;



public class ObjectManager : ManagerBase
{
    // [SerializeField] public PoolSetting[] testSettings;
    // 바꾸는 게 아닌 읽기전용으로만
    readonly string[] globalPoolSettings =
    {
        "GlobalCharacterPool",
        "GlobalControllerPool", 
        "GlobalEffectPool",
        "GlobalObjectPool",
        "GlobalUIPool"
    };

    List<PoolRequest> loadedPoolRequests = new();

    static Dictionary<string, ObjectPoolModule> poolDictionary = new();
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        RegistrationPool(globalPoolSettings);
        

        InitilizePool();                                                                                                                

        yield return null;
    }

    protected override void OnDisconnected()
    {
        
    }
    public static GameObject CreateObject(string wantName, Vector3 position)
    {
        GameObject result = CreateObject(wantName);
        if(result) result.transform.position = position;
        return result;
    }
    public static GameObject CreateObject(GameObject prefab, Vector3 position)
    {
        GameObject result = CreateObject(prefab);
        if(result) result.transform.position = position;
        return result;
    }
    public static GameObject CreateObject(string wantName, Vector3 position, Quaternion rotation) 
    {
        GameObject result = CreateObject(wantName);
        if (result)
        {
            result.transform.position = position;
            result.transform.rotation = rotation;
        } 
            
        return result;
    }
    public static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation) 
    {
        GameObject result = CreateObject( prefab);
        if (result)
        {
            result.transform.position = position;
            result.transform.rotation = rotation;
        } 
            
        return result;
    }

    public static GameObject CreateObject(string wantName, Transform parent = null)
    {
        GameObject result = null;

        wantName = wantName.ToLower();

        if (poolDictionary.TryGetValue(wantName, out ObjectPoolModule pool))
        {
            result = pool.CreateObject(parent);
        }
        else
        {
            if (DataManager.TryLoadDataFile<GameObject>(wantName, out GameObject prefab))
            {
                if (prefab) result = Instantiate(prefab, parent);
            }
            
        }
        if (!result) UIManager.ClaimErrorPopup(SystemMessage.ObjectNameNotFound(wantName));


        RegistrationObject(result);
        
        return result;     
    } 
    public static GameObject CreateObject(GameObject prefab, Transform parent = null)
    {
        if (prefab == null) return null;

        GameObject result = Instantiate(prefab, parent);
        RegistrationObject(result);
        return result;
    }

        
    public static GameObject CreateObject(string wantName, Transform parent, Vector3 position, Space space = Space.Self)
    {
        
        GameObject result = CreateObject( wantName);
        if (result)
        {
            switch (space)
            { 
                case Space.World:
                    result.transform.position = position;
                    break;
                case Space.Self:
                    result.transform.localPosition = position;
                    break;
            }
            
        } 
            
        return result;
    }
    public static GameObject CreateObject(GameObject prefab, Transform parent, Vector3 position, Space space = Space.Self)
    {
        
        GameObject result = CreateObject(prefab);
        if (result)
        {
            switch (space)
            { 
                case Space.World:
                    result.transform.position = position;
                    break;
                case Space.Self:
                    result.transform.localPosition = position;
                    break;
            }
            
        } 
            
        return result;
    }

    public static GameObject CreateObject(string wantName, Transform parent, Vector3 position, Quaternion rotation, Space space = Space.Self)
    {
        GameObject result = CreateObject(wantName);
        switch (space)
        {
            case Space.World:
                result.transform.position = position;
                result.transform.rotation = rotation;
                break;
            case Space.Self:
                result.transform.localPosition = position;
                result.transform.localRotation = rotation;
                break;
        }

        return result;
    }
    public static GameObject CreateObject(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation, Space space = Space.Self)
    {
        GameObject result = CreateObject( prefab);
        switch (space)
        {
            case Space.World:
                result.transform.position = position;
                result.transform.rotation = rotation;
                break;
            case Space.Self:
                result.transform.localPosition = position;
                result.transform.localRotation = rotation;
                break;
        }

        return result;
    }
    // scale
    public static GameObject CreateObject(string wantName, Transform parent, Vector3 position, Quaternion rotation, Vector3 scale, Space space = Space.Self)
    {
        GameObject result = CreateObject(wantName);
        if (result)
        { 
            result.transform.position = position;
            result.transform.rotation = rotation;
            result.transform.localScale = scale;
            
            
        }
        return result;
    }

    public static GameObject CreateObject(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation, Vector3 scale, Space space = Space.Self)
    {
        GameObject result = CreateObject(prefab);
        switch (space)
        {
            case Space.World:
                result.transform.position = position;
                result.transform.rotation = rotation;
                result.transform.localScale = scale;
                break;
            case Space.Self:
                result.transform.localPosition = position;
                result.transform.localRotation = rotation;
                result.transform.localScale = scale;

                float scaleScaleX = scale.x * (result.transform.localScale.x / result.transform.lossyScale.x);
                float scaleScaleY = scale.y * (result.transform.localScale.y / result.transform.lossyScale.y);
                float scaleScaleZ = scale.z * (result.transform.localScale.z / result.transform.lossyScale.z);
                result.transform.localScale = new Vector3(scaleScaleX, scaleScaleY, scaleScaleZ);
                break;
        }

        return result;
    }

    public void RegistrationPool(string poolName)
    {

        poolName = poolName.ToLower();

        PoolRequest currentRequest = DataManager.LoadDataFile<PoolRequest>(poolName);
        if (currentRequest == null) return;
        if (currentRequest.settings == null) return;
        loadedPoolRequests.Add(currentRequest);
        foreach (PoolSetting currentSetting in currentRequest.settings)
        {
            string currentName = currentSetting.poolName.ToLower();
            GameObject currentPrefab = currentSetting.target;
            if (currentPrefab == null) continue; // 없으면 다음으로 넘어감
            if (poolDictionary.ContainsKey(currentName)) continue; // 이 이름을 갖고 있다면
            poolDictionary.Add(currentName, new (currentSetting));
        }
    }

    public void RegistrationPool(params string[] poolNames)
    {
        foreach (string poolName in poolNames)
        {
            RegistrationPool(poolName);
        }
        
    }
    public static void RegistrationObject(GameObject target)
    {
        if (target)
        {
            foreach (var current in target.GetComponentsInChildren<IFunctionable>())
            {
                current.RegistrationFunctions();
            }

        }

    }

    public static void DestroyObject(GameObject target)
    {
        if (!target) return;
        UnRegistrationObject(target);
        if (target.TryGetComponent(out PooledObject pool))
        {
            pool.OnEnqueue();
        }
        else
        {
            Destroy(target);
        }
            
    }

    public static void UnRegistrationObject(GameObject target)
    {
        if (target)
        {
            foreach (var current in target.GetComponentsInChildren<IFunctionable>())
            {
                current.UnregistrationFunctions();
            }

        }

    }

    public void InitilizePool()
    {
        foreach (ObjectPoolModule currentPool in poolDictionary.Values)
        { 
            currentPool?.Initialize();
        }
    }
}
