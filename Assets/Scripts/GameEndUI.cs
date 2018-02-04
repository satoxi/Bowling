using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    public void Refresh(GameModel model)
    {
        _totalScore.text = string.Format("总得分：{0}", model.TotalScore);
        _perfectCount.text = string.Format("全中次数：{0}", model.PerfectCount);
        gameObject.SetActive(true);
    }

    [SerializeField]
    private Text _totalScore;
    [SerializeField]
    private Text _perfectCount;
}
