using System.Collections;
using System.Collections.Generic;

public interface ITransformable {
    void Consume();
}

public interface ITransformer {
    Interactable Accept(ITransformable input);
}

public interface IInteractable {
    void Interact();
}

public interface IDraggable {
    // Optional methods for drag interactions
    void OnBeginDrag();
    void OnDrag();
    void OnEndDrag();
}