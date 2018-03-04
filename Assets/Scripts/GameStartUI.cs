using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
    public void Init(Game game)
    {
        _game = game;    
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);

        _fadeTween = _startText.DOColor(Color.white, 0.7f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);

        _fadeTween.Kill();
        _fadeTween = null;
    }

    private void OnStartButtonClicked()
    {
        _game.StartMatch();
    }

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Text _startText;

    private Tween _fadeTween;
    private Game _game;
}
