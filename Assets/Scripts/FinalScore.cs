using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private TextMeshProUGUI menuScoreText;

    void Start()
    {
        menuScoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        menuScoreText.text = scoreText.text;
    }
}
