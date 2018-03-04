using System.Timers;
using System;
using UnityEngine;
using System.Collections.Generic;

public class GameModel
{
    public const int BowlingCount = 5;
    public const float RoundDuration = 5.5f;
    public const int TotalRound = 5;

    public Action OnMatchStart = delegate { };
    public Action OnMatchEnd = delegate { };

    public Action OnRoundEnd = delegate { };
    public Action OnRoundGenerated = delegate { };
    public Action<int> OnRoundScoreChanged = delegate { };
    public Action OnRoundPerfect = delegate { };

    public int CurrentBowlingIndex { get; private set; }

    public int CurrentRound { get; private set; }
    public float CurrentRoundElapsedTime { get; private set; }
    public List<RoundResultData> RoundResults { get; private set; }
    public int TotalScore { get; private set; }
    public int PerfectCount { get; private set; }

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
        RoundResults = new List<RoundResultData>();
        ResetMatch();
    }

    public void HandlePinDown(PinData data)
    {
        RoundResults[CurrentRound].TotalScore += data.Score;
        RoundResults[CurrentRound].DownPinCount++;
        if (RoundResults[CurrentRound].DownPinCount == _currentRoundPinCount)
        {
            RoundResults[CurrentRound].IsPerfect = true;
            RoundResults[CurrentRound].TotalScore += 20;
            if (CurrentRoundElapsedTime < RoundDuration)
            {
                RoundResults[CurrentRound].TotalScore += 
                    Mathf.FloorToInt(RoundDuration - CurrentRoundElapsedTime) * 5;
            }
        }

        OnRoundScoreChanged(RoundResults[CurrentRound].TotalScore);
        if (RoundResults[CurrentRound].IsPerfect)
        {
            OnRoundPerfect();
        }
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
        OnMatchStart();
        GenerateNextRound();
    }

    public void EndMatch()
    {
        CurrentGameState = GameState.MatchEnd;
        OnMatchEnd();
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

    private void ComputeTotalResult()
    {
        TotalScore = 0;
        PerfectCount = 0;
        for (int i = 0; i < RoundResults.Count; i++)
        {
            TotalScore += RoundResults[i].TotalScore;
            if (RoundResults[i].IsPerfect)
            {
                PerfectCount++;
            }
        }
    }

    private void GenerateNextRound()
    {
        CurrentRound++;
        if (CurrentRound >= TotalRound)
        {
            ComputeTotalResult();
            EndMatch();
            OnRoundEnd();
        }
        else
        {
            CurrentRoundElapsedTime = 0;
            CurrentBowlingIndex = -1;
            CurrentMatchState = MatchState.Standby;
            CurrentRoundData = _roundData[CurrentRound];
            _currentRoundPinCount = 0;
            for (int i = 0; i < CurrentRoundData.GetLength(0); i++)
            {
                for (int k = 0; k < CurrentRoundData.GetLength(1); k++)
                {
                    if (CurrentRoundData[i,k] != -1)
                    {
                        _currentRoundPinCount++;
                    }
                }
            }
            OnRoundGenerated();
        }
    }

    private void ResetMatch()
    {
        CurrentRound = -1;

        RoundResults.Clear();
        for (int i = 0; i < TotalRound; i++)
        {
            RoundResults.Add(new RoundResultData());
        }

        CurrentMatchState = MatchState.Null;
    }

    private MatchState _currentMatchState;
    private List<int[,]> _roundData;
    private List<RoundResultData> _roundResultList;
    private int _currentRoundPinCount;
}

public class RoundResultData
{
    public int TotalScore;
    public int DownPinCount;
    public bool IsPerfect;
}