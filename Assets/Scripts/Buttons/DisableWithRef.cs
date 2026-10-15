using UnityEngine;
using UnityEngine.UI;

public class DisableWithRef : MonoBehaviour
{
    [SerializeField] Button button;

    public void Enable()
    {
        button.interactable = true;
    }

    public void Disable()
    {
        button.interactable = false;
    }

    public void DisableThisObject()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
