using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField]
    private List<GameObject> inventory = new List<GameObject>();
    [SerializeField]
    private GameObject inventoryUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        PopulateInventory();
    }

    private void PopulateInventory() {
        foreach (GameObject item in inventory) {
            GameObject button = new GameObject(item.name, typeof(Button), typeof(Image));
            button.GetComponent<Image>().sprite = item.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            button.transform.SetParent(inventoryUI.transform);
            button.GetComponent<Button>().onClick.AddListener(() => HandlePickup(item));
        }
    }

    private void HandlePickup(GameObject item) {
        Debug.Log(InteractionManager.Instance.HasHeldItem());
        if (InteractionManager.Instance.HasHeldItem()) return;
        Interactable instance = Instantiate(item, (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).GetComponent<Interactable>();
        InteractionManager.Instance.SetHeldItem(instance);
    }
}
