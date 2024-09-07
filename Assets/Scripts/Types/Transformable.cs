using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformable : Interactable, ITransformable {
  public override void Interact() {
  }

  public void Consume() {
    Destroy(gameObject);
  }
}