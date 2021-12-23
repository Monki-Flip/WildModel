using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellsStackOutline : MonoBehaviour
{
    public Image Outline;

    void OnMouseDown()
    {
        Outline.enabled = !Outline.enabled;
    }
}
