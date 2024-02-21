using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] Image screen;

    private Color screenColor, titleColor;

    // Start is called before the first frame update
    void Start()
    {
        screenColor = screen.color;
        titleColor = title.color;
    }

    // Update is called once per frame
    void Update()
    {
        screenColor.a -= 0.01f;
        screen.color = screenColor;

        titleColor.a -= 0.01f;
        title.color = titleColor;

        if (screenColor.a <= 0f) gameObject.SetActive(false);
    }
}
