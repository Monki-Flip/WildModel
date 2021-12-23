using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBlocker : MonoBehaviour
{
    public List<GameObject> Objects;

    public void BlockObjects(int layer)
    {
        for(var i = 0; i < Objects.Count; i++)
        {
            if (Objects[i].layer < layer)
                Objects[i].SetActive(false);
        }
    }

    public void UnblockOblects()
    {
        for (var i = 0; i < Objects.Count; i++)
                Objects[i].SetActive(true);
    }
}
