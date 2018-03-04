﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameModel Model { get; private set; }

    public void StartMatch()
    {
        Model.StartMatch();
    }

    private void Awake()
    {
        _platform.HandlePinDown += HandlePinDown;

        Model = new GameModel();
        _gameUI.Init(this);
        _gameStartUI.Init(this);

        Model.CurrentGameState = GameState.Home;
        _gameStartUI.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        Model.OnMatchStart += OnMatchStart;
        Model.OnMatchEnd += OnMatchEnd;
        Model.OnRoundGenerated += OnRoundGenerated;
    }

    private void OnDisable()
    {
        Model.OnMatchStart -= OnMatchStart;
        Model.OnMatchEnd -= OnMatchEnd;
        Model.OnRoundGenerated -= OnRoundGenerated;
    }
        
    private void OnMatchStart()
    {
        _gameStartUI.gameObject.SetActive(false);

        _bowlingGroup.SetActive(true);

        _gameUI.Reset();
        _gameUI.gameObject.SetActive(true);

        AudioManager.Instance.PlayBGM();
    }

    private void OnMatchEnd()
    {
        _bowlingGroup.SetActive(false);

        _gameUI.gameObject.SetActive(false);
        _gameEndUI.Refresh(Model);

        AudioManager.Instance.StopBGM();
    }

	private void Update()
	{
        switch (Model.CurrentGameState)
        {
            case GameState.Match:
                HandleMatchUpdate(Time.deltaTime);
                HandleMatchInput();
                if (Model.CurrentMatchState == MatchState.Round)
                {
                    _platform.CheckPins();
                }
                break;
            case GameState.MatchEnd:
                HandleMatchEndInput();
                break;
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
            _gameEndUI.gameObject.SetActive(false);
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

    private void HandlePinDown(PinData data)
    {
        Model.HandlePinDown(data);
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
    private GameStartUI _gameStartUI;
    [SerializeField]
    private GameUI _gameUI;
    [SerializeField]
    private GameEndUI _gameEndUI;
    [SerializeField]
    private Platform _platform;
    [SerializeField]
    private GameObject _bowlingGroup;
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

public enum CustomTags
{
    Pin = 0,
    Bowling = 1,
    Platform = 2,
}
