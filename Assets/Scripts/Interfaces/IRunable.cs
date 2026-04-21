using UnityEngine;

public interface IRunable
{
    public void MoveToDestination(Vector3 destination, float tolerance);
    public void MoveToDirection(Vector3 direction);
    public void StopMovement();
}
