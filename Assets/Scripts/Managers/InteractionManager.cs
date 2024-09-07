using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
  public static InteractionManager instance { get; private set; }
  private Camera mainCamera;

  const float CIRCLECAST_RADIUS = 1f;

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


  void Update() {
    if (DragManager.instance.IsDragging()) {
      CheckForInteractables();
    }
  }

  private void CheckForInteractables() {
    Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    Collider2D[] hits = Physics2D.OverlapCircleAll(mousePosition, CIRCLECAST_RADIUS);
    List<Interactable> interactables = new List<Interactable>();
    List<ITransformer> transformers = new List<ITransformer>();
    List<ITransformable> transformables = new List<ITransformable>();

    foreach (Collider2D collider in hits) {
      Interactable interactable = collider.GetComponent<Interactable>();
      if (interactable != null) {
        interactables.Add(interactable);
        // FIXME: Maybe relocate interact call here
        interactable.Interact();
        if (interactable is ITransformer) {
          transformers.Add(interactable as ITransformer);
        }
        if (interactable is ITransformable) {
          transformables.Add(interactable as ITransformable);
        }
      }
    }

    DoTransforms(transformables, transformers);
  }

  private void DoTransforms(List<ITransformable> transformables, List<ITransformer> transformers) {
    foreach (ITransformable transformable in transformables) {
      foreach (ITransformer transformer in transformers) {
        transformer.Accept(transformable);
      }
    }
  }
}