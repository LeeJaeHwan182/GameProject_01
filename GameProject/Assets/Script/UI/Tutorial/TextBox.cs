using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;
    public Sprite cg;
}

public class TextBox : MonoBehaviour
{
    public static bool GAMESTART = false;

    [SerializeField] private Image image_StandingCG;
    [SerializeField] private Image image_DialogueBox;
    [SerializeField] private Text txt_Dialogue;

    private bool isDialogue = false;

    float timer;
    int waitingTime;

    private int count = 0;

    [SerializeField] private Dialogue[] dialogue;

    private void Start()
    {
        ShowDialogue();
        timer = 0;
        waitingTime = 5;
    }

    public void ShowDialogue()
    {
        OnOff(true);
        count = 0;
        NextDialogue();
    }

    private void OnOff(bool _flag)
    {
        image_StandingCG.gameObject.SetActive(_flag);
        image_DialogueBox.gameObject.SetActive(_flag);
        txt_Dialogue.gameObject.SetActive(_flag);

        isDialogue = _flag;
    }

    private void NextDialogue()
    {
        txt_Dialogue.text = dialogue[count].dialogue;
        image_StandingCG.sprite = dialogue[count].cg;
        count++;
    }

    private void Update()
    {
        if (isDialogue)
        {
            timer += Time.deltaTime;

            if(timer > waitingTime)
            {
                if (count < dialogue.Length)
                {
                    NextDialogue();
                }
                else
                {
                    GAMESTART = true;
                    OnOff(false);
                }
                timer = 0;
            }
        }
    }
}
