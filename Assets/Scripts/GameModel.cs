using System.Timers;
using System;
using UnityEngine;
using System.Collections.Generic;

public class GameModel
{
    public const int BowlingCount = 5;
    public const float RoundDuration = 5f;
    public const int TotalRound = 5;

    public Action OnRoundEnd = delegate { };
    public Action OnRoundGenerated = delegate { };

    public int CurrentBowlingIndex { get; private set; }

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
        _roundData = new List<int[,]>();
        _roundData.Add(new int[,]{
            {-1, -1, 0, -1, -1},
            {-1, -1, -1, -1, 0}, 
            {0, -1, -1, -1, -1}
        });
        _roundData.Add(new int[,]{
            {-1, -1, 0, -1, -1},
            {-1, -1, -1, -1, 0}, 
            {-1, 1, 0, 1, -1}
        });
        _roundData.Add(new int[,]{
            {-1, 1, 1, -1, -1},
            {1, 1, 0, 0, 0}, 
            {-1, 1, 1, 0, -1}
        });
        _roundData.Add(new int[,]{
            {-1, 1, 1, 1, -1},
            {1, 1, 1, 1, 1}, 
            {-1, 1, 1, 1, -1}
        });
        _roundData.Add(new int[,]{
            {-1, 1, 1, 2, -1},
            {2, 1, 1, 1, 1}, 
            {-1, 2, 1, 1, -1}
        });
        ResetMatch();
    }

    public bool MoveBowling()
    {
        if (CurrentBowlingIndex < BowlingCount - 1)
        {
            CurrentBowlingIndex++;
            return true;
        }
        return false;
    }

    public void StartMatch()
    {
        CurrentGameState = GameState.Match;
        GenerateNextRound();
    }

    public void EndMatch()
    {
        CurrentGameState = GameState.MatchEnd;
        ResetMatch();
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
        if (CurrentRound >= TotalRound)
        {
            EndMatch();
            OnRoundEnd();
        }
        else
        {
            CurrentRoundElapsedTime = 0;
            CurrentBowlingIndex = -1;
            CurrentMatchState = MatchState.Standby;
            CurrentRoundData = _roundData[CurrentRound];
            OnRoundGenerated();
        }
    }

    private void ResetMatch()
    {
        CurrentRound = -1;
        CurrentMatchState = MatchState.Null;
    }

    private MatchState _currentMatchState;
    private List<int[,]> _roundData;
}