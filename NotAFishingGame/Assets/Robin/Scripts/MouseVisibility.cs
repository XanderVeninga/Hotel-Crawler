using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVisibility : MonoBehaviour
{
    public bool mouseInvisible;

    void Start()
    {
        mouseInvisible = true;
    }

    void Update()
    {
        if(mouseInvisible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if(!mouseInvisible)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true; ;
        }
    }
}
