using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTransfomer : Interactable
{
    [SerializeField]
    private List<TransformationRule> transformationRules;

    public override Interactable Interact(Interactable other)
    {
        if (other == null) return this; // Pickup

        foreach (TransformationRule rule in transformationRules)
        {
            if (other != null && other.Is(rule.requiredItemName))
            {
                Quaternion heldItemRotation = other.transform.rotation;
                Vector3 heldItemPosition = other.transform.position;
                Destroy(other.gameObject);
                return Instantiate(rule.transformedItem.gameObject, heldItemPosition, heldItemRotation).GetComponent<Interactable>();
            }
        }

        return other;
    }

    public override InteractionCheck CanInteract(Interactable other)
    {
        if (other == null) return new InteractionCheck(true, "Pick up " + this.name);

        foreach (TransformationRule rule in transformationRules)
        {
            if (other != null && other.Is(rule.requiredItemName)) return new InteractionCheck(true, "Transform " + other.name + " into " + rule.transformedItem.name);
        }
        return new InteractionCheck(false, "");
    }
}
