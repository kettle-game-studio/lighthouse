using UnityEngine;

public class Comment : Interactinator
{
    public string comment = "default_comment";

    protected override void Action(PlayerController player)
    {
        player.Say(player.gameState.GetString(comment));
    }
}
