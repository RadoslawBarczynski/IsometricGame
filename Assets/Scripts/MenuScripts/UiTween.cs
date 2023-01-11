using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiTween : MonoBehaviour
{
    public Transform box;
    public GameObject box1;
    private void OnEnable()
    {
        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseInOutExpo().delay = 0.1f;
    }

    public void CloseDeckPanel()
    {
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInOutExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
