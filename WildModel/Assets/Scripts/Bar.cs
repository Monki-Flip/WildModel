using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image TaskBar;
    public int TaskCount;
    public int TaskDone;

    private void Start()
    {
        TaskBar.fillAmount = 0f;
    }

    public void UpdateBar()
    {
        TaskBar.fillAmount = TaskDone / TaskCount;
    }
}
