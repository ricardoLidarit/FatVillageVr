using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private void Awake() => CursorFree();

    private void Update() => CursorFree();

    private void CursorFree()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
