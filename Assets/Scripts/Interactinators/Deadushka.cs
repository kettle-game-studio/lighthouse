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
        string comment = "";
        for (int i = 0; i < unlockRequests.Length; ++i)
        {
            if (unlockRequests[i])
            {
                comment += $"{unlockTargets[i].title}: {unlockTargets[i].comment}\n";
                unlockTargets[i].target.locked = false;
                unlockRequests[i] = false;
            }
        }
        if (comment.Length == 0)
            comment = "Nothing to say";

        player.Say(comment, "Deadushka");
    }
}
