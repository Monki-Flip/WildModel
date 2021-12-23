using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TasksManager : MonoBehaviour
{
    public CellsStack CellsStack;
    public Score Score;

    public Tutorial Tutorial;
    public GameObject Panel;
    public GameObject Content;
    public List<GameObject> TasksPrefab;
    public List<GameObject> TasksPrefabInPanel;
    public int NextTaskToAdd;

    public TMP_Text CellsInStackCount;

    public int TasksCount;
    public int TasksDone;
    public Image Bar;
    public TMP_Text TasksButtonPregressText;
    public TMP_Text TasksButtonMainText;

    public MainTasks MainTasks;


    private void Start()
    {
        TasksCount = Tutorial.TasksCount;
    }

    public void AddTaskToPanel()
    {
        if (NextTaskToAdd != 14 || TasksPrefabInPanel.Count == 0)
        {
            var prefab = Instantiate(TasksPrefab[NextTaskToAdd], Content.transform);
            NextTaskToAdd++;
            prefab.transform.localPosition = Vector2.zero;
            prefab.GetComponent<RectTransform>().transform.localScale = Vector3.one;

            TasksPrefabInPanel.Add(prefab);

            prefab.GetComponent<Task>().CellsStack = CellsStack;
            prefab.GetComponent<Task>().Score = Score;
            prefab.GetComponent<Task>().Tutorial = Tutorial;
            prefab.GetComponent<Task>().TasksManager = this;

            if (prefab.name.Substring(0, 4) == "Task")
            {
                Debug.Log(prefab.name.Remove(prefab.name.Length - 7));
                MainTasks.DataTasksUpdate[prefab.name.Remove(prefab.name.Length - 7)]();
                MainTasks.TasksToCheck.Add(prefab.name.Remove(prefab.name.Length - 7));
            }
        }
    }

    public bool CheckTutorial1()
    {
        if (CellsInStackCount.text == "0")
        {
            ChangeTaskState("Tutorial1(Clone)");
            return true;
        }
        return false;
    }

    public void ChangeTaskState(string taskName)
    {
        Panel.SetActive(true);
        var tasks = Content.GetComponentsInChildren<Task>();
        foreach (var task in tasks)
        {
            if (task.gameObject.name == taskName)
            {
                task.MarkFirstCondition();
                task.MarkSecondCondition();
                task.MakeButtonActive();
                break;
            }
        }
        Panel.SetActive(false);
    }

    public void UpdateTaskButton()
    {
        var str = new StringBuilder();
        str.Append(TasksDone.ToString() + "/" + TasksCount.ToString());
        Bar.fillAmount = (float)TasksDone / (float) TasksCount;
        TasksButtonPregressText.text = str.ToString();
    }
}
