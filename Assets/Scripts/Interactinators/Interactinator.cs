using System;
using UnityEngine;

public class Interactinator : MonoBehaviour
{
    public bool locked = false;
    public string lockText = "";
    public Action<PlayerController> unlockRequest = null;

    public void Interact(PlayerController player)
    {
        if (locked) {
            player.Say(lockText);
            if (unlockRequest != null)
                unlockRequest(player);
        }
        else
            Action(player);
    }

    protected virtual void Action(PlayerController player)
    {
        player.Say("interacted!!", name);
    }
}
