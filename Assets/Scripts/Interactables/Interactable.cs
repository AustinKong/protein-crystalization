using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField]
    private new string name;
    private bool isHighlighted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void SetHighlight(bool highlight) {
        if (isHighlighted != highlight) {
            isHighlighted = highlight;
            sr.color = isHighlighted ? Color.red : Color.white;
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
