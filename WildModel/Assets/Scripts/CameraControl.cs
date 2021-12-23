using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class CameraControl : MonoBehaviour
{
    [Range(0, 20f)]
    public float moveSpeed = 15f;
    public float Bounds;
    public Camera cam;
    public GameObject TasksPanel;

    private Vector2 startPos;

    private Vector2 targetPos;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = cam.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("Нажал");
        }
        if (Input.GetMouseButton(0) && !(TasksPanel.activeSelf))
        {
            //Debug.Log("Зажал");
            float posX = cam.ScreenToWorldPoint(Input.mousePosition).x - startPos.x;
            float posY = cam.ScreenToWorldPoint(Input.mousePosition).y - startPos.y;
            targetPos = new Vector2(Mathf.Clamp(transform.position.x - posX, -Bounds, Bounds),
                                    Mathf.Clamp(transform.position.y - posY, -Bounds, Bounds));
        }
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.x, moveSpeed * Time.deltaTime),
                                            Mathf.Lerp(transform.position.y, targetPos.y, moveSpeed * Time.deltaTime),
                                            transform.position.z);
    }
}
