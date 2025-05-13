using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsocijacijeGame : MonoBehaviour
{
    [System.Serializable]
    public class Column
    {
        public string[] clues = new string[4];
        public string solution;
        public bool isRevealed = false;
    }

    public Column[] columns = new Column[4];
    public string finalSolution;

    public Text[] clueTexts;         // 16 clue Texts (A1–D4)
    public Text[] columnSolutions;   // 4 Texts for A, B, C, D
    public Text finalSolutionText;   // Final solution Text

    public InputField[] columnInputs;    // 4 InputFields for A–D guesses
    public InputField finalInput;

    public Button[] clueButtons;         // 16 Buttons for clues
    public Button[] checkColumnButtons;  // 4 Buttons to check A–D
    public Button checkFinalButton;

    private void Start()
    {
        InitializeHardcodedData();
        UpdateUI();
    }

    private void InitializeHardcodedData()
    {
        // Column A - FRUIT
        columns[0] = new Column
        {
            clues = new string[] { "Apple", "Banana", "Orange", "Grapes" },
            solution = "Fruit"
        };

        // Column B - COLORS
        columns[1] = new Column
        {
            clues = new string[] { "Red", "Blue", "Green", "Yellow" },
            solution = "Color"
        };

        // Column C - ANIMALS
        columns[2] = new Column
        {
            clues = new string[] { "Dog", "Cat", "Elephant", "Tiger" },
            solution = "Animal"
        };

        // Column D - VEHICLES
        columns[3] = new Column
        {
            clues = new string[] { "Car", "Bike", "Train", "Plane" },
            solution = "Vehicle"
        };

        // Final solution - CATEGORIES
        finalSolution = "Categories";
    }

    public void RevealClue(int index)
    {
        int col = index / 4;
        int row = index % 4;
        clueTexts[index].text = columns[col].clues[row];
        clueButtons[index].interactable = false;
    }

    public void CheckColumn(int index)
    {
        if (columnInputs[index].text.Trim().ToLower() == columns[index].solution.Trim().ToLower())
        {
            columnSolutions[index].text = columns[index].solution;
            columns[index].isRevealed = true;
            columnInputs[index].interactable = false;
            checkColumnButtons[index].interactable = false;
            RevealFullColumn(index);
        }
    }

    public void CheckFinal()
    {
        if (finalInput.text.Trim().ToLower() == finalSolution.Trim().ToLower())
        {
            finalSolutionText.text = finalSolution;
            finalInput.interactable = false;
            checkFinalButton.interactable = false;
            RevealAll();
        }
    }

    private void RevealFullColumn(int col)
    {
        for (int i = 0; i < 4; i++)
        {
            int idx = col * 4 + i;
            clueTexts[idx].text = columns[col].clues[i];
            clueButtons[idx].interactable = false;
        }
    }

    private void RevealAll()
    {
        for (int col = 0; col < 4; col++)
        {
            RevealFullColumn(col);
            columnSolutions[col].text = columns[col].solution;
        }

        finalSolutionText.text = finalSolution;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < 16; i++)
        {
            clueTexts[i].text = "???";
            clueButtons[i].interactable = true;
        }

        for (int i = 0; i < 4; i++)
        {
            columnSolutions[i].text = "???";
            columnInputs[i].interactable = true;
            checkColumnButtons[i].interactable = true;
        }

        finalSolutionText.text = "???";
        finalInput.interactable = true;
        checkFinalButton.interactable = true;
    }
}
