using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto); }
}
