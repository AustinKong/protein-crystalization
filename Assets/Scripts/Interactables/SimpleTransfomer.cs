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
}
