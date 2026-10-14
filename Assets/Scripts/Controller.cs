using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] public Transform rowsImages;

    [SerializeField] private Button Backspace;
    [SerializeField] private Button Submit;

    public static Dictionary<int, string> verityList = new Dictionary<int, string>
    {
        { 1, "Verity" },
        { 2, "Cruelty" },
        { 3, "Lovity" },
        { 4, "Falsity" }
    };

    public event System.Action<int> OnIndexChanged;

    public static int[] answerArray = new int[8];

    public static int[] inputArray = new int[8];

    private int _currentIndex = 0;

    public int currentIndex
    {
        get { return _currentIndex; }

        set
        {
            _currentIndex = value;

            OnIndexChanged?.Invoke(_currentIndex);
        }

    }

    public static int currentRow = 0;

    private void Start()
    {
        Submit.interactable = false;

        OnIndexChanged += UpdateSubmit;

        for (int i = 0; i < answerArray.Length; i++)
        {
            answerArray[i] = Random.Range(1, 5);
        }

        Debug.Log("Answer Array: " + string.Join(", ", answerArray));
    }

    private void Update()
    {
        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            onBackspace();
        }
    }

    public void OnAddVerity()
    {
        if (currentIndex < 8)
        {
            Debug.Log("Adding Verity at index: " + currentIndex + " | Row: " + currentRow);

            Transform index = rowsImages.GetChild(currentRow).GetChild(currentIndex);

            index.gameObject.SetActive(true);
            index.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Verity");

            inputArray[currentIndex] = 1;
            currentIndex++;
        }
    }

    public void OnAddCruelty()
    {
        if (currentIndex < 8)
        {
            Debug.Log("Adding Cruelty at index: " + currentIndex + " | Row: " + currentRow);

            Transform index = rowsImages.GetChild(currentRow).GetChild(currentIndex);

            index.gameObject.SetActive(true);
            index.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Cruelty");

            inputArray[currentIndex] = 2;
            currentIndex++;
        }
    }

    public void OnAddLovity()
    {
        if (currentIndex < 8)
        {
            Debug.Log("Adding Lovity at index: " + currentIndex + " | Row: " + currentRow);

            Transform index = rowsImages.GetChild(currentRow).GetChild(currentIndex);

            index.gameObject.SetActive(true);
            index.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Lovity");

            inputArray[currentIndex] = 3;
            currentIndex++;
        }
    }

    public void OnAddFalsity()
    {
        if (currentIndex < 8)
        {
            Debug.Log("Adding Falsity at index: " + currentIndex + " | Row: " + currentRow);

            Transform index = rowsImages.GetChild(currentRow).GetChild(currentIndex);

            index.gameObject.SetActive(true);
            index.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Falsity");

            inputArray[currentIndex] = 4;
            currentIndex++;
        }
    }

    public void onSubmit()
    {
        if (currentIndex < 8) return;

        bool isCorrect = true;

        for (int i = 0; i < inputArray.Length; i++)
        {
            if (inputArray[i] != answerArray[i])
            {
                isCorrect = false;
            }
        }

        if (!isCorrect)
        {
            Debug.Log("Incorrect!");
            currentRow++;

            rowsImages.GetChild(currentRow).gameObject.SetActive(true);

            currentIndex = 0;
        }
        else Debug.Log("Correct!");

        if (currentRow > rowsImages.childCount)
        {
            Debug.Log("Game Over!");
        }
    }
    public void onBackspace()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            inputArray[currentIndex] = 0;

            Transform index = rowsImages.GetChild(currentRow).GetChild(currentIndex);

            index.gameObject.SetActive(false);
        }
    }

    public void UpdateSubmit(int index)
    {
        Submit.interactable = index == inputArray.Length;
    }
}
