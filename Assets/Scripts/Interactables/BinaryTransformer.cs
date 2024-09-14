using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTransformer : Interactable 
{
    [SerializeField]
    private List<BinaryTransformationRule> transformationRules;
    private bool toDestroy = false;

    public override Interactable Interact(Interactable other) {
        if (other == null) return this; // Pickup
        foreach (BinaryTransformationRule rule in transformationRules) {
            if (other != null && other.Is(rule.requiredItemName)) {
                Quaternion heldItemRotation = other.transform.rotation;
                Vector3 heldItemPosition = other.transform.position;
                Destroy(other.gameObject);
                Instantiate(rule.transformedItem2.gameObject, transform.position, transform.rotation);
                toDestroy = true;
                return Instantiate(rule.transformedItem1.gameObject, heldItemPosition, heldItemRotation).GetComponent<Interactable>();
            }
        }

        return this;
    }

    public override InteractionCheck CanInteract(Interactable other) {
        if (other == null) return new InteractionCheck(true, "Pick up " + this.name);

        foreach (BinaryTransformationRule rule in transformationRules) {
            if (other != null && other.Is(rule.requiredItemName)) return new InteractionCheck(true, "Transform " + other.name + " into " + rule.transformedItem1.name);
        }
        return new InteractionCheck(false, "");
    }

    private void Update() {
        if (toDestroy) {
            Destroy(gameObject);
        }
    }
}
