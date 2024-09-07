using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class Draggable: MonoBehaviour, IDraggable {
  private Vector2 offset;
  private Vector2 lastMousePosition;
  private Camera mainCamera;
  private Rigidbody2D rb;
  private bool isHeld = false;

  const float VELOCITY_CAP = 10;

  void Start() {
    mainCamera = Camera.main;
    rb = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate() {
    if (!isHeld) return;
    // Applies forces to rigidbody
    Vector2 mousePosition = (Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
    rb.velocity = (mousePosition - (Vector2) transform.position) / Time.fixedDeltaTime;
  }

  public void OnBeginDrag() {
    isHeld = true;
    offset = transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
  }

  public void OnDrag() {
    Vector2 mousePosition = (Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
    lastMousePosition = mousePosition;
  }

  public void OnEndDrag() {
    isHeld = false;
    // Maintain velocity when releasing object
    Vector2 mousePosition = (Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
    Vector2 velocity = (mousePosition - (Vector2) lastMousePosition) / Time.deltaTime;
    rb.velocity = velocity.magnitude > VELOCITY_CAP ? velocity.normalized * VELOCITY_CAP : velocity;
  }
}