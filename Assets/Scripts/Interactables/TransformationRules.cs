[System.Serializable]
public class TransformationRule {
  public string requiredItemName;
  public Interactable transformedItem;
  public TransformationRule(string requiredItemName, Interactable transformedItem) {
    this.requiredItemName = requiredItemName;
    this.transformedItem = transformedItem;
  }
}

[System.Serializable]
public class BinaryTransformationRule {
  public string requiredItemName;
  public Interactable transformedItem1;
  public Interactable transformedItem2;
  public BinaryTransformationRule(string requiredItemName, Interactable transformedItem1, Interactable transformedItem2) {
    this.requiredItemName = requiredItemName;
    this.transformedItem1 = transformedItem1;
    this.transformedItem2 = transformedItem2;
  }
}