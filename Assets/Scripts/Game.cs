﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameModel Model { get; private set; }

    private void Awake()
    {
        Model = new GameModel();
        Model.CurrentGameState = GameState.Home;
    }

    private void OnEnable()
    {
        Model.OnRoundGenerated += OnRoundGenerated;
    }

    private void OnDisable()
    {
        Model.OnRoundGenerated -= OnRoundGenerated;
    }
        
	private void Update()
	{
        switch (Model.CurrentGameState)
        {
            case GameState.Home:
                HandleHomeInput();
                break;
            case GameState.Match:
                HandleMatchUpdate(Time.deltaTime);
                HandleMatchInput();
                break;
            case GameState.MatchEnd:
                HandleMatchEndInput();
                break;
        }
	}

    private void HandleHomeInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Model.StartMatch();
        }
    }

	private void HandleMatchInput()
	{
        if (Model.CurrentMatchState != MatchState.Round)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Model.MoveBowling())
            {
                _bowlings[Model.CurrentBowlingIndex].Move();
            }
        }
	}

    private void HandleMatchEndInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Model.StartMatch();
        }
    }

    private void HandleMatchUpdate(float deltaTime)
    {
        if (Model.CurrentMatchState == MatchState.Round)
        {
            Model.UpdateRoundTime(deltaTime);
            _platform.Move();
        }
    }

    private void OnRoundGenerated()
    {
        _platform.Reset(Model.CurrentRoundData);
        for (int i = 0; i < _bowlings.Count; i++)
        {
            _bowlings[i].Reset();
        }

        Model.CurrentMatchState = MatchState.Round;
    }

    [SerializeField]
    private Platform _platform;
	[SerializeField]
	private List<Bowling> _bowlings;
}

public enum GameState
{
    Null,
    Home,
    Match,
    MatchEnd,
}

public enum MatchState
{
    Null,
    Standby,
    Round,
}
