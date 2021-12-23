using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    public int Value;
    public TMP_Text Text;

    private void Start()
    {
        Value = 0;
        Text.text = Value.ToString();
    }

    public void Add(int v)
    {
        Value += v;
        Text.text = Value.ToString();
    }
}