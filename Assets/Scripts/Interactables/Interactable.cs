using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutlineFx;

public struct InteractionCheck {
    public InteractionCheck(bool canInteract, string description) {
        this.canInteract = canInteract;
        this.description = description;
    }
    public bool canInteract;
    public string description;
}

public abstract class Interactable : MonoBehaviour
{
    protected Rigidbody2D rb;
    private SpriteRenderer sr;
    private Outline outlineScript;

    public new string name;
    private bool isHighlighted = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        outlineScript = transform.GetChild(0).gameObject.AddComponent<OutlineFx.OutlineFx>();
        outlineScript.enabled = false;
    }

    public void SetHighlight(bool highlight) {
        if (isHighlighted != highlight) {
            isHighlighted = highlight;
            outlineScript.enabled = highlight;
        }
    }

    public void Move(Vector3 position)
    {
        rb.MovePosition(position);
    }

    /*
        Possible combinations include:
        - Pickup: return this
        - Drop: return null
        - Do nothing: return other
        - Combine: return new object, destory this and other
        - Transform: return new object, destroy other
    */
    public abstract Interactable Interact(Interactable other);
    public abstract InteractionCheck CanInteract(Interactable other);

    public bool Is(string name)
    {
        return this.name == name;
    }
}
