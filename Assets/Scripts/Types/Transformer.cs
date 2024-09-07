using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer: Interactable, ITransformer {
  public TransformerRecipe[] recipes;

  public Interactable Accept(ITransformable input) {
    foreach (TransformerRecipe recipe in recipes) {
      if (recipe.input.Equals((Transformable) input)) {
        input.Consume();
        Instantiate(recipe.output, ((Transformable) input).transform.position, Quaternion.identity);
        return recipe.output;
      }
    }
    return null;
  }

  public override void Interact() {
  }
}