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

    public void Start()
    {
        SensSlider.GetComponent<Slider>().SetValueWithoutNotify(PlayerController.lookSensitivity);
    }

    public void SetSensitivity (float value)
    {
        PlayerController.lookSensitivity = value;
    }
}
