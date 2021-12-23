using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityProj
{
    public class Animal : MonoBehaviour
    {
        public string AnimalType;
    
        public void SwitchParent()
        {
            var hittenColliders = Physics2D.OverlapCircleAll(gameObject.transform.position - new Vector3(0, 0.5f, 0), 0.01f);

            Debug.Log(hittenColliders.Length);
            Debug.Log(hittenColliders[0].gameObject.name);
            gameObject.transform.parent = hittenColliders[0].gameObject.transform;
            gameObject.transform.localPosition = new Vector2(0f, 0.2f);
        }

    }
}