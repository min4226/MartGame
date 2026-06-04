using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed = 3f;
    void Start()
    {
        
    }

   
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
