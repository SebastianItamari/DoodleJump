using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private TextMeshProUGUI menuScoreText;
    // Start is called before the first frame update
    void Start()
    {
        menuScoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        menuScoreText.text = scoreText.text;
    }
}
