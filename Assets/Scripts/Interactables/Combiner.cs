using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : Interactable
{
    [SerializeField]
    private List<TransformationRule> transformationRules;
    private bool toDestroy = false;

    public override Interactable Interact(Interactable other)
    {
        if (other == null) return this; // Pickup
        foreach (TransformationRule rule in transformationRules)
        {
            if (other != null && other.Is(rule.requiredItemName))
            {
                Vector3 heldItemPosition = other.transform.position;
                Destroy(other.gameObject);
                Instantiate(rule.transformedItem.gameObject, (heldItemPosition + transform.position) / 2, transform.rotation);
                toDestroy = true;
                return null;
            }
        }

        return other;
    }

    public override InteractionCheck CanInteract(Interactable other)
    {
        if (other == null) return new InteractionCheck(true, "Pick up " + this.name);

        foreach (TransformationRule rule in transformationRules)
        {
            if (other != null && other.Is(rule.requiredItemName)) return new InteractionCheck(true, "Combine " + other.name + " with " + this.name + " to create " + rule.transformedItem.name);
        }
        return new InteractionCheck(false, "");
    }

    private void Update()
    {
        if (toDestroy)
        {
            Destroy(gameObject);
        }
    }
}
