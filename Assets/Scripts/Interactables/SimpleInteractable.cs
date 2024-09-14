using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only function is to be picked up and dropped
public class SimpleInteractable : Interactable
{
    public override Interactable Interact(Interactable other)
    {
        if (other == null) return this; // Pickup
        return other; // Don't pickup if holding something
    }

    public override InteractionCheck CanInteract(Interactable other) {
        return new InteractionCheck(other == null, "Pick up " + this.name);
    }
}
