using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Camera mainCamera;
    private const float INTERACTION_RADIUS = 1.1f;

    private List<Interactable> nearbyInteractables = new List<Interactable>();
    private Interactable heldItem;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update() {
        // Must be in this order
        ClearHighlights();
        GetNearbyInteractables(2);
        HighlightInteractables();

        MoveHeldItem();
        ManageInteraction();
    }

    private void ManageInteraction() {
        if (Input.GetMouseButtonDown(0)) {
            // Drop held item if there is nothing nearby
            if (nearbyInteractables.Count == 1 && heldItem != null) {
                heldItem = null;
                return;
            }

            if (nearbyInteractables.Count > 0) {
                if (heldItem != null) {
                    // Interact with closest interactable if held item is not null
                    Interactable output = nearbyInteractables[1].Interact(heldItem); // Second closest interactable, cause the closest is held item
                    heldItem = output;
                } else {
                    // Pick up closest interactable if held item is null
                    Interactable closestInteractable = nearbyInteractables[0];
                    heldItem = closestInteractable.Interact(heldItem);
                }
            }
        }
    }

    // Moves held item to mouse position
    private void MoveHeldItem() {
        if (heldItem != null) {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            heldItem.Move(new Vector3(mousePosition.x, mousePosition.y, 0));
        }
    }

    // Gets the nearest N interactables
    private void GetNearbyInteractables(int count) {
        Vector3 mousePosition = (Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, INTERACTION_RADIUS);

        nearbyInteractables.Clear();

        foreach (Collider2D collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null) nearbyInteractables.Add(interactable);
        }

        nearbyInteractables.Sort((a, b) => 
            ((a.transform.position - mousePosition).sqrMagnitude)
            .CompareTo((b.transform.position - mousePosition).sqrMagnitude));

        // Trim to leave nearest N interactables
        while (nearbyInteractables.Count > count) {
            nearbyInteractables.RemoveAt(nearbyInteractables.Count - 1);
        }
    }

    // Clears all highlights
    private void ClearHighlights()
    {
        foreach (Interactable interactable in nearbyInteractables)
        {   
            // Workaround unity's weird destruction behaviour
            if (interactable != null && !interactable.Equals(heldItem)) interactable.SetHighlight(false);
        }
    }

    // Highlights N nearest interactables
    private void HighlightInteractables()
    {
        foreach (Interactable interactable in nearbyInteractables)
        {
            interactable.SetHighlight(true);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(mousePosition, INTERACTION_RADIUS);
    }
}
