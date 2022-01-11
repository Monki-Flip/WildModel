using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public CellsStack CellsStack;
    public CellsManager CellsManager; 

    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 StartPos;
    private PolygonCollider2D Collider;
    private Collider2D IntersectedCollider;


    private void Start()
    {
        canvas = gameObject.GetComponentInParent<Canvas>();
        CellsStack = gameObject.GetComponentInParent<Canvas>().GetComponentInParent<CellsStack>();
        Collider = GetComponent<PolygonCollider2D>();
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartPos = transform.localPosition;
        StartCoroutine(ScaleToBoardCellSize());
        //Debug.Log("OnBeginDrag");
    }

    IEnumerator ScaleToBoardCellSize()
    {
        while (rectTransform.localScale.x > 0.5f && rectTransform.localScale.y > 0.5f)
        {
            rectTransform.localScale -= new Vector3(0.05f, 0.05f, 0f);
            yield return null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        var hittenColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f).Where(x => x.gameObject.name == "EmptyCell(Clone)").ToList();
        var minLen = float.MaxValue;
        if(IntersectedCollider != null)
            IntersectedCollider.GetComponent<SpriteRenderer>().color = Color.white;
        for (var i = 0; i < hittenColliders.Count; i++)
        {
            if(hittenColliders[i].Distance(gameObject.GetComponent<PolygonCollider2D>()).distance < minLen)
            {
                minLen = hittenColliders[i].Distance(gameObject.GetComponent<PolygonCollider2D>()).distance;
                IntersectedCollider = hittenColliders[i];
            }
        }
        if (IntersectedCollider != null)
            IntersectedCollider.GetComponent<SpriteRenderer>().color =  Color.black;
        
        if (hittenColliders.Count == 0)
        {
            if (IntersectedCollider != null)
                IntersectedCollider.GetComponent<SpriteRenderer>().color = Color.white;
            IntersectedCollider = null;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        if (IntersectedCollider != null)
        {
            Debug.Log(IntersectedCollider.gameObject);
            CellsManager.CreateCell(IntersectedCollider.gameObject);
        }
        else
        {
            StartCoroutine(GoToStartPosition());
            StartCoroutine(ScaleToCellInStackSize());
        }
    }

    IEnumerator GoToStartPosition()
    {
        float totalMovementTime = 0.25f; //the amount of time you want the movement to take
        float currentMovementTime = 0f; //The amount of time that has passed
        var origin = transform.localPosition;
        while (Vector3.Distance(transform.localPosition, StartPos) > 0)
        {
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(origin, StartPos, currentMovementTime / totalMovementTime);
            yield return null;
        }
        CellsStack.IsDragging = false;
    }

    IEnumerator ScaleToCellInStackSize()
    {
        while (rectTransform.localScale.x < 1f && rectTransform.localScale.y < 1f)
        {
            rectTransform.localScale += new Vector3(0.05f, 0.05f, 0f);
            yield return null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CellsStack.IsDragging = true;
        //Debug.Log("OnPointerDown");
    }

}