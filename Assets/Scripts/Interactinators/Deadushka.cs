using System;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Deadushka : Interactinator
{
    public TextMeshPro text;
    Animator animator;
    float nextBlink = 0;
    float nextSmoke = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        var tooltip = player.gameState.nextThingToUnlock();
        if (tooltip != null)
        {
            player.Say(player.gameState.GetString($"{tooltip}_request"), "You");
            player.Say(player.gameState.GetString($"{tooltip}_response"), "Deadushka");
            saySomething = true;
        }

        if (!saySomething)
        {
            player.Say("Say something", "You");
            player.Say("Nothing to say", "Deadushka");
        }

    }
}
