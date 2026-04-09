using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public enum Expressions
    {
        Sad = 0,
        Neutral = 1,
        Loving = 2,
        Horny = 3,
        Happy = 4,
        Embarrased = 5,
        Bored = 6,
        Astonished = 7,
        Angry = 8
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
    public Image nameBox;
    public Sprite[] backgroundSprites;
    public Image backgroundImage;
    public Image backgroundTransition;
    float transitionTimer = 1;

    float screenWidth = 1920;
    float screenHeight = 1080;

    int choiceCount = 4;

    public Dictionary <Expressions, Sprite> expressionDict = new();
    public Sprite[] expressionSprites;

    public DialogueInfoData dialogueInfoData;
    float textTimer = 0;

    public GameObject shipScreen;
    public GameObject dialogueScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        dialogueButton = GetComponent<Button>();

        for (int i = 0; i < expressionSprites.Length; i++)
        {
            Expressions expIndex = (Expressions)i;
            expressionDict.Add(expIndex, expressionSprites[i]);
            Debug.Log((Expressions)i);
        }
        Debug.Log(expressionDict[(Expressions)1]);
        Debug.Log(expressionDict[Expressions.Happy]);
        dialogueInfoData = new DialogueInfoData()
        {
            text = "",
            dialogueIndex = 0,
            timeSpent = 0,
            skipTime = 0,
            relationShipValue = 0
        };

        DialogueProgress("Intro");
    }

    // Update is called once per frame
    void Update()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        //dialogueButton.enabled = textComponent.text.Length < textBase.Length;

        textTimer += Time.deltaTime;

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

        if (transitionTimer > 0)
        {
            transitionTimer -= Time.deltaTime;
            backgroundTransition.color = new Color(1,1,1,transitionTimer);
        }
    }

    [System.Serializable]
    public struct DialogueInfoData
    {
        public string text;
        public int dialogueIndex;
        public float timeSpent;
        public float skipTime;
        public int relationShipValue;
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

                dialogueOptionButtons[i].GetComponent<Button>().interactable = true;
                dialogueOptionButtons[i].GetComponent<Image>().color = Color.white;

                if (asset.valueCheck[dialogueIndex + i * asset.rowCount] != "")
                {
                    int rollValue = Random.Range(1, 6);
                    switch (asset.valueCheck[dialogueIndex + i * asset.rowCount])
                    {
                        case ("Body"):
                            if (CrewmateManager.endStats[0] + rollValue > asset.requirement[dialogueIndex + i * asset.rowCount])
                            {
                                continue;
                            }
                            break;
                        case ("Mind"):
                            if (CrewmateManager.endStats[1] + rollValue > asset.requirement[dialogueIndex + i * asset.rowCount])
                            {
                                continue;
                            }
                            break;
                        case ("Soul"):
                            if (CrewmateManager.endStats[2] + rollValue > asset.requirement[dialogueIndex + i * asset.rowCount])
                            {
                                continue;
                            }
                            break;
                        case ("Speechcraft"):
                            if (!(CrewmateManager.crewOneIndex == 2 || CrewmateManager.crewTwoIndex  == 2))
                            {
                                continue;
                            }
                            break;
                        case ("Culture"):
                            if (!(CrewmateManager.crewOneIndex == 1 || CrewmateManager.crewTwoIndex == 1))
                            {
                                continue;
                            }
                            break;
                        case ("Manuverability"):
                            if (!(CrewmateManager.crewOneIndex == 3 || CrewmateManager.crewTwoIndex == 3))
                            {
                                continue;
                            }
                            break;
                    }
                    dialogueOptionButtons[i].GetComponent<Button>().interactable = false;
                    dialogueOptionButtons[i].GetComponent<Image>().color = Color.grey;
                }
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

        TelemetryLogger.Log(this, "Dialogue Choice", asset.variableChange[dialogueIndex + buttonOption * asset.rowCount]);

        DialogueProgress(asset.exit[dialogueIndex + buttonOption * asset.rowCount]);
    }

    public void DialogueProgress(string destination)
    {
        Debug.Log("dialogue progress " + destination);
        if (textComponent.text.Length < textBase.Length)
        {
            readingCharIndex = textBase.Length;
            dialogueInfoData.skipTime = textTimer;
            SetupDialogueChoices();
        }
        else if (readingCharIndex >= textBase.Length && asset.endDialogue[dialogueIndex])
        {
            readingCharIndex = 0;
            shipScreen.SetActive(true);
            dialogueScreen.SetActive(false);
            Gamestate.gameProgressionIndex++;
        }
        else
        {
            dialogueInfoData.text = textBase;
            dialogueInfoData.dialogueIndex = dialogueIndex;
            dialogueInfoData.timeSpent = textTimer;
            dialogueInfoData.relationShipValue = RelationshipManager.politiciaDict["relationValue"];
            TelemetryLogger.Log(this, "Dialogue Information", dialogueInfoData);

            dialogueInfoData = new DialogueInfoData()
            {
                text = "",
                dialogueIndex = 0,
                timeSpent = 0,
                skipTime = 0,
                relationShipValue = RelationshipManager.politiciaDict["relationValue"]
            };

            textTimer = 0;
            //Debug.Log(asset.dialogueExit[dialogueIndex]);
            if (asset.dialogueExit[dialogueIndex] != "")
            {
                destination = asset.dialogueExit[dialogueIndex];
            }
            //Debug.Log(destination);
            if (destination != "")
            {
                if (destination == "Gamestate")
                {
                    destination = Gamestate.dialogueKey;
                    Debug.Log(Gamestate.dialogueKey + " " + Gamestate.gameProgressionIndex);
                    dialogueIndex = 0;
                }
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
                nameBox.enabled = (asset.leftCharacter[dialogueIndex] != "");
            }
            else
            {
                nameText.text = asset.rightCharacter[dialogueIndex];
                nameBox.enabled = (asset.rightCharacter[dialogueIndex] != "");
            }

            switch (dialogueIndex)
            {
                case 59:
                    backgroundTransition.sprite = backgroundImage.sprite;
                    backgroundImage.sprite = backgroundSprites[0];
                    transitionTimer = 1;
                    break;
                case 72:
                    backgroundTransition.sprite = backgroundImage.sprite;
                    backgroundImage.sprite = backgroundSprites[1];
                    transitionTimer = 1;
                    break;
                case 112:
                    backgroundTransition.sprite = backgroundImage.sprite;
                    backgroundImage.sprite = backgroundSprites[2];
                    transitionTimer = 1;
                    break;
                case 151:
                    backgroundTransition.sprite = backgroundImage.sprite;
                    backgroundImage.sprite = backgroundSprites[3];
                    transitionTimer = 1;
                    break;
            }

            characterPortait.SetActive(asset.rightCharacter[dialogueIndex] == "Politicia");
            //Debug.Log(expressionDict[Expressions.Sad]);

            Sprite exp;
            switch (asset.rightExpression[dialogueIndex])
            {
                case Expressions.Sad:
                    exp = expressionDict[Expressions.Sad];
                    break;
                case Expressions.Loving:
                    exp = expressionDict[Expressions.Loving];
                    break;
                case Expressions.Horny:
                    exp = expressionDict[Expressions.Horny];
                    break;
                case Expressions.Happy:
                    exp = expressionDict[Expressions.Happy];
                    break;
                case Expressions.Embarrased:
                    exp = expressionDict[Expressions.Embarrased];
                    break;
                case Expressions.Bored:
                    exp = expressionDict[Expressions.Bored];
                    break;
                case Expressions.Astonished:
                    exp = expressionDict[Expressions.Astonished];
                    break;
                case Expressions.Angry:
                    exp = expressionDict[Expressions.Angry];
                    break;
                default:
                    exp = expressionDict[Expressions.Neutral];
                    break;
            }

            characterPortait.GetComponent<Image>().sprite = exp;

        }
    }

    
}
