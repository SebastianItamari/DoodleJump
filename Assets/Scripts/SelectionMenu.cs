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
    [SerializeField] private Image rect1;
    [SerializeField] private Image rect2;
    [SerializeField] private GameObject shield;
    private CanvasGroup sh;
    private GameManager gameManager;

    private void Start()
    {
        LeanTween.moveLocalX(rect1.gameObject, -1672f, 0.4f)
               .setEase(LeanTweenType.easeOutQuad);

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
        StartCoroutine(playTransitionPlay());
    }

    IEnumerator playTransitionPlay()
    {
        sh = shield.GetComponent<CanvasGroup>();
        LeanTween.moveLocalX(rect1.gameObject, -800f, 0.8f)
                .setEase(LeanTweenType.easeInQuad).setOnComplete(changeShieldSize);
        LeanTween.moveLocalX(rect2.gameObject, 800f, 0.8f)
                .setEase(LeanTweenType.easeInQuad);
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void changeShieldSize()
    {
        shield.SetActive(true);
        LeanTween.alphaCanvas(sh, 0f, 0.8f)
                 .setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(shield, new Vector3(3f, 3f, 1f), 0.8f)
                 .setEase(LeanTweenType.easeOutQuad);
    }
}
