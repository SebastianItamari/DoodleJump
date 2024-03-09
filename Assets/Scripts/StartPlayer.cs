using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    void Start()
    {
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Instantiate(GameManager.instance.players[indexJugador].player, transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
