using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    public Dialogue Dialogues;
    public Animation Anim;
    public GameObject CloseButton;
    public GameObject NextTextButton;
    public GameObject Angel;
    public GameObject Podlojka;

    public TMP_Text CloudText;

    public int CurrentText;
    public bool IsTyping;
    public bool IsCloseButton;

    // Start is called before the first frame update
    void Start()
    {
        //BodyInitPos = new Vector3(-425f, 150f, 0);
        //PieceInitPos = new Vector3(-177f, 126f, 0);
        CloseButton.SetActive(false);
        NextTextButton.SetActive(false);
        Podlojka.SetActive(false);
        IsCloseButton = true;

    }
    
    public void PlayAnim(string name)
    {
        CloudText.text = "";
        Anim.Play(name);
    }

    public void MakeCloseButtonBlack()
    {
        CloseButton.GetComponent<TMP_Text>().color = new Color(0, 0, 0, 1f);
    }

    public void ChangeButton()
    {
        IsCloseButton = !IsCloseButton;
    }
    
    public void EndTyping()
    {
        IsTyping = false;
    }

    public void DisplayText(int dialogueNumber)
    {
        //if (Sentences.Count == 1)
        //    NextButton.enabled = false;
        IsTyping = true;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(Dialogues.Sentences[dialogueNumber]));

    }

    IEnumerator TypeSentence(string sentence)
    {
        //Debug.Log("пишу");
        yield return new WaitForSeconds(1);
        foreach (var letter in sentence)
        {
            CloudText.text += letter;
            yield return null;
        }
        ActivateButton();
    }

    public void ActivateButton()
    {
        Podlojka.SetActive(true);
        if (IsCloseButton)
            CloseButton.SetActive(true);
        else
            NextTextButton.SetActive(true);
    }
}
