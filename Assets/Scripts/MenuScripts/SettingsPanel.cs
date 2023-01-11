using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI textfield;
    public GameObject closeButton;
    public GameObject settingButton;

    public float tweenTime;

    void Start()
    {
        //transform.localScale = Vector2.zero;
    }

    private void Update()
    {
        if(image.fillAmount == 1)
        {
            textfield.enabled = true;
            closeButton.SetActive(true);
        }
        else
        {
            textfield.enabled = false;
            closeButton.SetActive(false);
        }
    }

    public void Open()
    {
        LeanTween.rotateZ(settingButton, 180.0f, 0.2f);
        LeanTween.value(gameObject, 0.01f, 1, tweenTime)
            .setOnUpdate((value) =>
            {
                image.fillAmount = value;
            });
    }

    public void Close()
    {
        LeanTween.rotateZ(settingButton, 0f, 0.2f);
        LeanTween.value(gameObject, 1, 0, tweenTime)
            .setOnUpdate((value) =>
            {
                image.fillAmount = value;
            });
    }

    public void MuteSound()
    {

    }
}
