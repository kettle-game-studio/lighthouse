using System;
using System.Linq;
using TMPro;
using UnityEngine;

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
    bool[] unlockRequests;

    public TextMeshPro text;

    void Start()
    {
        unlockRequests = new bool[unlockTargets.Length];
        for (int i = 0; i < unlockTargets.Length; ++i)
        {
            int iCache = i;
            unlockTargets[i].target.unlockRequest = player => unlockRequests[iCache] = true;
        }
    }

    protected override void Action(PlayerController player)
    {
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
