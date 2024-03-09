using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionMenu : MonoBehaviour
{
    private int index;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI name;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        index = PlayerPrefs.GetInt("JugadorIndex");

        if (index > gameManager.players.Count - 1) //Por si borramos personajes de la lista
        {
            index = 0;
        }

        ChangeScreen();
    }

    private void ChangeScreen()
    {
        PlayerPrefs.SetInt("JugadorIndex", index);
        image.sprite = gameManager.players[index].image;
        name.text = gameManager.players[index].name;
    }

    public void NextPlayer()
    {
        if(index == gameManager.players.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        ChangeScreen();
    }

    public void PreviousPlayer()
    {
        if (index == 0)
        {
            index = gameManager.players.Count - 1;
        }
        else
        {
            index--;
        }

        ChangeScreen();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
