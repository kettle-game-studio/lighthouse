using System;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Deadushka : Interactinator
{
    public TextMeshPro text;
    public AudioClip smokeClip;
    public AudioClip[] yesClips;
    public AudioClip[] noClips;
    public AudioClip[] somethingClips;
    Animator animator;
    float nextBlink = 0;
    float nextSmoke = 0;
    AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time > nextSmoke)
        {
            nextSmoke = Time.time + UnityEngine.Random.Range(10, 30);
            animator.SetTrigger("Smoke");
        }
        if (Time.time > nextBlink)
        {
            nextBlink = Time.time + UnityEngine.Random.Range(0.5f, 5);
            animator.SetTrigger("Blink");
        }
    }

    private AudioClip randomClip(AudioClip[] clips)
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
    protected override void Action(PlayerController player)
    {
        animator.SetTrigger("Speak");
        bool saySomething = false;
        var tooltip = player.gameState.nextThingToUnlock();
        if (tooltip != null)
        {
            audioSource.clip = randomClip(player.gameState.HasString($"{tooltip}_comment") ? noClips:yesClips);
            audioSource.Play();
            player.Say(player.gameState.GetString($"{tooltip}_request"), "You");
            player.Say(player.gameState.GetString($"{tooltip}_response"), this.tooltip);
            saySomething = true;
        }

        if (!saySomething)
        {
            audioSource.clip = randomClip(somethingClips);
            audioSource.Play();
            player.Say("Say something", "You");
            player.Say("Nothing to say", tooltip);
        }
    }

    public void StartSmoke(AnimationEvent ev)
    {
        if(audioSource.isPlaying) return;
        audioSource.clip = smokeClip;
        audioSource.Play();
    }

    public void TakeSound()
    {
        audioSource.clip = randomClip(yesClips);
        audioSource.Play();
    }
}
