using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutlineFx;

public abstract class Interactable : MonoBehaviour
{
    protected Rigidbody2D rb;
    private SpriteRenderer sr;
    private Outline outlineScript;

    [SerializeField]
    private new string name;
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

    public abstract Interactable Interact(Interactable other);

    public bool Is(string name)
    {
        return this.name == name;
    }
}
