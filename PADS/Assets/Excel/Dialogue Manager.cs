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

    public string[] leftCharacter;
    public Expressions[] leftExpression;

    public string[] rightCharacter;
    public Expressions[] rightExpression;

    public bool[] isLeftSpeaking;
    public bool[] endDialogue;

    public string[] dialogueEnter;
    public string[] dialogueText;
    public string[] dialogueExit;

    public string[,] valueCheck;
    public Conditionals[,] conditional;
    public float[,] requirement;
    public string[,] dialogueOption;
    public string[,] variableChange;
    public float[,] valueChange;
    public string[,] exit;

    public string textBase;
    public TMP_Text textComponent;
    public TMP_Text nameText;
    public GameObject characterPortait;
    [SerializeField] static int dialogueIndex = 1;
    public float readingCharIndex = 0;
    public GameObject[] dialogueOptionButtons;
    [SerializeField] float textSpeed = 30f;
    public Button dialogueButton;
    public Image nextTriangle;

    float screenWidth = 1920;
    float screenHeight = 1080;

    int choiceCount = 4;

    [Tooltip("Path to the Excel workbook to open. Paths are relative to 'Assets/'")]
    [SerializeField] string excelFilePath = "Excel/PoliticiaDialogueSpreadsheet.xlsx";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        dialogueButton = GetComponent<Button>();
        
        var excel = new ExcelImporter(excelFilePath);
        ImportText(excel);
        Debug.Log("Finished Importing!");

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
                Debug.Log(textBase.Length);
                if (readingCharIndex >= textBase.Length - 1)
                {
                    readingCharIndex = textBase.Length;
                    Debug.Log("Makin choices");
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
        if (dialogueOption[dialogueIndex, 0] == null)
        {
            nextTriangle.enabled = true;
        }
        else
        {
            dialogueButton.enabled = false;
            choiceCount = 0;
            for (int i = 0; i < 4; i++)
            {
                Debug.Log(dialogueOption[dialogueIndex, i]);
                if (dialogueOption[dialogueIndex, i] != null)
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
                dialogueOptionButtons[i].GetComponentInChildren<TMP_Text>().text = dialogueOption[dialogueIndex, i];
                Vector3 tempPos = dialogueOptionButtons[i].GetComponent<RectTransform>().anchoredPosition;
                Debug.Log(tempPos);
                tempPos.x = -screenWidth / 8;
                
                tempPos.y = choiceCount * 75 + i * -150;
                Debug.Log(tempPos);
                dialogueOptionButtons[i].GetComponent<RectTransform>().anchoredPosition = tempPos;
                Debug.Log(dialogueOptionButtons[i].GetComponent<RectTransform>().position);
            }
        }
    }

    public void ChoiceButton(int buttonOption)
    {
        if (variableChange[dialogueIndex, buttonOption] != null)
        {
            RelationshipManager.politiciaDict[variableChange[dialogueIndex, buttonOption]] += ((int)valueChange[dialogueIndex, buttonOption]);
            
        }
        DialogueProgress(exit[dialogueIndex, buttonOption]);
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
            Debug.Log(dialogueExit[dialogueIndex]);
            if (dialogueExit[dialogueIndex] != null)
            {
                destination = dialogueExit[dialogueIndex];
            }
            Debug.Log(destination);
            if (destination != "")
            {
                while (dialogueEnter[dialogueIndex] != destination)
                {
                    dialogueIndex++;
                }
            }
            else
            {
                dialogueIndex++;
            }
            textBase = dialogueText[dialogueIndex];
            readingCharIndex = 0;
            dialogueButton.enabled = true;
            nextTriangle.enabled = false;
            for (int i = 0; i < choiceCount; i++)
            {
                dialogueOptionButtons[i].SetActive(false);
            }
            textComponent.text = "";

            if (isLeftSpeaking[dialogueIndex])
            {
                nameText.text = leftCharacter[dialogueIndex];
            }
            else nameText.text = rightCharacter[dialogueIndex];

            Color tempColor = Color.white;
            switch (rightExpression[dialogueIndex])
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

    public void ImportText(ExcelImporter excel)
    {
        if (!excel.TryGetTable("Dialogue", out var table))
        {
            Debug.LogError($"Could not find 'Dialogue' table in {excelFilePath}");
            return;
        }

        leftCharacter = new string[table.RowCount];
        leftExpression = new Expressions[table.RowCount];

        rightCharacter = new string[table.RowCount];
        rightExpression = new Expressions[table.RowCount];

        isLeftSpeaking = new bool[table.RowCount];
        endDialogue = new bool[table.RowCount];

        dialogueEnter = new string[table.RowCount];
        dialogueText = new string[table.RowCount];
        dialogueExit = new string[table.RowCount];

        valueCheck = new string[table.RowCount, 4];
        conditional = new Conditionals[table.RowCount, 4];
        requirement = new float[table.RowCount, 4];
        dialogueOption = new string[table.RowCount, 4];
        variableChange = new string[table.RowCount, 4];
        valueChange = new float[table.RowCount, 4];
        exit = new string[table.RowCount, 4];

        for (int row = 1; row < table.RowCount; row++)
        {
            dialogueText[row] = table.GetValue<string>(row, "Dialogue Text");
            if (string.IsNullOrWhiteSpace(dialogueText[row])) continue; // Skip blank rows

            // Left Character
            leftCharacter[row] = table.GetValue<string>(row, "Left Character");
            if (table.TryGetEnum<DialogueManager.Expressions>(row, "Left Expression", out var expressionL))
            {
                leftExpression[row] = expressionL;
            }

            // Right Character
            rightCharacter[row] = table.GetValue<string>(row, "Right Character");
            if (table.TryGetEnum<DialogueManager.Expressions>(row, "Right Expression", out var expressionR))
            {
                rightExpression[row] = expressionR;
            }

            // Text Progression
            isLeftSpeaking[row] = !string.IsNullOrWhiteSpace(table.GetValue<string>(row, "Is Left Speaking?"));
            endDialogue[row] = !string.IsNullOrWhiteSpace(table.GetValue<string>(row, "End Dialogue?"));

            // Dialogue Navigation
            dialogueEnter[row] = table.GetValue<string>(row, "Dialogue Enter");
            dialogueExit[row] = table.GetValue<string>(row, "Dialogue Exit");

            // Option A
            valueCheck[row, 0] = table.GetValue<string>(row, "Value Check A");
            if (table.TryGetEnum<DialogueManager.Conditionals>(row, "Conditional A", out var conditionA))
            {
                conditional[row,0] = conditionA;
            }
            requirement[row,0] = table.GetValue<float>(row, "Requirement A");
            dialogueOption[row, 0] = table.GetValue<string>(row, "Dialogue Option A");
            variableChange[row, 0] = table.GetValue<string>(row, "Variable Change A");
            valueChange[row, 0] = table.GetValue<float>(row, "Value Change A");
            exit[row, 0] = table.GetValue<string>(row, "Exit A");

            // Option B
            valueCheck[row, 1] = table.GetValue<string>(row, "Value Check B");
            if (table.TryGetEnum<DialogueManager.Conditionals>(row, "Conditional B", out var conditionB))
            {
                conditional[row, 1] = conditionB;
            }
            requirement[row,1] = table.GetValue<float>(row, "Requirement B");
            dialogueOption[row, 1] = table.GetValue<string>(row, "Dialogue Option B");
            variableChange[row, 1] = table.GetValue<string>(row, "Variable Change B");
            valueChange[row, 1] = table.GetValue<float>(row, "Value Change B");
            exit[row, 1] = table.GetValue<string>(row, "Exit B");

            // Option C
            valueCheck[row,2] = table.GetValue<string>(row, "Value Check C");
            if (table.TryGetEnum<DialogueManager.Conditionals>(row, "Conditional C", out var conditionC))
            {
                conditional[row, 2] = conditionC;
            }
            requirement[row, 2] = table.GetValue<float>(row, "Requirement C");
            dialogueOption[row, 2] = table.GetValue<string>(row, "Dialogue Option C");
            variableChange[row, 2] = table.GetValue<string>(row, "Variable Change C");
            valueChange[row, 2] = table.GetValue<float>(row, "Value Change C");
            exit[row, 2] = table.GetValue<string>(row, "Exit C");

            // Option D
            valueCheck[row, 3] = table.GetValue<string>(row, "Value Check D");
            if (table.TryGetEnum<DialogueManager.Conditionals>(row, "Conditional D", out var conditionD))
            {
                conditional[row, 3] = conditionD;
            }
            requirement[row, 3] = table.GetValue<float>(row, "Requirement D");
            dialogueOption[row, 3] = table.GetValue<string>(row, "Dialogue Option D");
            variableChange[row, 3] = table.GetValue<string>(row, "Variable Change D");
            valueChange[row, 3] = table.GetValue<float>(row, "Value Change D");
            exit[row, 3] = table.GetValue<string>(row, "Exit D");

            //var item = DataHelper.GetOrCreateAsset(name, items, category);
            //if (string.IsNullOrWhiteSpace(item.displayName))
            //{
            //    item.displayName = name;
            //}

            //var description = table.GetValue<string>(row, "Description");
            //if (string.IsNullOrWhiteSpace(description)) continue; // Skip blank rows

            //if (string.IsNullOrWhiteSpace(item.description))
            //{
            //    item.description = description;
            //}

            //item.cost = table.GetValue<int>(row, "Cost");
            //if (table.TryGetEnum<Rarity>(row, "Rarity", out var rarity))
            //{
            //    item.rarity = rarity;
            //}

            //Debug.Log(name);
        }
    }
}
