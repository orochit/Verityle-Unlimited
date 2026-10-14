using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Dictionary<int, string> verityList = new Dictionary<int, string>
    {
        { 1, "Verity" },
        { 2, "Cruelty" },
        { 3, "Lovity" },
        { 4, "Falsity" }
    };

    public static int[] answerArray = new int[8];

    private void Start()
    {
        for (int i = 0; i < answerArray.Length; i++)
        {
            answerArray[i] = Random.Range(1, 5);
        }

        Debug.Log("Answer Array: " + string.Join(", ", answerArray));
    }
}
