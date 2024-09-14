using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; } // Singleton instance
    public TMP_Text cursorText; // Reference to the Text (TMP) UI element
    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        cursorText.text = ""; // Start with no text visible
        cursorText.gameObject.SetActive(false); // Disable text visibility initially
    }

    private void Update()
    {
        FollowCursor();
    }

    // Update the position of the UI text to follow the cursor
    private void FollowCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        cursorText.transform.position = mousePosition + new Vector3(10, -20, 0); // Offset slightly from the cursor
    }

    // Show the interaction text when an interaction is possible
    public void ShowTooltip(string message)
    {
        cursorText.text = message;
        cursorText.gameObject.SetActive(true);
    }

    // Hide the interaction text
    public void HideTooltip()
    {
        cursorText.text = "";
        cursorText.gameObject.SetActive(false);
    }
}
