using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [Header("Rows")]
    [SerializeField] public Transform rowsImages;
    [SerializeField] public Transform rowsTable;

    [Header("Buttons")]
    [SerializeField] private Button Backspace;
    [SerializeField] private Button Submit;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject MuteAudio;
    [SerializeField] private GameObject MoreInfo;
    [SerializeField] private GameObject ExitMoreInfo;

    [Header("Panels")]
    [SerializeField] private GameObject MoreInfoPanel;
    [SerializeField] private GameObject GameOverPanel;

    [Header("Audio")]
    [SerializeField] private AudioClip sfxVerity;
    [SerializeField] private AudioClip sfxCruelty;
    [SerializeField] private AudioClip sfxLovity;
    [SerializeField] private AudioClip sfxFalsity;
    [SerializeField] private AudioClip sfxKidsCheering;

    private AudioSource audioSource;

    public static Dictionary<int, string> verityList = new Dictionary<int, string>
    {
        { 1, "Verity" },
        { 2, "Cruelty" },
        { 3, "Lovity" },
        { 4, "Falsity" }
    };

    public static bool muteAudio = false;

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


    private void ResetGame()
    {
        answerArray = new int[8];
        inputArray = new int[8];
        currentIndex = 0;
        currentRow = 0;

        if (muteAudio) MuteAudio.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/volume");
        if (!muteAudio) MuteAudio.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/speaker-filled-audio-tool");
    }

    private void Start()
    {
        ResetGame();

        audioSource = GetComponent<AudioSource>();

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
            if (!muteAudio) audioSource.PlayOneShot(sfxVerity);
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
            if (!muteAudio) audioSource.PlayOneShot(sfxCruelty);
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
            if (!muteAudio) audioSource.PlayOneShot(sfxLovity);
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
            if (!muteAudio) audioSource.PlayOneShot(sfxFalsity);

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

        CheckAnswer();
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

    public void onMute()
    {
        muteAudio = !muteAudio;

        if (muteAudio) MuteAudio.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/volume");
        if (!muteAudio) MuteAudio.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/speaker-filled-audio-tool");
    }

    public void onMoreInfo()
    {
        MoreInfoPanel.SetActive(true);

        Animator[] animator = MoreInfoPanel.GetComponentsInChildren<Animator>();

        bool isOpen = animator[0].GetBool("isOpen");
        animator[0].SetBool("isOpen", !isOpen);
    }

    public void onMoreInfoExit()
    {
        Animator[] animator = MoreInfoPanel.GetComponentsInChildren<Animator>();

        bool isOpen = animator[0].GetBool("isOpen");
        animator[0].SetBool("isOpen", !isOpen);
    }

    public void onSettings()
    {
        Animator animator = Settings.GetComponent<Animator>();
        bool isOpen = animator.GetBool("isOpen");
        animator.SetBool("isOpen", !isOpen);

        ToggleMute();
        ToggleInfo();
    }

    public void onNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleInfo()
    {
        Animator animator = MoreInfo.GetComponent<Animator>();

        bool isOpen = animator.GetBool("isOpen");
        animator.SetBool("isOpen", !isOpen);
    }

    public void ToggleMute()
    {
        Animator animator = MuteAudio.GetComponent<Animator>();
        bool isOpen = animator.GetBool("isOpen");
        animator.SetBool("isOpen", !isOpen);
    }

    public void UpdateSubmit(int index)
    {
        Submit.interactable = index == inputArray.Length;
    }

    private void CheckAnswer()
    {
        bool isCorrect = true;

        bool[] rightOnIndex = new bool[8];
        int[] answerCounter = new int[5];

        for (int i = 0; i < inputArray.Length; i++)
        {
            if (inputArray[i] != answerArray[i])
            {
                if (isCorrect) isCorrect = false;

                answerCounter[answerArray[i]]++;
                rightOnIndex[i] = false;
            }
            else
            {
                rightOnIndex[i] = true;
            }
        }

        ColorOnCorrect(rightOnIndex);

        ColorIfInArray(answerCounter, rightOnIndex);

        if (!isCorrect)
        {
            Debug.Log("Incorrect!");

            if (currentRow >= 5)
            {
                Debug.Log("Game Over!");
                Backspace.interactable = false;
                Submit.interactable = false;
                GameOver();
            }
            else
            {
                currentRow++;
                rowsImages.GetChild(currentRow).gameObject.SetActive(true);
                currentIndex = 0;
            }
        }
        else
        {
            audioSource.PlayOneShot(sfxKidsCheering);
            Backspace.interactable = false;
            Submit.interactable = false;
            Debug.Log("Correct!");
        }

    }

    private void ColorOnCorrect(bool[] rightOnIndex)
    {
        for (int i = 0; i < rightOnIndex.Length; i++)
        {
            Transform index = rowsTable.GetChild(currentRow).GetChild(i);

            if (rightOnIndex[i] == true)
            {
                index.GetComponent<Image>().color = Color.green;
            }
        }
    }

    private void ColorIfInArray(int[] answerCounter, bool[] rightOnIndex)
    {
        for (int i = 0; i < rightOnIndex.Length; i++)
        {
            Transform index = rowsTable.GetChild(currentRow).GetChild(i);

            if (rightOnIndex[i] == true) continue;

            int whichVerity = inputArray[i];

            if (answerCounter[whichVerity] > 0)
            {
                answerCounter[whichVerity]--;
                index.GetComponent<Image>().color = Color.yellow;
            }
            else index.GetComponent<Image>().color = Color.grey;
        }
    }

    private void GameOver()
    {
        GameOverPanel.SetActive(true);
    }
}
