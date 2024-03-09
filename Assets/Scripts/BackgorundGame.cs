using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBackgroundGame", menuName = "BackgroundGame")]
public class BackgroundGame : ScriptableObject
{
    public GameObject background;
    public float varY;
    public List<float> horizontalSpeeds;
}
