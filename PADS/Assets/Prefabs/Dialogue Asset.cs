using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAsset", menuName = "Scriptable Objects/DialogueAsset")]
public class DialogueAsset : ScriptableObject
{
    public int rowCount;

    public string[] leftCharacter;
    public DialogueManager.Expressions[] leftExpression;

    public string[] rightCharacter;
    public DialogueManager.Expressions[] rightExpression;

    public bool[] isLeftSpeaking;
    public bool[] endDialogue;

    public string[] dialogueEnter;
    public string[] dialogueText;
    public string[] dialogueExit;

    public string[] valueCheck;
    public DialogueManager.Conditionals[] conditional;
    public float[] requirement;
    public string[] dialogueOption;
    public string[] variableChange;
    public float[] valueChange;
    public string[] exit;
}
