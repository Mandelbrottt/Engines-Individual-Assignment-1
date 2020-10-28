using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HighScoreSO", menuName ="Game/HighScore")]
[System.Serializable]
public class HighScoreSo : ScriptableObject
{
    public int score;
}
