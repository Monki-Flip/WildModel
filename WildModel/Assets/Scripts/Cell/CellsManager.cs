using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellsManager : MonoBehaviour
{
    public CellsStack CellsStack;
    public Score Score;
    public Animals AnimalsManager;

    public EmptyCell FirstEmptyCell;

    public GameObject EmptyCell;
    public GameObject CommonCell;
    public GameObject ForestCell;
    public GameObject GrassCell;
    public GameObject MountainCell;
    public GameObject MushroomCell;
    public GameObject WaterCell;

    public GameObject Deer;
    public GameObject Wolf;

    public List<GameObject> Cells;
    public List<GameObject> Animals;

    public int LastAddedScore;
    public bool IsAnyCellOnBoard;

    void Start()
    {
        FirstEmptyCell.CreateEmptyCellTop();
        FirstEmptyCell.CreateEmptyCellTopLeft();
        FirstEmptyCell.CreateEmptyCellBottomLeft();
    }

    public GameObject GetRandomCellForAnimal()
    {
        var cells = Cells.Where(x => x.GetComponent<Cell>().Name != "Water" && x.GetComponent<Cell>().Name != "Mountain" && x.GetComponent<Cell>().Animal == null).ToList();
        var rnd = new System.Random();
        var randI = rnd.Next(0, cells.Count);
        //Debug.Log(randI + "   " + cells.Count);
        return cells[randI];
    }

    public void PutAnimalOnCell(GameObject cell, string animalName)
    {
        var animal = animalName == "Deer" ? Instantiate(Deer) : Instantiate(Wolf);
        animal.transform.parent = cell.transform;
        animal.transform.localScale = new Vector3(1f, 1f, 1f);
        animal.transform.localPosition = new Vector3(0f, 0.2f, 0f);
        cell.GetComponent<Cell>().Animal = animal;
        cell.GetComponent<Cell>().IsAnimalOn = true;
        AnimalsManager.List.Add(animal);
    }

    public GameObject GetCell(string name)
    {
        switch(name)
        {
            case "Common":
                return CommonCell;
            case "Forest":
                return ForestCell;
            case "Grass":
                return GrassCell;
            case "Mountain":
                return MountainCell;
            case "Water":
                return WaterCell;
            case "Mushroom":
                return MushroomCell;
        }
        return EmptyCell;
    }

    public void CreateCell(GameObject emptyCell)
    {
        if (CanBePut(emptyCell))
        {
            LastAddedScore = Score.Value;
            var cellInStick = CellsStack.Stack[0];
            CellsStack.RemoveTop();
            //Debug.Log(cellInStick.GetComponent<Cell>().Name);
            var cell = Instantiate(GetCell(cellInStick.GetComponent<Cell>().Name));
            //Debug.Log(cell.name + ": ");    
            CheckAround(cell.GetComponent<Cell>(), emptyCell);
            cell.transform.parent = emptyCell.transform.parent;
            cell.transform.position = emptyCell.transform.position;
            Destroy(emptyCell);
            Cells.Add(cell);
            LastAddedScore = Score.Value - LastAddedScore;

            var colliders = new Collider2D[10];
            cell.GetComponent<PolygonCollider2D>().GetContacts(new ContactFilter2D(), colliders);
            foreach(var coll in colliders)
            {
                if (coll != null && coll.gameObject.tag == "EmptyCell")
                    Destroy(coll.gameObject);
            }
        }
    }

    public bool CanBePut(GameObject emptyCell)
    {
        if (!IsAnyCellOnBoard)
        {
            IsAnyCellOnBoard = true;
            return true;
        }
        return true;
    }

    public void CheckAround(Cell cell, GameObject emptyCell)
    {
        var hittenColliders = Physics2D.OverlapCircleAll(emptyCell.transform.position, 1f);
        //foreach(var coll in hittenColliders)
        //    Debug.Log(coll.gameObject.name + coll.gameObject.transform.position);

        for (var i = 0; i < hittenColliders.Length; i++)
        {
            if (hittenColliders[i].gameObject.tag == "Cell" && hittenColliders[i].gameObject != cell.gameObject)
            {
                cell.Neighbors.Add(hittenColliders[i].gameObject);
                hittenColliders[i].gameObject.GetComponent<Cell>().Neighbors.Add(cell.gameObject);
                MarkPosition(hittenColliders[i].gameObject, cell, emptyCell);
                AddScore(cell, hittenColliders[i].GetComponent<Cell>());
            }
            else if (hittenColliders[i].gameObject.tag == "EmptyCell")
                MarkPosition(hittenColliders[i].gameObject, cell, emptyCell);
        }
        CreateGridAround(cell, emptyCell.GetComponent<EmptyCell>());
    }

    public void CreateGridAround(Cell cell, EmptyCell emptyCell)
    {
        if (!cell.HasBottomAnyCell)
            emptyCell.ÑreateEmptyCellBottom();
        if (!cell.HasBottomLeftAnyCell)
            emptyCell.CreateEmptyCellBottomLeft();
        if (!cell.HasBottomRightAnyCell)
            emptyCell.CreateEmptyCellBottomRight();
        if (!cell.HasTopAnyCell)
            emptyCell.CreateEmptyCellTop();
        if (!cell.HasTopLeftAnyCell)
            emptyCell.CreateEmptyCellTopLeft();
        if (!cell.HasTopRightAnyCell)
            emptyCell.CreateEmptyCellTopRight();
    }

    public void MarkPosition(GameObject obj, Cell cell, GameObject emptyCell)
    {
        //Debug.Log(obj.name + obj.transform.position);
        //cell.Neighbors.Add(obj);
        //obj.GetComponent<Cell>().Neighbors.Add(cell.gameObject);

        Vector2 pos = obj.transform.position;
        if (pos == (Vector2)emptyCell.transform.position + Vector2.up * 0.85f + Vector2.right * 0.15f)
            cell.HasTopAnyCell = true;
        else if (pos == (Vector2)emptyCell.transform.position + Vector2.down * 0.85f + Vector2.left * 0.15f)
            cell.HasBottomAnyCell = true;
        else if (pos == (Vector2)emptyCell.transform.position + Vector2.up * 0.425f + Vector2.right * 0.975f)
            cell.HasTopRightAnyCell = true;
        else if (pos == (Vector2)emptyCell.transform.position + Vector2.up * 0.425f + Vector2.left * 0.825f)
            cell.HasTopLeftAnyCell = true;
        else if (pos == (Vector2)emptyCell.transform.position + Vector2.down * 0.425f + Vector2.left * 0.975f)
            cell.HasBottomLeftAnyCell = true;
        else if (pos == (Vector2)emptyCell.transform.position + Vector2.down * 0.425f + Vector2.right * 0.825f)
            cell.HasBottomRightAnyCell = true;
        else if (obj.tag == "Cell")
        {
            cell.Neighbors.Remove(obj);
            obj.GetComponent<Cell>().Neighbors.Remove(cell.gameObject);
        }
    }

    public void AddScore(Cell cell, Cell nearCell)
    {
        var name = nearCell.Name;
        //Debug.Log("Çàøåë â ôóíêö" + name + nearCell);
        switch (cell.Name)
        {
            case "Common":
                if (name == "Common") Score.Add(5);
                else if (name == "Forest") Score.Add(7);
                else if (name == "Mountain") Score.Add(23);
                else if (name == "Water") Score.Add(17);
                else if (name == "Grass") Score.Add(13);
                else if (name == "Mushroom") Score.Add(15);
                break;
            case "Forest":
                if (name == "Common") Score.Add(7);
                else if (name == "Forest") Score.Add(10);
                else if (name == "Mountain") Score.Add(35);
                else if (name == "Water") Score.Add(20);
                else if (name == "Grass") Score.Add(15);
                else if (name == "Mushroom") Score.Add(17);
                break;
            case "Grass":
                if (name == "Common") Score.Add(13);
                else if (name == "Forest") Score.Add(15);
                else if (name == "Mountain") Score.Add(45);
                else if (name == "Water") Score.Add(30);
                else if (name == "Grass") Score.Add(20);
                else if (name == "Mushroom") Score.Add(20);
                break;
            case "Mountain":
                if (name == "Common") Score.Add(23);
                else if (name == "Forest") Score.Add(25);
                else if (name == "Mountain") Score.Add(50);
                else if (name == "Water") Score.Add(40);
                else if (name == "Grass") Score.Add(45);
                else if (name == "Mushroom") Score.Add(50);
                break;
            case "Water":
                if (name == "Common") Score.Add(17);
                else if (name == "Forest") Score.Add(20);
                else if (name == "Mountain") Score.Add(40);
                else if (name == "Water") Score.Add(35);
                else if (name == "Grass") Score.Add(30);
                else if (name == "Mushroom") Score.Add(35);
                break;
            case "Mushroom":
                if (name == "Common") Score.Add(15);
                else if (name == "Forest") Score.Add(17);
                else if (name == "Mountain") Score.Add(50);
                else if (name == "Water") Score.Add(35);
                else if (name == "Grass") Score.Add(20);
                else if (name == "Mushroom") Score.Add(25);
                break;
        }
    }

    public void CreateStartField()
    {
        FirstEmptyCell.GetComponent<EmptyCell>().ÑreateEmptyCellBottom();
        CreateCell(FirstEmptyCell.Neighbors[3]);

        CreateCell(FirstEmptyCell.Neighbors[0]);
        CreateCell(FirstEmptyCell.Neighbors[1]);
        CreateCell(FirstEmptyCell.Neighbors[2]);

        CreateCell(FirstEmptyCell.gameObject);
    }
}
