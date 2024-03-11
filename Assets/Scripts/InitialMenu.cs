using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image rect1;
    [SerializeField] private Image rect2;
    [SerializeField] private GameObject shield;
    [SerializeField] private AudioClip slide;
    [SerializeField] private AudioClip collision;
    [SerializeField] private AudioClip sword;
    private CanvasGroup sh;

    private void Start()
    {
        BackgroundSound.instance.ReproduceIntro();
        StartCoroutine(Bounce());

        LeanTween.moveLocalX(playButton.gameObject, 0f, 1f)
                .setEase(LeanTweenType.easeOutBounce);

        LeanTween.moveLocalX(selectButton.gameObject, 0f, 1f)
                .setEase(LeanTweenType.easeOutBounce);

        LeanTween.moveLocalX(exitButton.gameObject, 0f, 1f)
                .setEase(LeanTweenType.easeOutBounce);

        LeanTween.moveLocalY(title.gameObject, 490f, 1f)
                .setEase(LeanTweenType.easeOutBounce);
    }

    IEnumerator Bounce()
    {
        yield return new WaitForSeconds(0.2f);
        AudioController.instance.Reproduce(collision);
    }

    IEnumerator playTransitionSelectPlayer()
    {
        AudioController.instance.Reproduce(slide);
        LeanTween.moveLocalX(rect1.gameObject, 0f, 0.4f)
                .setEase(LeanTweenType.easeInQuad);
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SelectPlayer()
    {
        AudioController.instance.SoundButton();
        StartCoroutine(playTransitionSelectPlayer());
    }

    public void Exit()
    {
        AudioController.instance.SoundButton();
        Debug.Log("Exit...");
        Application.Quit();
    }

    IEnumerator playTransitionPlay()
    {
        AudioController.instance.Reproduce(sword);
        sh = shield.GetComponent<CanvasGroup>();
        LeanTween.moveLocalX(rect1.gameObject, 800f, 0.8f)
                .setEase(LeanTweenType.easeInQuad).setOnComplete(changeShieldSize);
        LeanTween.moveLocalX(rect2.gameObject, -800f, 0.8f)
                .setEase(LeanTweenType.easeInQuad);

        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    private void changeShieldSize()
    {
        shield.SetActive(true);
        LeanTween.alphaCanvas(sh, 0f, 0.8f)
                 .setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(shield, new Vector3(2.5f, 2.5f, 1f), 0.8f)
                 .setEase(LeanTweenType.easeOutQuad);
    }

    public void Play()
    {
        BackgroundSound.instance.Stop();
        AudioController.instance.SoundButton();
        StartCoroutine(playTransitionPlay());
    }
}
