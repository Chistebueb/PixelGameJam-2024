using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SensSlider;
    public GameObject Quality;
    public GameObject Jump;
    public GameObject Sprint;
    public GameObject Crouch;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    public void Start()
    {
        SensSlider.GetComponent<Slider>().SetValueWithoutNotify(PlayerController.lookSensitivity); 
        Quality.GetComponent<TMP_Dropdown>().SetValueWithoutNotify(QualitySettings.GetQualityLevel());
        Jump.GetComponent<TMP_InputField>().SetTextWithoutNotify(PlayerController.jumpKey.ToString());
        Sprint.GetComponent<TMP_InputField>().SetTextWithoutNotify(PlayerController.sprintKey.ToString());
        Crouch.GetComponent<TMP_InputField>().SetTextWithoutNotify(PlayerController.crouchKey.ToString());
    }

    public void SetSensitivity (float value)
    {
        PlayerController.lookSensitivity = value;
    }

    public void GoBack ()
    {
        if (mainMenu != null)
            mainMenu.SetActive(true);

        if (settingsMenu != null)
            settingsMenu.SetActive(false);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetJump(string key)
    {
        PlayerController.jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key, true); ;
    }
    public void SetSprint(string key)
    {
        PlayerController.sprintKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key, true); ;
    }
    public void SetCrouch(string key)
    {
        PlayerController.crouchKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), key, true); ;
    }
}
