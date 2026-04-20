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
    public AudioSource source;
    public AudioClip[] sounds;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetState(State state, int audioIndex = -1, bool loop = false)
    {
        if (audioIndex >= 0)
        {
            source.loop = loop;
            source.clip = sounds[audioIndex];
            source.Play();
        }
        animator.SetInteger("State", (int)state);
    }
}
