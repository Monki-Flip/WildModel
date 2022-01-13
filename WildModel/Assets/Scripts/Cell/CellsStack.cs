using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellsStack : MonoBehaviour
{
    public int InitCount;
    public bool IsDragging;

    public TMP_Text CellsInStackCountText;
    public CellsManager CellsManager;

    public List<GameObject> Stack;
    public GameObject CommonCell;
    public GameObject ForestCell;
    public GameObject GrassCell;
    public GameObject MountainCell;
    public GameObject MushroomCell;
    public GameObject WaterCell;
    public List<GameObject> AllCells;

    public Vector3 InitPos;

    private Vector3 Step = new Vector3(0, -20f, 0);
    private Vector3 NextPosition;



    void Start()
    {
        NextPosition = InitPos;
        Stack = new List<GameObject>();
        AllCells = new List<GameObject>() { CommonCell, ForestCell, WaterCell, GrassCell, MushroomCell, MountainCell };
        AddFirstRandomCells();
    }

    public void AddRandomCells(int cellCount)
    {
        var rnd = new System.Random();
        for (var i = 0; i < cellCount; i++)
        {
            var randI = rnd.Next(0, 101);
            var ind = randI <= 20 ? 0 : randI <= 40 ? 1 : randI <= 60 ? 2 : randI <= 75 ? 3 : randI <= 90 ? 4 : 5; 
            var newCell = Instantiate(AllCells[ind]);
            newCell.transform.parent = gameObject.GetComponentInChildren<Canvas>().gameObject.transform;
            newCell.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
            newCell.GetComponent<RectTransform>().anchoredPosition = NextPosition;
            newCell.GetComponent<DragAndDrop>().CellsManager = CellsManager;

            NextPosition += Step;

            newCell.GetComponent<RectTransform>().SetAsFirstSibling();

            Stack.Add(newCell);
        }

        CellsInStackCountText.text = Stack.Count.ToString();
    }

    public void AddFirstRandomCells()
    {
        var cellsInd = new List<int>() { 0, 0, 1, 2, 5};
        foreach(var ind in cellsInd)
        {
            var newCell = Instantiate(AllCells[ind]);
            newCell.transform.parent = gameObject.GetComponentInChildren<Canvas>().gameObject.transform;
            newCell.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
            newCell.GetComponent<RectTransform>().anchoredPosition = NextPosition;
            newCell.GetComponent<DragAndDrop>().CellsManager = CellsManager;

            NextPosition += Step;

            newCell.GetComponent<RectTransform>().SetAsFirstSibling();

            Stack.Add(newCell);
        }

        CellsInStackCountText.text = Stack.Count.ToString();
    }

    public void RemoveTop()
    {
        Destroy(Stack[0]);
        Stack.RemoveAt(0);
        NextPosition -= Step;
        for (var i = 0; i < Stack.Count; i++)
        {
            Stack[i].transform.localPosition -= Step;
        }
        CellsInStackCountText.text = Stack.Count.ToString();
    }
}
