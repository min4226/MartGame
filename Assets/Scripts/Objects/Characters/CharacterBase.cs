using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    ControllerBase _controller;
    public ControllerBase Controller => _controller;

    protected Vector3 _lookRotation;
    public Vector3 LookRotation => _lookRotation;
    public ControllerBase Possessed(ControllerBase from)
    {
        if (_controller) Unpossessed();
        _controller = from;
        OnPossessed(Controller);
        return _controller;
    }

    public virtual void OnPossessed(ControllerBase newController)
    { 
        
    }

    public void Unpossessed()
    {
        if(Controller) OnUnPossessed(_controller);
        _controller = null;
    }

    public bool Unpossessed(ControllerBase oldController)
    {
        if (Controller != oldController) return false;
        Unpossessed();
        return true;
    }


    public virtual void OnUnPossessed(ControllerBase oldController)
    {

    }
}
