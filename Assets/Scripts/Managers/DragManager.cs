using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
  public static DragManager instance { get; private set; }
  private IDraggable currentlyDragged;
  private Camera mainCamera;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
    mainCamera = Camera.main;
  }

  void Update()
  {
    // Unity destroy object is spaghetti code, this makes sure the null is really a null
    if (currentlyDragged == null || currentlyDragged.Equals(null)) currentlyDragged = null;

    // Begin dragging
    if (Input.GetMouseButtonDown(0)) TryDrag();
    // Dragging
    if (Input.GetMouseButton(0)) {
      if (currentlyDragged != null) currentlyDragged.OnDrag();
      else TryDrag(); // Check if the object to be dragged still exists, if not, try to drag another
    }
    // End dragging
    if (Input.GetMouseButtonUp(0) && currentlyDragged != null)
    {
      currentlyDragged.OnEndDrag();
      currentlyDragged = null;
    }
  }

  private void TryDrag()
  {
    Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

    if (hit.collider != null)
    {
      IDraggable draggable = hit.collider.GetComponent<IDraggable>();
      if (draggable != null)
      {
        currentlyDragged = draggable;
        currentlyDragged.OnBeginDrag();
      }
    }
  }

  public bool IsDragging()
  {
    return currentlyDragged != null;
  }
}
