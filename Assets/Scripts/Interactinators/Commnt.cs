using UnityEngine;

public class Comment : Interactinator
{
    public string comment = "I cant touch it";

    protected override void Action(PlayerController player)
    {
        player.Say(comment);
    }
}
