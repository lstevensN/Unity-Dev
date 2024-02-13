using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    [SerializeField] BoolVariable engaged;

    [Header("Sprite")]
    [SerializeField] Texture2D spritePassive;


    private void OnMouseEnter()
    {
        Cursor.SetCursor(spritePassive, hotSpot, cursorMode);
    }

    private void OnMouseExit()
    {
        if (!engaged.value) Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
