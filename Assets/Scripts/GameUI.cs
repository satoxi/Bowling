using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public void Init(GameModel model)
    {
        model.OnRoundScoreChanged += OnRoundScoreChanged;
    }

    private void OnRoundScoreChanged(int score)
    {
        _score.text = score.ToString();
    }

    [SerializeField]
    private Text _score;
}
