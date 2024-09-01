using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable: MonoBehaviour {
  private bool isHeld = false;
  private Vector3 offset;
  private Vector3 lastMousePosition;
  private Rigidbody2D rb;

  const float velocityCap = 10;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update() {
    if (isHeld) {
      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
      lastMousePosition = mousePosition;
    }
  }

  void FixedUpdate() {
    if (isHeld) {
      Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
      rb.velocity = (mousePosition - transform.position) / Time.fixedDeltaTime;
    } 
  }

  private void OnMouseDown() {
    isHeld = true;
    offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
  }

  private void OnMouseUp() {
    isHeld = false;
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    Vector2 velocity = (mousePosition - lastMousePosition) / Time.deltaTime;
    rb.velocity = velocity.magnitude > velocityCap ? velocity.normalized * velocityCap : velocity;
  }
}