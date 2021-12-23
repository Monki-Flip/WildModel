using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellsStack : MonoBehaviour
{
    public int InitCount;

    public TMP_Text CellsInStackCountText;

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
        AddRandomCells(InitCount);
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
            newCell.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 1);
            newCell.GetComponent<RectTransform>().anchoredPosition = NextPosition;
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
