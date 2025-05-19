/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshProUGUI and TMP_InputField


public class MojBrojGame : MonoBehaviour
{
    public TextMeshProUGUI targetText;
    public TMP_InputField userInput;
    public TextMeshProUGUI timerText;
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public GameObject numberButtonPrefab;
    public Transform numberButtonContainer;
    public List<Button> operatorButtons;

    private Stack<Button> allButtons = new Stack<Button>();
    private Stack<bool> typeOfClicked = new Stack<bool>(); // true = number, false = operator
    private float currentTime;
    private bool timerRunning;
    private int totalTime = 60;
    private int targetNumber;
    private List<int> numbers = new List<int>();

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        timerRunning = true;
        currentTime = totalTime;

        numbers = GenerateNumbers(6);
        targetNumber = numbers[0];
        targetText.text = "Target: " + targetNumber.ToString();

        for (int i = 1; i < numbers.Count; i++)
        {
            GameObject btnObj = Instantiate(numberButtonPrefab, numberButtonContainer);
            Button btn = btnObj.GetComponent<Button>();
            btn.GetComponentInChildren<TextMeshProUGUI>().text = numbers[i].ToString();
            btn.onClick.AddListener(() => OnNumberClick(btn));
        }

        foreach (var opBtn in operatorButtons)
        {
            opBtn.onClick.AddListener(() => OnOperatorClick(opBtn));
        }
    }

    void Update()
    {
        if (!timerRunning) return;

        currentTime -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(currentTime).ToString();

        if (currentTime <= 0)
        {
            EndGame();
        }
    }

    void OnNumberClick(Button btn)
    {
        if (typeOfClicked.Count > 0 && typeOfClicked.Peek()) return;
        btn.interactable = false;
        typeOfClicked.Push(true);
        allButtons.Push(btn);
        userInput.text += btn.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    void OnOperatorClick(Button btn)
    {
        if (typeOfClicked.Count == 0 || !typeOfClicked.Peek()) return;
        typeOfClicked.Push(false);
        allButtons.Push(btn);
        userInput.text += " " + btn.GetComponentInChildren<TextMeshProUGUI>().text + " ";
    }

    public void Submit()
    {
        EndGame();
    }

    void EndGame()
    {
        timerRunning = false;

        int result = EvaluateExpression(userInput.text);
        int score = 0;
        int diff = Mathf.Abs(result - targetNumber);

        if (diff == 0) score = 10;
        else if (diff <= 5) score = 5;

        dialogPanel.SetActive(true);
        dialogText.text = $"Game Over! You scored {score} points.\nClosest to target: {targetNumber}, Your result: {result}";
    }

    List<int> GenerateNumbers(int count)
    {
        List<int> nums = new List<int>();
        System.Random rand = new System.Random();
        nums.Add(rand.Next(100, 1000)); // Target number
        for (int i = 1; i < count; i++)
        {
            nums.Add(rand.Next(1, 10));
        }
        return nums;
    }

    int EvaluateExpression(string expression)
    {
        try
        {
            var dt = new System.Data.DataTable();
            return Convert.ToInt32(dt.Compute(expression, ""));
        }
        catch
        {
            return 0;
        }
    }
}
*/