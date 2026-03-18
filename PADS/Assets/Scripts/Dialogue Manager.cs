using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public enum Expressions
    {
        Sad,
        Neutral,
        Loving,
        Horny,
        Happy,
        Embarrased,
        Bored,
        Astonished,
        Angry
    }

    public enum Conditionals
    {
        More_than,
        More_or_Equal_to,
        Equal_to,
        Not_Equal_to,
        Less_or_Equal_to,
        Less_than,
        Contains
    }

    //public string[,] valueCheck;
    //public DialogueManager.Conditionals[,] conditional;
    //public float[,] requirement;
    //public string[,] dialogueOption;
    //public string[,] variableChange;
    //public float[,] valueChange;
    //public string[,] exit;

    public DialogueAsset asset;

    public string textBase;
    public TMP_Text textComponent;
    public TMP_Text nameText;
    public GameObject characterPortait;
    public int dialogueIndex = 1;
    public float readingCharIndex = 0;
    public GameObject[] dialogueOptionButtons;
    [SerializeField] float textSpeed = 30f;
    public Button dialogueButton;
    public Image nextTriangle;

    float screenWidth = 1920;
    float screenHeight = 1080;

    int choiceCount = 4;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        dialogueButton = GetComponent<Button>();

        DialogueProgress("Intro");
    }

    // Update is called once per frame
    void Update()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        //dialogueButton.enabled = textComponent.text.Length < textBase.Length;

        if (textComponent.text.Length < textBase.Length)
        {
            if (readingCharIndex < textBase.Length)
            {
                readingCharIndex += Time.deltaTime * textSpeed;
                //Debug.Log(textBase.Length);
                if (readingCharIndex >= textBase.Length - 1)
                {
                    readingCharIndex = textBase.Length;
                    //Debug.Log("Makin choices");
                    SetupDialogueChoices();
                }
            }

            while (textComponent.text.Length < readingCharIndex)
            {
                textComponent.text += textBase[textComponent.text.Length];
            }
        }
    }

    public void SetupDialogueChoices()
    {
        //Debug.Log(asset.dialogueOption[dialogueIndex]); 
        if (asset.dialogueOption[dialogueIndex] == "")
        {
            nextTriangle.enabled = true;
        }
        else
        {
            dialogueButton.enabled = false;
            choiceCount = 0;
            for (int i = 0; i < 4; i++)
            {
                //Debug.Log(dialogueIndex + i * asset.rowCount);
                //Debug.Log(asset.dialogueOption[dialogueIndex + i * asset.rowCount]);
                if (asset.dialogueOption[dialogueIndex + i * asset.rowCount] != "")
                {
                    choiceCount++;
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < choiceCount; i++)
            {
                dialogueOptionButtons[i].SetActive(true);
                dialogueOptionButtons[i].GetComponentInChildren<TMP_Text>().text = asset.dialogueOption[dialogueIndex + i * asset.rowCount];
                Vector3 tempPos = dialogueOptionButtons[i].GetComponent<RectTransform>().anchoredPosition;
                //Debug.Log(tempPos);
                tempPos.x = -screenWidth / 8;
                
                tempPos.y = choiceCount * 75 + i * -150;
                //Debug.Log(tempPos);
                dialogueOptionButtons[i].GetComponent<RectTransform>().anchoredPosition = tempPos;
                //Debug.Log(dialogueOptionButtons[i].GetComponent<RectTransform>().position);
            }
        }
    }

    public void ChoiceButton(int buttonOption)
    {
        if (asset.variableChange[dialogueIndex + buttonOption * asset.rowCount] != "")
        {
            RelationshipManager.politiciaDict[asset.variableChange[dialogueIndex + buttonOption * asset.rowCount]] += ((int)asset.valueChange[dialogueIndex + buttonOption * asset.rowCount]);
        }
        else if (asset.dialogueOption[dialogueIndex + buttonOption * asset.rowCount] == "Restart")
        {
            dialogueIndex = 0;
            RelationshipManager.politiciaDict["relationValue"] = 5;
        }
        else if (asset.dialogueOption[dialogueIndex + buttonOption * asset.rowCount] == "Exit")
        {
            Application.Quit();
            dialogueIndex = 0;
        }
        DialogueProgress(asset.exit[dialogueIndex + buttonOption * asset.rowCount]);
    }

    public void DialogueProgress(string destination)
    {
        if (textComponent.text.Length < textBase.Length)
        {
            readingCharIndex = textBase.Length;
            SetupDialogueChoices();
        }
        else
        {
            //Debug.Log(asset.dialogueExit[dialogueIndex]);
            if (asset.dialogueExit[dialogueIndex] != "")
            {
                destination = asset.dialogueExit[dialogueIndex];
            }
            //Debug.Log(destination);
            if (destination != "")
            {
                while (asset.dialogueEnter[dialogueIndex] != destination)
                {
                    dialogueIndex++;
                }
            }
            else
            {
                dialogueIndex++;
            }
            textBase = asset.dialogueText[dialogueIndex];
            readingCharIndex = 0;
            dialogueButton.enabled = true;
            nextTriangle.enabled = false;
            for (int i = 0; i < choiceCount; i++)
            {
                dialogueOptionButtons[i].SetActive(false);
            }
            textComponent.text = "";

            if (asset.isLeftSpeaking[dialogueIndex])
            {
                nameText.text = asset.leftCharacter[dialogueIndex];
            }
            else nameText.text = asset.rightCharacter[dialogueIndex];

            Color tempColor = Color.white;
            switch (asset.rightExpression[dialogueIndex])
            {
                case Expressions.Neutral:
                    tempColor = Color.white;
                    break;
                case Expressions.Sad:
                    tempColor = Color.blue;
                    break;
                case Expressions.Angry:
                    tempColor = Color.red;
                    break;
            }

            characterPortait.GetComponent<Image>().color = tempColor;
        }
    }

    
}
