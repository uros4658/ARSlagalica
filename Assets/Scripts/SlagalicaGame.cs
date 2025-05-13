using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SlagalicaGame : MonoBehaviour
{
    public InputField userInput;
    public Text timerText;
    public Transform letterButtonsParent; // Panel with layout group
    public GameObject buttonPrefab;
    public Button submitButton, checkButton, deleteButton, eraseButton;

    private Stack<Button> clickedButtons = new Stack<Button>();
    private float timeLeft = 60f;
    private bool timerRunning = true;

    private List<char> randomLetters = new List<char>();
    private SlagalicaController controller;

    void Start()
    {
        controller = new SlagalicaController(); // Your game logic controller
        GenerateLetters();
        StartCoroutine(Timer());
        SetupButtons();
    }

    void GenerateLetters()
    {
        randomLetters = controller.GetRandomLetters(); // E.g., returns ['A', 'B', 'C', ...]
        foreach (char c in randomLetters)
        {
            GameObject btnObj = Instantiate(buttonPrefab, letterButtonsParent);
            Button btn = btnObj.GetComponent<Button>();
            Text btnText = btn.GetComponentInChildren<Text>();
            btnText.text = c.ToString();
            btn.onClick.AddListener(() => OnLetterButtonClick(btn, c));
        }
        controller.StartLongestWordFinder(randomLetters);
    }

    void OnLetterButtonClick(Button btn, char c)
    {
        btn.interactable = false;
        clickedButtons.Push(btn);
        userInput.text += c;
    }

    public void Submit()
    {
        EndGame();
    }

    public void CheckWord()
    {
        string word = userInput.text.ToLower();
        if (controller.CheckWord(word))
        {
            userInput.image.color = Color.green;
        }
        else
        {
            userInput.image.color = Color.red;
        }
    }

    public void DeleteLetter()
    {
        userInput.image.color = Color.white;
        if (clickedButtons.Count > 0)
        {
            Button lastButton = clickedButtons.Pop();
            lastButton.interactable = true;
            userInput.text = userInput.text.Substring(0, userInput.text.Length - 1);
        }
    }

    public void EraseAll()
    {
        userInput.image.color = Color.white;
        userInput.text = "";
        while (clickedButtons.Count > 0)
        {
            Button btn = clickedButtons.Pop();
            btn.interactable = true;
        }
    }

    IEnumerator Timer()
    {
        while (timerRunning && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeLeft).ToString();
            yield return null;
        }
        if (timeLeft <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        timerRunning = false;
        StopAllCoroutines();
        string finalWord = userInput.text.ToLower();
        int points = controller.CheckWord(finalWord) ? finalWord.Length * 2 : 0;
        string solution = controller.LongestWord();

        // Display result dialog (you can use Unity's UI pop-up)
        Debug.Log($"Game Over! Points: {points}. Our solution: {solution}");
    }

    void SetupButtons()
    {
        submitButton.onClick.AddListener(Submit);
        checkButton.onClick.AddListener(CheckWord);
        deleteButton.onClick.AddListener(DeleteLetter);
        eraseButton.onClick.AddListener(EraseAll);
    }
}
