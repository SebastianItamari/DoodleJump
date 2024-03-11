using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] private Image rect1;
    [SerializeField] private Image rect2;
    public GameObject player;
    void Start()
    {
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        player = Instantiate(GameManager.instance.players[indexJugador].player, transform.position, Quaternion.identity);
        player.SetActive(false);
        LeanTween.moveLocalX(rect1.gameObject, -1672f, 0.8f)
                .setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveLocalX(rect2.gameObject, 1672f, 0.8f)
                .setEase(LeanTweenType.easeOutQuad).setOnComplete(ActivePlayer); ;
    }

    private void ActivePlayer()
    {
        player.SetActive(true);
    }
}
