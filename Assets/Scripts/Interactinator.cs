using UnityEngine;

public class Interactinator : MonoBehaviour
{
    public virtual void Interact(PlayerController player)
    {
        Debug.Log($"{this} interacted!!");
    }
}
