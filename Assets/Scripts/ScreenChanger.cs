using UnityEngine;

public class ScreenChanger : OpenbleUIBase
{
    [SerializeField] Animator anim;
    public void ChangeStart()
    {
        anim?.SetTrigger("Out");
    }

    public void ChangeEnd()
    {
        anim?.SetTrigger("In");
    }
}
