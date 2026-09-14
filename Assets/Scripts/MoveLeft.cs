using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PoliceExit"))
        {
            gameObject.SetActive(false);

            GameManager.Instance.CustomerSpawn.StartNextCustomer();
        }
    }
}
