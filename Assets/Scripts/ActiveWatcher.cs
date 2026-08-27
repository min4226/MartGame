using UnityEngine;

public class ActiveWatcher : MonoBehaviour
{
    

    private void OnEnable()
    {
        Debug.Log(
            $"🔥 InputField 활성화됨\n" +
            $"Object: {gameObject.name}\n" +
            $"StackTrace:\n{System.Environment.StackTrace}"
        );
    }

}
