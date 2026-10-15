using UnityEngine;
using UnityEngine.UI;

public class DisableWhenAnimating : MonoBehaviour
{
    public void Enable()
    {
        Button button = GetComponent<Button>();

        button.interactable = true;
    }

    public void Disable()
    {
        Button button = GetComponent<Button>();
        button.interactable = false;
    }
}
