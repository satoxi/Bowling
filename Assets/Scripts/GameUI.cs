using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public void Init(Game game)
    {
        _model = game.Model;
    }

    public void Reset()
    {
        for (int i = 0; i < _scoreHuds.Count; i++)
        {
            _scoreHuds[i].Reset();
        }
    }

    private void OnEnable()
    {
        _model.OnRoundScoreChanged += OnRoundScoreChanged;
        _model.OnRoundPerfect += OnRoundPerfect;
    }

    private void OnDisable()
    {
        _model.OnRoundScoreChanged -= OnRoundScoreChanged;
        _model.OnRoundPerfect -= OnRoundPerfect;
    }

    private void OnRoundScoreChanged(int score)
    {
        _scoreHuds[_model.CurrentRound].RefreshScore(score);
    }

    private void OnRoundPerfect()
    {
        _scoreHuds[_model.CurrentRound].SetPerfect();    
    }

    [SerializeField]
    private List<ScoreHud> _scoreHuds;

    private GameModel _model;
}