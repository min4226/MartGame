using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Transform[] targetPoints;
    private Transform exitPoint;

    private int currentIndex;
    private bool isGoingToExit;

    public void Init(Transform[] targets, Transform exit)
    {
        targetPoints = targets;
        exitPoint = exit;

        currentIndex = 0;
        isGoingToExit = false;
    }

    private void Update()
    {
        if (isGoingToExit)
        {
            MoveTo(exitPoint);
            return;
        }

        if (targetPoints == null || targetPoints.Length == 0)
            return;

        MoveTo(targetPoints[currentIndex]);
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
            if (isGoingToExit)
            {
                Destroy(gameObject);
                return;
            }

            currentIndex++;

            if (currentIndex >= targetPoints.Length)
            {
                isGoingToExit = true;
            }
        }
    }

    public void Catch()
    {
        Debug.Log("도둑을 잡았습니다!");

        Destroy(gameObject);
    }
}