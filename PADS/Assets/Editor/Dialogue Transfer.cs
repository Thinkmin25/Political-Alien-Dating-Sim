using UnityEngine;

[CreateAssetMenu(fileName = "DialogueTransfer", menuName = "Scriptable Objects/DialogueTransfer")]
public class DialogueTransfer : ScriptableObject
{

    [Tooltip("Path to the Excel workbook to open. Paths are relative to 'Assets/'")]
    [SerializeField] string excelFilePath = "Editor/PoliticiaDialogueSpreadsheet.xlsx";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public DialogueAsset asset;

    [ContextMenu("Run Import")]
    public void RefreshDialogue()
    {
        var excel = new ExcelImporter(excelFilePath);
        ImportText(excel);
        Debug.Log("Finished Importing!");
    }

    
    public void ImportText(ExcelImporter excel)
    {

        if (!excel.TryGetTable("Dialogue", out var table))
        {
            Debug.LogError($"Could not find 'Dialogue' table in {excelFilePath}");
            return;
        }
        
        asset.leftCharacter = new string[table.RowCount];
        asset.leftExpression = new DialogueManager.Expressions[table.RowCount];

        asset.rightCharacter = new string[table.RowCount];
        asset.rightExpression = new DialogueManager.Expressions[table.RowCount];

        asset.isLeftSpeaking = new bool[table.RowCount];
        asset.endDialogue = new bool[table.RowCount];

        asset.dialogueEnter = new string[table.RowCount];
        asset.dialogueText = new string[table.RowCount];
        asset.dialogueExit = new string[table.RowCount];

        asset.valueCheck = new string[table.RowCount * 4];
        asset.conditional = new DialogueManager.Conditionals[table.RowCount * 4];
        asset.requirement = new float[table.RowCount * 4];
        asset.dialogueOption = new string[table.RowCount * 4];
        asset.variableChange = new string[table.RowCount * 4];
        asset.valueChange = new float[table.RowCount * 4];
        asset.exit = new string[table.RowCount * 4];

        asset.rowCount = table.RowCount;

        for (int row = 1; row < table.RowCount; row++)
        {
            asset.dialogueText[row] = table.GetValue<string>(row, "Dialogue Text");
            if (string.IsNullOrWhiteSpace(asset.dialogueText[row])) continue; // Skip blank rows

            
            // Left Character
            asset.leftCharacter[row] = table.GetValue<string>(row, "Left Character");
            if (table.TryGetEnum<DialogueManager.Expressions>(row, "Left Expression", out var expressionL))
            {
                asset.leftExpression[row] = expressionL;
            }

            // Right Character
            asset.rightCharacter[row] = table.GetValue<string>(row, "Right Character");
            if (table.TryGetEnum<DialogueManager.Expressions>(row, "Right Expression", out var expressionR))
            {
                asset.rightExpression[row] = expressionR;
            }

            // Text Progression
            asset.isLeftSpeaking[row] = !string.IsNullOrWhiteSpace(table.GetValue<string>(row, "Is Left Speaking?"));
            asset.endDialogue[row] = !string.IsNullOrWhiteSpace(table.GetValue<string>(row, "End Dialogue?"));

            // Dialogue Navigation
            asset.dialogueEnter[row] = table.GetValue<string>(row, "Dialogue Enter");
            asset.dialogueExit[row] = table.GetValue<string>(row, "Dialogue Exit");

            // Option A
            asset.valueCheck[row] = table.GetValue<string>(row, "Value Check A");
            if (table.TryGetEnum<DialogueManager.Conditionals>(row, "Conditional A", out var conditionA))
            {
                asset.conditional[row] = conditionA;
            }
            asset.requirement[row] = table.GetValue<float>(row, "Requirement A");
            asset.dialogueOption[row] = table.GetValue<string>(row, "Dialogue Option A");
            asset.variableChange[row] = table.GetValue<string>(row, "Variable Change A");
            asset.valueChange[row] = table.GetValue<float>(row, "Value Change A");
            asset.exit[row] = table.GetValue<string>(row, "Exit A");

            // Option B
            asset.valueCheck[row + table.RowCount] = table.GetValue<string>(row, "Value Check B");
            if (table.TryGetEnum<DialogueManager.Conditionals>(row, "Conditional B", out var conditionB))
            {
                asset.conditional[row + table.RowCount] = conditionB;
            }
            asset.requirement[row + table.RowCount] = table.GetValue<float>(row, "Requirement B");
            asset.dialogueOption[row + table.RowCount] = table.GetValue<string>(row, "Dialogue Option B");
            asset.variableChange[row + table.RowCount] = table.GetValue<string>(row, "Variable Change B");
            asset.valueChange[row + table.RowCount] = table.GetValue<float>(row, "Value Change B");
            asset.exit[row + table.RowCount] = table.GetValue<string>(row, "Exit B");

            // Option C
            asset.valueCheck[row + table.RowCount * 2] = table.GetValue<string>(row, "Value Check C");
            if (table.TryGetEnum<DialogueManager.Conditionals>(row, "Conditional C", out var conditionC))
            {
                asset.conditional[row + table.RowCount * 2] = conditionC;
            }
            asset.requirement[row + table.RowCount * 2] = table.GetValue<float>(row, "Requirement C");
            asset.dialogueOption[row + table.RowCount * 2] = table.GetValue<string>(row, "Dialogue Option C");
            asset.variableChange[row + table.RowCount * 2] = table.GetValue<string>(row, "Variable Change C");
            asset.valueChange[row + table.RowCount * 2] = table.GetValue<float>(row, "Value Change C");
            asset.exit[row + table.RowCount * 2] = table.GetValue<string>(row, "Exit C");

            // Option D
            asset.valueCheck[row + table.RowCount * 3] = table.GetValue<string>(row, "Value Check D");
            if (table.TryGetEnum<DialogueManager.Conditionals>(row, "Conditional D", out var conditionD))
            {
                asset.conditional[row + table.RowCount * 3] = conditionD;
            }
            asset.requirement[row + table.RowCount * 3] = table.GetValue<float>(row, "Requirement D");
                asset.dialogueOption[row + table.RowCount * 3] = table.GetValue<string>(row, "Dialogue Option D");
            asset.variableChange[row + table.RowCount * 3] = table.GetValue<string>(row, "Variable Change D");
            asset.valueChange[row + table.RowCount * 3] = table.GetValue<float>(row, "Value Change D");
            asset.exit[row + table.RowCount * 3] = table.GetValue<string>(row, "Exit D");

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
