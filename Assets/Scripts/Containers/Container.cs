using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container: MonoBehaviour, IInventory {
  private IStorable inventory = null;

  public void AddToInventory(IStorable item) {
    inventory = item;
  }

  public void RemoveFromInventory(IStorable item) {
    inventory = item;
  }

  public IStorable[] GetInventory() {
    return new IStorable[] { inventory };
  }
}