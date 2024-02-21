using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    [SerializeField] BoolVariable engaged;

    [Header("Sprites")]
    [SerializeField] Texture2D sprite;

    [Header("Outline")]
    [SerializeField] Outline outline;


    private void OnMouseEnter()
    {
        Cursor.SetCursor(sprite, hotSpot, cursorMode);
        if (engaged != null) { engaged.value = true; Debug.Log("engaged"); }
        if (outline != null) { outline.enabled = true; }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        if (engaged != null) { engaged.value = false; Debug.Log("disengaged"); }
        if (outline != null) { outline.enabled = false; }
    }
}
