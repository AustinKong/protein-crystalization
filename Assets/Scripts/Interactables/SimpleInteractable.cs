using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only function is to be picked up and dropped
public class SimpleInteractable : Interactable
{
    public override Interactable Interact(Interactable other)
    {
        if (other == null) return this; // Pickup
        else return other; // Don't pickup if holding something
    }
}
