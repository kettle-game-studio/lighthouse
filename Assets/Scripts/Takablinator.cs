using UnityEngine;

public class Takablinator : Interactinator
{
    public override void Interact(PlayerController player)
    {
        player.Take(transform);
    }
}
