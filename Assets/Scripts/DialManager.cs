using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialManager : MonoBehaviour
{
    public Queue<Dialogue> dials;
    private string dSentence;
    private string nm;
    private Dialogue currentDial;
    private Color tmp;

    public TextMeshProUGUI nameField;
    public TextMeshProUGUI dialText;
    public GameObject dialBox;
    public GameObject icon;
    void Start()
    {
        dials = new Queue<Dialogue>();
        dialBox.GetComponent<Image>().enabled = false;
        tmp = icon.GetComponent<Image>().color;
        alphaIcon();

    }

    public void startDialogue(Dialogue[] dialogue)
    {
        dials.Clear();
        dialBox.GetComponent<Image>().enabled = true;
        alphaIcon();

        foreach (Dialogue sentence in dialogue)
        {
            dials.Enqueue(sentence);

        }
        displayNext();
    }

    public void displayNext()
    {
        if (dials.Count == 0)
        {
            endDialogue();

            return;
        }

        currentDial = dials.Dequeue();
        nm = currentDial.nameField;
        nameField.text = nm;
        dSentence = currentDial.sentences;

        icon.GetComponent<Image>().sprite = currentDial.icon;


        StopAllCoroutines();
        StartCoroutine(typeSentence(dSentence));
    }

    IEnumerator typeSentence(string sentence)
    {
        dialText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialText.text += letter;
            yield return null;
        }
    }

    private void endDialogue()
    {
        nameField.text = "";
        dialText.text = "";
        dialBox.GetComponent<Image>().enabled = false;
        alphaIcon();
    }

    private void alphaIcon()
    {
        if (tmp.a == 0f)
        {
            tmp.a = 255f;
            icon.GetComponent<Image>().color = tmp;
        }
        else
        {
            tmp.a = 0f;
            icon.GetComponent<Image>().color = tmp;
        }
    }
}
