using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CupboardItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;

    public GameObject worldPrefab;

    public void OnBeginDrag(PointerEventData eventData) {
        startPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = GetWorldPositionUnderMouse();
    }

    public void OnEndDrag(PointerEventData eventData) {
        // Instantiate a copy in the game world
        Vector3 worldPosition = GetWorldPositionUnderMouse();
        worldPosition.z = 0;
        Instantiate(worldPrefab, worldPosition, Quaternion.identity);
        transform.position = startPosition;
        transform.SetParent(originalParent);
    }

    private Vector3 GetWorldPositionUnderMouse()
    {
        // Convert the mouse position from screen space to world space
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;  // Adjust this value depending on your game setup
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
