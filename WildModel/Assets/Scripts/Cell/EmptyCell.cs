using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCell : MonoBehaviour
{
    public GameObject Prefab;
    public CellsStackOutline CellsStackOutline;
    public CellsStack CellsStack;
    public CellsManager CellsManager;

    public List<GameObject> Neighbors;


    void OnMouseDown()
    {
        if(CellsStack.Stack.Count > 0)
            CellsManager.CreateCell(this.gameObject);
    }

    private GameObject CreateEmptyCell()
    {
        var newEmptyCell = Instantiate(Prefab);
        newEmptyCell.GetComponent<EmptyCell>().Prefab = Prefab;
        newEmptyCell.GetComponent<EmptyCell>().CellsStackOutline = CellsStackOutline;
        newEmptyCell.GetComponent<EmptyCell>().CellsStack = CellsStack;
        newEmptyCell.GetComponent<EmptyCell>().CellsManager = CellsManager;
        newEmptyCell.GetComponent<EmptyCell>().Neighbors.Add(gameObject);
        Neighbors.Add(newEmptyCell);
        
        return newEmptyCell;
    }
    public void CreateEmptyCellTop()
    {
        var newEmptyCell = CreateEmptyCell();
        newEmptyCell.transform.parent = transform.parent;
        newEmptyCell.transform.localPosition = gameObject.transform.position + Vector3.up * 0.85f + Vector3.right * 0.15f;
    }

    public void ÑreateEmptyCellBottom()
    {
        var newEmptyCell = CreateEmptyCell();
        newEmptyCell.transform.parent = transform.parent;
        newEmptyCell.transform.localPosition = gameObject.transform.position + Vector3.down * 0.85f + Vector3.left * 0.15f;
    }

    public void CreateEmptyCellTopRight()
    {
        var newEmptyCell = CreateEmptyCell();
        newEmptyCell.transform.parent = transform.parent;
        newEmptyCell.transform.localPosition = gameObject.transform.position + Vector3.up * 0.425f + Vector3.right * 0.975f;
    }

    public void CreateEmptyCellTopLeft()
    {
        var newEmptyCell = CreateEmptyCell();
        newEmptyCell.transform.parent = transform.parent;
        newEmptyCell.transform.localPosition = gameObject.transform.position + Vector3.up * 0.425f + Vector3.left * 0.825f;
    }

    public void CreateEmptyCellBottomLeft()
    {
        var newEmptyCell = CreateEmptyCell();
        newEmptyCell.transform.parent = transform.parent;
        newEmptyCell.transform.localPosition = gameObject.transform.position + Vector3.down * 0.425f + Vector3.left * 0.975f;
    }

    public void CreateEmptyCellBottomRight()
    {
        var newEmptyCell = CreateEmptyCell();
        newEmptyCell.transform.parent = transform.parent;
        newEmptyCell.transform.localPosition = gameObject.transform.position + Vector3.down * 0.425f + Vector3.right * 0.825f;
    }
}
