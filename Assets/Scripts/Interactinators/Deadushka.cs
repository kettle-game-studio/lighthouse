using System;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Deadushka : Interactinator
{
    [Serializable]
    public struct unlockTarget
    {
        public string title;
        public Interactinator target;
        public string comment;
    }

    public unlockTarget[] unlockTargets;
    public TextMeshPro text;
    bool[] unlockRequests;
    Animator animator;
    float nextBlink = 0;
    float nextSmoke = 0;

    void Start()
    {
        animator = GetComponent<Animator>();

        unlockRequests = new bool[unlockTargets.Length];
        for (int i = 0; i < unlockTargets.Length; ++i)
        {
            int iCache = i;
            unlockTargets[i].target.unlockRequest = player => unlockRequests[iCache] = true;
        }
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

    protected override void Action(PlayerController player)
    {
        animator.SetTrigger("Speak");
        bool saySomething = false;
        for (int i = 0; i < unlockRequests.Length; ++i)
        {
            if (unlockRequests[i])
            {
                player.Say($"Can I exterminate {unlockTargets[i].title}?", "You");
                player.Say(unlockTargets[i].comment, "Deadushka");
                unlockTargets[i].target.locked = false;
                unlockRequests[i] = false;
                saySomething = true;
            }
        }
        if (!saySomething)
        {
            player.Say("Say something", "You");
            player.Say("Nothing to say", "Deadushka");
        }

    }
}
