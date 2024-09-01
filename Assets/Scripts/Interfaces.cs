using System.Collections;
using System.Collections.Generic;

// Something that can be stored
public interface IStorable {
    void Store();
    void Retrieve();
}

// Inventory that stores IStorable(s)
public interface IInventory {
    void AddToInventory(IStorable item);
    void RemoveFromInventory(IStorable item);
    IStorable[] GetInventory();
}