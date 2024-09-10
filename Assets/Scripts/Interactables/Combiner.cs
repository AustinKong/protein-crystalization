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

    private void Update()
    {
        if (toDestroy)
        {
            Destroy(gameObject);
        }
    }
}
