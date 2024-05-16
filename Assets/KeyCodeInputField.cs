using UnityEngine;
using TMPro; 
using UnityEngine.EventSystems; 

public class KeyCodeInputField : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public TMP_InputField inputField; 
    private KeyCode lastKeyCode;

    void Start()
    {
        if (inputField == null)
        {
            Debug.LogError("InputField is not assigned!");
            return;
        }

        inputField.readOnly = true;
    }

    void Update()
    {
        if (inputField.isFocused)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    lastKeyCode = keyCode;
                    inputField.text = keyCode.ToString();
                    inputField.DeactivateInputField(); 
                    break;
                }
            }
        }
    }

    public KeyCode GetLastKeyCode()
    {
        return lastKeyCode;
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("InputField selected");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("InputField deselected");
    }
}
