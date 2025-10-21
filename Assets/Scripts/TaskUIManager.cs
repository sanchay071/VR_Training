using UnityEngine;
using TMPro;

public class TaskUIManager : MonoBehaviour
{
    public TextMeshProUGUI taskCounterText;
    public TextMeshProUGUI instructionText;

    private int currentTask = 0;
    private string[] instructions = {
        "Open the rear door",
        "Grab and apply masking tape",
        "Identify cutting tool",
        "Cut the glass"
    };

    void Start()
    {
        UpdateUI();
    }

    public void CompleteTask()
    {
        currentTask++;
        UpdateUI();
    }

    void UpdateUI()
    {
        // Update counter
        taskCounterText.text = $"Tasks Completed: {currentTask}/4";

        // Update instruction (if any left)
        if (currentTask < instructions.Length)
            instructionText.text = $"Task: {instructions[currentTask]}";
        else
            instructionText.text = "All tasks completed!";
    }
}
