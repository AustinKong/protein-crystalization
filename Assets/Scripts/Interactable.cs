using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable: MonoBehaviour, IInteractable {
  public new string name;
  public string description;

  public abstract void Interact();

  public bool Equals(Interactable other) {
    return this.name == other.name && this.description == other.description;
  }
}