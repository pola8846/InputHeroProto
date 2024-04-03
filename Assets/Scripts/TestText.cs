using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestText : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        string temp_S = "";
        temp_S += "PlayerHP: ";
        temp_S += GameManager.Player2D.Health.ToString();
        text.text = temp_S;
    }
}
