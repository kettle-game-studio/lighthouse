using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Phone : MonoBehaviour
{
    public enum State
    {
        Signal0 = 0,
        Signal1 = 1,
        Signal2 = 2,
        Signal3 = 3,
        Message = 4,
        Rickroll = 5,
    }

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetState(State state)
    {
        animator.SetInteger("State", (int)state);
    }
}
