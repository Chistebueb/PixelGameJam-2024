using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void SetSensitivity (float value)
    {
        PlayerController.lookSensitivity = value;
    }

    public void GoBack()
    {
        if (mainMenu != null)
            mainMenu.SetActive(true);

        if (settingsMenu != null)
            settingsMenu.SetActive(false);
    }
}
