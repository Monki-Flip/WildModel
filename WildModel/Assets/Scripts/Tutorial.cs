using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject MESSAGE;  //MESSAGE.SetActive(true);

    public GameObject TutorialPanel;
    public TasksManager TasksManager;
    public GameObject AngelPanel;
    public Canvas Canv;
    public Image Panel;
    public Cloud TextCloud;
    public GameObject TasksPanel;
    public CellsManager CellsManager;
    public GameObject ModelInfo;
    public GameObject Shop;
    public Button ShopCloseButton;
    public ModelTextManager ModelTextManager;
    public Card Card;
    public GameObject SkipButton;
    public LotkaVolterraModel Lotka;


    public Animation TutorialTasksHelper;
    public Animation TutorialModelHelper;
    public Animation TutorialShopButtonHelper;

    public int CurrentIteration;
    public int CurrentText;
    public int TextCount;

    public bool[] IsRun;

    public int TasksCount;

    public bool IsTutorial1Done;

    private void Start()
    {
        IsRun = new bool[50];
    }
    private void Update()
    {
        if (CurrentIteration == 0)
        {
            if (!Canv.enabled)
                CurrentIteration++;
            else if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.PlayAnim("Open1");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
        }
        else if (CurrentIteration == 1)
        {
            SkipButton.SetActive(false);
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                System.Threading.Thread.Sleep(1000);
                Canv.enabled = true;
                Panel.color = new Color(0, 0, 0, 0.5f);
                TextCloud.PlayAnim("Open2");
                TextCloud.ChangeButton();
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
            else if (!TextCloud.IsTyping)
                CurrentIteration++;
        }
        else if (CurrentIteration == 2)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.PlayAnim("Open2");
                TextCloud.ChangeButton();
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
                TasksManager.AddTaskToPanel();
            }
            else if (!TextCloud.IsTyping)
                CurrentIteration++;
        }
        else if (CurrentIteration == 3)
        {
            if (!IsRun[CurrentIteration] && TasksManager.CheckTutorial1())
            {
                IsRun[CurrentIteration] = true;
                Canv.enabled = true;
                TasksManager.UpdateTaskButton();
                TextCloud.ChangeButton();
                TextCloud.PlayAnim("Open1");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;

            }
        }
        else if (CurrentIteration == 4)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.ChangeButton();
                TextCloud.PlayAnim("Open2");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
            else if (!TextCloud.IsTyping)
                CurrentIteration++;
        }
        else if (CurrentIteration == 5)
        {
            IsRun[CurrentIteration] = true;
            TutorialTasksHelper.gameObject.SetActive(true);
            TutorialTasksHelper.Play();
        }
        else if (CurrentIteration == 6)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TutorialTasksHelper.gameObject.SetActive(false);
                Canv.enabled = true;
                TasksPanel.SetActive(false);
                TasksManager.ChangeOpenImgState();
                TextCloud.PlayAnim("Open2");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
            else if (!TextCloud.IsTyping)
            {
                CellsManager.PutAnimalOnCell(CellsManager.GetRandomCellForAnimal(), "Deer");
                CellsManager.PutAnimalOnCell(CellsManager.GetRandomCellForAnimal(), "Wolf");
                Lotka.Preys = 5;
                Lotka.Predators = 2;
                Lotka.UpdateAnimalsCounters();
                CurrentIteration++;
            }
        }
        else if (CurrentIteration == 7)
        {
            if (!IsRun[CurrentIteration])
            {
                System.Threading.Thread.Sleep(1000);
                IsRun[CurrentIteration] = true;
                Canv.enabled = true;
                TasksPanel.SetActive(false);
                TasksManager.ChangeOpenImgState();
                TextCloud.PlayAnim("Open3");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
            else if (!TextCloud.IsTyping)
                CurrentIteration++;
        }
        else if (CurrentIteration == 8)
        {
            IsRun[CurrentIteration] = true;
            TutorialModelHelper.gameObject.SetActive(true);
            TutorialModelHelper.Play();
            if (ModelInfo.activeSelf)
            {
                CurrentIteration++;
            }
        }
        else if (CurrentIteration == 9)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TutorialModelHelper.gameObject.SetActive(false);
                Canv.enabled = true;
                Panel.enabled = false;
                TextCloud.Angel.transform.localPosition += new Vector3(40f, 0f, 0f);
                TextCloud.gameObject.transform.localPosition += new Vector3(40f, 0f, 0f);
                TextCloud.ChangeButton();
                TextCloud.PlayAnim("Open1");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
        }
        else if (CurrentIteration == 10)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.PlayAnim("Open5");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
        }
        else if (CurrentIteration == 11)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.PlayAnim("Open1");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
        }
        else if (CurrentIteration == 12)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.PlayAnim("Open3");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
        }
        else if (CurrentIteration == 13)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.ChangeButton();
                TextCloud.PlayAnim("Open5");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
            else if (!TextCloud.IsTyping)
            {
                TextCloud.Angel.transform.localPosition -= new Vector3(40f, 0f, 0f);
                TextCloud.gameObject.transform.localPosition -= new Vector3(40f, 0f, 0f);
                CurrentIteration++;
            }
        }
        else if (CurrentIteration == 14)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                ModelInfo.SetActive(false);
                Canv.enabled = true;
                Panel.enabled = true;
                TextCloud.PlayAnim("Open1");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
            else if (!TextCloud.IsTyping)
            {
                CurrentIteration++;
            }
        }
        else if (CurrentIteration == 15)
        {
            IsRun[CurrentIteration] = true;
            TutorialShopButtonHelper.gameObject.SetActive(true);
            TutorialShopButtonHelper.Play();
            if (Shop.activeSelf)
            {
                CurrentIteration++;
            }
        }
        else if (CurrentIteration == 16)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TutorialShopButtonHelper.gameObject.SetActive(false);
                Canv.enabled = true;
                TextCloud.ChangeButton();
                TextCloud.PlayAnim("Open3");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
        }
        else if (CurrentIteration == 17)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.ChangeButton();
                TextCloud.PlayAnim("Open2");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
            else if (!TextCloud.IsTyping)
            {
                CurrentIteration++;
            }
        }
        else if (CurrentIteration == 18)
        {
            if (!IsRun[CurrentIteration] && Card.Outline.enabled)
            {
                IsRun[CurrentIteration] = true;
                Invoke("Wait18", 1f);
            }
            else if (ModelTextManager.LastBoughtCard == Card)
            {
                CurrentIteration++;
            }
        }
        else if (CurrentIteration == 19)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                ShopCloseButton.onClick.Invoke();
                Canv.enabled = true;
                TextCloud.ChangeButton();
                TextCloud.PlayAnim("Open3");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;

            }
        }
        else if (CurrentIteration == 20)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.PlayAnim("Open5");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;

            }
        }
        else if (CurrentIteration == 21)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.PlayAnim("Open3");
                TextCloud.DisplayText(CurrentText);
                CurrentText++;
            }
        }
        else if (CurrentIteration == 22)
        {
            if (!IsRun[CurrentIteration])
            {
                IsRun[CurrentIteration] = true;
                TextCloud.ChangeButton();
                TextCloud.PlayAnim("Open1");
                TextCloud.DisplayText(CurrentText);
                TasksManager.AddTaskToPanel();
                TasksManager.ChangeTaskState("Tutorial2(Clone)");
                TasksManager.UpdateTaskButton();
            }
            else if (TasksManager.TasksPrefabInPanel.Count == 0)
            {
                StartMainTasks();
            }
        }
    }

    public void NextText() //NextIteration, но если поменять, все сломается
    {
        CurrentIteration++;
    }

    public void Tutorial1Done()
    {
        IsTutorial1Done = true;
    }

    public void Wait18()
    {
        Canv.enabled = true;
        TextCloud.PlayAnim("Open2");
        TextCloud.DisplayText(CurrentText);
        CurrentText++;
    }

    public void SkipTutoral()
    {
        AngelPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        CurrentIteration = 100000;
        CellsManager.CreateStartField();
        CellsManager.PutAnimalOnCell(CellsManager.GetRandomCellForAnimal(), "Deer");
        CellsManager.PutAnimalOnCell(CellsManager.GetRandomCellForAnimal(), "Wolf");
        TasksManager.Score.Add(95);
        CellsManager.CellsStack.AddRandomCells(10);
        Lotka.Preys = 20;
        Lotka.Predators = 5;
        Lotka.UpdateAnimalsCounters();
        TasksManager.TasksCount = 12;
        TasksManager.NextTaskToAdd = 2;
        TasksManager.TasksButtonMainText.text = "Баланс мира";
        TasksManager.UpdateTaskButton();
        TasksManager.AddTaskToPanel();
        TasksManager.AddTaskToPanel();
        TasksManager.AddTaskToPanel();
    }

    public void StartMainTasks()
    {
        AngelPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        CurrentIteration = 100000;
        TasksManager.TasksCount = 12;
        TasksManager.TasksDone = 0;
        TasksManager.NextTaskToAdd = 2;
        TasksManager.TasksButtonMainText.text = "Баланс мира";
        TasksManager.UpdateTaskButton();
        TasksManager.AddTaskToPanel();
        TasksManager.AddTaskToPanel();
        TasksManager.AddTaskToPanel();
    }
}
