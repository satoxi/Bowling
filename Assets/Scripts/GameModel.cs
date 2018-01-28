using System.Timers;
using System;
using UnityEngine;
using System.Collections.Generic;

public class GameModel
{
    public const float RoundDuration = 5f;
    public const int TotalRound = 5;

    public Action OnRoundEnd = delegate { };
    public Action OnRoundGenerated = delegate { };

    public int CurrentRound { get; private set; }
    public float CurrentRoundElapsedTime { get; private set; }

    public GameState CurrentGameState;
    public MatchState CurrentMatchState
    {
        get { return _currentMatchState; }
        set
        {
            if (_currentMatchState != value)
            {
                _currentMatchState = value;
            }
        }
    }
    public int[,] CurrentRoundData;

    public GameModel()
    {
        CurrentGameState = GameState.Null;
        ResetRound();
    }

    public void StartMatch()
    {
        CurrentGameState = GameState.Match;
        GenerateNextRound();
    }

    public void EndMatch()
    {
        CurrentGameState = GameState.MatchEnd;
        ResetRound();
    }

    public void UpdateRoundTime(float deltaTime)
    {
        CurrentRoundElapsedTime += deltaTime;
        if (CurrentRoundElapsedTime >= GameModel.RoundDuration)
        {
            GenerateNextRound();
        }
    }

    private void GenerateNextRound()
    {
        CurrentRound++;
        if (CurrentRound > TotalRound)
        {
            EndMatch();
            OnRoundEnd();
        }
        else
        {
            CurrentRoundElapsedTime = 0;
            CurrentMatchState = MatchState.Standby;
            OnRoundGenerated();
        }
    }

    private void ResetRound()
    {
        CurrentRound = -1;
        CurrentRoundElapsedTime = 0;

        CurrentMatchState = MatchState.Null;
    }

    private MatchState _currentMatchState;
    private List<int[,]> _roundData;
}