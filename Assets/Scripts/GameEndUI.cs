using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    public void Init(Game game)
    {
        _game = game;
    }

    public void Show()
    {
        _totalScore.text = string.Format("总得分：{0}", 
            _game.Model.TotalScore);
        _perfectCount.text = string.Format("全中次数：{0}", 
            _game.Model.PerfectCount);
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _replayButton.onClick.AddListener(OnReplayButtonClicked);
    }

    private void OnDisable()
    {
        _replayButton.onClick.RemoveListener(OnReplayButtonClicked);
    }

    private void OnReplayButtonClicked()
    {
        _game.StartMatch();
    }

    [SerializeField]
    private Text _totalScore;
    [SerializeField]
    private Text _perfectCount;
    [SerializeField]
    private Button _replayButton;

    private Game _game;
}
