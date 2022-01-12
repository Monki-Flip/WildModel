using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainTasks : MonoBehaviour
{
    public TasksManager TasksManager;
    public CellsManager CellsManager;
    public LotkaVolterraModel CurrentLotka;
    public ModelTextManager ModelTextManager;
    public IterationsManager IterationsManager;

    private int CommonCellsCount;
    private int WaterCellsCount;
    private List<GameObject> WaterCells;
    private int GrassCellsCount;
    private LotkaVolterraModel Lotka;
    private List<GameObject> MountainCells;
    private List<GameObject> MushroomCells;

    private Dictionary<string, Action> TasksCheck;
    public Dictionary<string, Action> DataTasksUpdate;
    public List<string> TasksToCheck;
    private List<Card> LastPurchaseHistory;
    public int FirstDay;

    public GameObject WinPanel;
    public GameObject DefeatPanel;


    private void Start()
    {
        WaterCells = new List<GameObject>();
        TasksToCheck = new List<string>();
        LastPurchaseHistory = new List<Card>();
        MountainCells = new List<GameObject>();
        MushroomCells = new List<GameObject>();

        TasksCheck = new Dictionary<string, Action>();
        TasksCheck.Add("Task1", () => CheckTask1());
        TasksCheck.Add("Task2", () => CheckTask2());
        TasksCheck.Add("Task3", () => CheckTask3());
        TasksCheck.Add("Task4", () => CheckTask4());
        TasksCheck.Add("Task5", () => CheckTask5());
        TasksCheck.Add("Task6", () => CheckTask6());
        TasksCheck.Add("Task7", () => CheckTask7());
        TasksCheck.Add("Task8", () => CheckTask8());
        TasksCheck.Add("Task9", () => CheckTask9());
        TasksCheck.Add("Task10", () => CheckTask10());
        TasksCheck.Add("Task11", () => CheckTask11());
        TasksCheck.Add("Task12", () => CheckTask12());
        TasksCheck.Add("Task13", () => CheckTask13());

        DataTasksUpdate = new Dictionary<string, Action>();
        DataTasksUpdate.Add("Task1", () => UpdateDataForTask1());
        DataTasksUpdate.Add("Task2", () => UpdateDataForTask2());
        DataTasksUpdate.Add("Task3", () => UpdateDataForTask3());
        DataTasksUpdate.Add("Task4", () => UpdateDataForTask4());
        DataTasksUpdate.Add("Task5", () => UpdateDataForTask5());
        DataTasksUpdate.Add("Task6", () => UpdateDataForTask6());
        DataTasksUpdate.Add("Task7", () => UpdateDataForTask7());
        DataTasksUpdate.Add("Task8", () => UpdateDataForTask8());
        DataTasksUpdate.Add("Task9", () => UpdateDataForTask9());
        DataTasksUpdate.Add("Task10", () => UpdateDataForTask10());
        DataTasksUpdate.Add("Task11", () => UpdateDataForTask11());
        DataTasksUpdate.Add("Task12", () => UpdateDataForTask12());
        DataTasksUpdate.Add("Task13", () => UpdateDataForTask13());

    }

    private void Update()
    {
        for (var i = 0; i < TasksToCheck.Count(); i++)
            TasksCheck[TasksToCheck[i]]();
    }

    private void CheckTask1()
    {
        if (CellsManager.Cells.Where(x => x.GetComponent<Cell>().Name == "Common").Count() - CommonCellsCount > 4)
        {
            TasksManager.ChangeTaskState("Task1(Clone)");
            TasksToCheck.Remove("Task1");
        }
    }

    private void UpdateDataForTask1()
    {
        CommonCellsCount = CellsManager.Cells.Where(x => x.GetComponent<Cell>().Name == "Common").Count();
    }

    private void CheckTask2()
    {
        if (CellsManager.Cells.Where(x => x.GetComponent<Cell>().Name == "Water").Count() - WaterCellsCount > 0)
            TasksManager.TasksPrefabInPanel.Where(x => x.name == "Task2(Clone)").First().GetComponent<Task>().MarkFirstCondition();
        foreach(var cell in CellsManager.Cells)
        {
            if(cell.GetComponent<Cell>().Name == "Water" && !WaterCells.Contains(cell))
            {
                var foodCount = Physics2D.OverlapCircleAll(cell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<Cell>(out Cell c) && (x.gameObject.GetComponent<Cell>().Name == "Grass" || x.gameObject.GetComponent<Cell>().Name == "Mushroom")).Count();
                Debug.Log(foodCount);
                if (foodCount > 1)
                {
                    TasksManager.TasksPrefabInPanel.Where(x => x.name == "Task2(Clone)").First().GetComponent<Task>().MarkSecondCondition();
                    break;
                }
            }
        }

        if (TasksManager.TasksPrefabInPanel.Where(x => x.name == "Task2(Clone)").FirstOrDefault().GetComponent<Task>().IsDone)
        {
            TasksManager.ChangeTaskState("Task2(Clone)");
            TasksToCheck.Remove("Task2");
        }
    }

    private void UpdateDataForTask2()
    {
        WaterCellsCount = CellsManager.Cells.Where(x => x.GetComponent<Cell>().Name == "Water").Count();
        WaterCells.AddRange(CellsManager.Cells.Where(x => x.GetComponent<Cell>().Name == "Water").ToList());
    }


    private void CheckTask3()
    {
        if(CellsManager.LastAddedScore > 24)
        {
            TasksManager.ChangeTaskState("Task3(Clone)");
            TasksToCheck.Remove("Task3");
        }
    }

    private void UpdateDataForTask3()
    {
        CellsManager.LastAddedScore = 0;
    }

    private void CheckTask4()
    {
        if(Lotka.Alpha != CurrentLotka.Alpha || Lotka.Beta != CurrentLotka.Beta || Lotka.Gamma != CurrentLotka.Gamma || Lotka.Sigma != CurrentLotka.Sigma)
        {
            TasksManager.ChangeTaskState("Task4(Clone)");
            TasksToCheck.Remove("Task4");
        }
    }

    private void UpdateDataForTask4()
    {
        Lotka = new LotkaVolterraModel();
        Lotka.Alpha = CurrentLotka.Alpha;
        Lotka.Beta = CurrentLotka.Beta;
        Lotka.Gamma = CurrentLotka.Gamma;
        Lotka.Sigma = CurrentLotka.Sigma;
    }

    private void CheckTask5()
    {
        if (CellsManager.Cells.Where(x => x.GetComponent<Cell>().Name == "Grass").Count() - GrassCellsCount > 6)
        {
            TasksManager.ChangeTaskState("Task5(Clone)");
            TasksToCheck.Remove("Task5");
        }
    }

    private void UpdateDataForTask5()
    {
        GrassCellsCount = CellsManager.Cells.Where(x => x.GetComponent<Cell>().Name == "Grass").Count();
    }

    private void CheckTask6()
    {
        TasksToCheck.Remove("Task6");
        var task = (TasksManager.TasksPrefabInPanel.Where(x => x.name == "Task6(Clone)").First());
        TasksManager.TasksPrefabInPanel.Remove(task);
        Destroy(task);
        TasksManager.AddTaskToPanel();
    }

    private void UpdateDataForTask6()
    {

    }

    private void CheckTask7()
    {
        if (ModelTextManager.PurchaseHistory.Skip(LastPurchaseHistory.Count()).Where(x => x.gameObject.name == "Card (Голодные времена)").Count() > 1)
        {
            TasksManager.ChangeTaskState("Task7(Clone)");
            TasksToCheck.Remove("Task7");
        }
    }

    private void UpdateDataForTask7()
    {
        LastPurchaseHistory.AddRange(ModelTextManager.PurchaseHistory);
    }

    private void CheckTask8()
    {
        foreach (var cell in CellsManager.Cells)
        {
            if (cell.GetComponent<Cell>().Name == "Mountain" && !MountainCells.Contains(cell))
            {
                var mountainAroundCount = Physics2D.OverlapCircleAll(cell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<Cell>(out Cell c) && x.gameObject.GetComponent<Cell>().Name == "Mountain").Count();
                if (mountainAroundCount > 2)
                    TasksManager.TasksPrefabInPanel.Where(x => x.name == "Task8(Clone)").First().GetComponent<Task>().MarkFirstCondition();
            }
        }
        if (CellsManager.LastAddedScore > 124)
            TasksManager.TasksPrefabInPanel.Where(x => x.name == "Task8(Clone)").First().GetComponent<Task>().MarkSecondCondition();
        if (TasksManager.TasksPrefabInPanel.Where(x => x.name == "Task8(Clone)").FirstOrDefault().GetComponent<Task>().IsDone) //трабл object reference not set
        {
            TasksManager.ChangeTaskState("Task8(Clone)");
            TasksToCheck.Remove("Task8");
        }
    }

    private void UpdateDataForTask8()
    {
        foreach (var cell in CellsManager.Cells)
        {
            if (cell.GetComponent<Cell>().Name == "Mountain")
            {
                /*
                var emptyCellsAroundCount = Physics2D.OverlapCircleAll(cell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<EmptyCell>(out EmptyCell c)).Count();
                if (emptyCellsAroundCount == 0)
                    MountainCells.Add(cell);
                */
                MountainCells.Add(cell);
            }
        }
    }

    private void CheckTask9()
    {
        if (ModelTextManager.PurchaseHistory.Skip(LastPurchaseHistory.Count()).Where(x => x.gameObject.name == "Card (Быстрые лапы)").Count() > 1)
        {
            TasksManager.ChangeTaskState("Task9(Clone)");
            TasksToCheck.Remove("Task9");
        }
    }

    private void UpdateDataForTask9()
    {
        LastPurchaseHistory = new List<Card>();
        LastPurchaseHistory.AddRange(ModelTextManager.PurchaseHistory);
    }

    private void CheckTask10()
    {
        foreach (var cell in CellsManager.Cells)
        {
            if (cell.GetComponent<Cell>().Name == "Water" && !WaterCells.Contains(cell))
            {
                var waterAroundCount = Physics2D.OverlapCircleAll(cell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<Cell>(out Cell c) && x.gameObject.GetComponent<Cell>().Name == "Water").Count();
                if (waterAroundCount > 2)
                {
                    TasksManager.ChangeTaskState("Task10(Clone)");
                    TasksToCheck.Remove("Task10");
                    break;
                }
            }
        }
    }

    private void UpdateDataForTask10()
    {
        WaterCells = new List<GameObject>();
        foreach (var cell in CellsManager.Cells)
        {
            if (cell.GetComponent<Cell>().Name == "Water")
            {
                /*
                var emptyCellsAroundCount = Physics2D.OverlapCircleAll(cell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<EmptyCell>(out EmptyCell c)).Count();
                if (emptyCellsAroundCount == 0)
                    WaterCells.Add(cell);
                */
                WaterCells.Add(cell);
            }
        }
    }

    private void CheckTask11()
    {
        foreach (var cell in CellsManager.Cells)
        {
            if (cell.GetComponent<Cell>().Name == "Water" && !WaterCells.Contains(cell))
            {
                var visited = new List<GameObject>();
                var queue = new List<GameObject>();
                var waterAround = Physics2D.OverlapCircleAll(cell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<Cell>(out Cell c) && x.gameObject.GetComponent<Cell>().Name == "Water")
                                        .Select(x => x.gameObject)
                                        .ToList();
                queue.AddRange(waterAround);
                while(queue.Count > 0)
                {
                    var nextCell = queue[0];
                    queue.RemoveAt(0);
                    visited.Add(nextCell);
                    waterAround = Physics2D.OverlapCircleAll(nextCell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<Cell>(out Cell c) && x.gameObject.GetComponent<Cell>().Name == "Water" && !visited.Contains(x.gameObject))
                                        .Select(x => x.gameObject)
                                        .ToList();
                    queue.AddRange(waterAround);
                }
                if (visited.Count > 4)
                {
                    TasksManager.ChangeTaskState("Task11(Clone)");
                    TasksToCheck.Remove("Task11");
                    break;
                }
            }
        }
    }

    private void UpdateDataForTask11()
    {
        WaterCells = new List<GameObject>();
        foreach (var cell in CellsManager.Cells)
        {
            if (cell.GetComponent<Cell>().Name == "Water")
            {
                /*
                var emptyCellsAroundCount = Physics2D.OverlapCircleAll(cell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<EmptyCell>(out EmptyCell c)).Count();
                if (emptyCellsAroundCount == 0)
                    WaterCells.Add(cell);
                */
                WaterCells.Add(cell);
            }
        }
    }

    private void CheckTask12()
    {
        foreach (var cell in CellsManager.Cells)
        {
            if (cell.GetComponent<Cell>().Name == "Mushroom" && !MushroomCells.Contains(cell))
            {
                var visited = new List<GameObject>();
                var queue = new List<GameObject>();
                var mushroomAround = Physics2D.OverlapCircleAll(cell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<Cell>(out Cell c) && x.gameObject.GetComponent<Cell>().Name == "Mushroom")
                                        .Select(x => x.gameObject)
                                        .ToList();
                queue.AddRange(mushroomAround);
                while (queue.Count > 0)
                {
                    var nextCell = queue[0];
                    queue.RemoveAt(0);
                    visited.Add(nextCell);
                    mushroomAround = Physics2D.OverlapCircleAll(nextCell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<Cell>(out Cell c) && x.gameObject.GetComponent<Cell>().Name == "Mushroom" && !visited.Contains(x.gameObject))
                                        .Select(x => x.gameObject)
                                        .ToList();
                    queue.AddRange(mushroomAround);
                }
                if (visited.Count > 4)
                {
                    TasksManager.ChangeTaskState("Task12(Clone)");
                    TasksToCheck.Remove("Task12");
                    break;
                }
            }
        }
    }

    private void UpdateDataForTask12()
    {
        MushroomCells = new List<GameObject>();
        foreach (var cell in CellsManager.Cells)
        {
            if (cell.GetComponent<Cell>().Name == "Mushroom")
            {
                /*
                var emptyCellsAroundCount = Physics2D.OverlapCircleAll(cell.transform.position, 0.55f)
                                        .Where(x => x.TryGetComponent<EmptyCell>(out EmptyCell c)).Count();
                if (emptyCellsAroundCount == 0)
                    MushroomCells.Add(cell);
                */
                MushroomCells.Add(cell);
            }
        }
    }

    private void CheckTask13()
    {
        Debug.Log(IterationsManager.Day);
        Debug.Log(FirstDay);
        Debug.Log(Lotka.Preys);
        Debug.Log(Lotka.Predators);

        if (IterationsManager.Day - FirstDay == 7 && CurrentLotka.Preys > 0 && CurrentLotka.Predators > 0)
        {
            TasksManager.ChangeTaskState("Task12(Clone)");
            TasksToCheck.Remove("Task12");
            WinPanel.SetActive(true);
        }
        else if (IterationsManager.Day - FirstDay == 7)
        {
            TasksManager.ChangeTaskState("Task12(Clone)");
            TasksToCheck.Remove("Task12");
            DefeatPanel.SetActive(true);

        }
    }

    private void UpdateDataForTask13()
    {
        FirstDay = IterationsManager.Day;
    }
}
