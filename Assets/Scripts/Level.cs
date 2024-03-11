using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level")]
public class Level : ScriptableObject
{
    public GameObject background;
    public float varY;
    public List<float> horizontalSpeeds;
    public int recurrenceCoin;
}
