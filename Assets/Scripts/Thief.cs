using UnityEngine;

public class Thief : MonoBehaviour
{
    private Transform targetPoint;
    private Transform exitPoint;

    [SerializeField] private float moveSpeed = 2f;

    private bool isGoingToExit = false;

    public void Init(Transform target, Transform exit)
    {
        targetPoint = target;
        exitPoint = exit;
    }

    private void Update()
    {
        if (!isGoingToExit)
        {
            MoveTo(targetPoint);
        }
        else
        {
            MoveTo(exitPoint);
        }
    }

    private void MoveTo(Transform target)
    {
        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            if (!isGoingToExit)
            {
                isGoingToExit = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}