using UnityEngine;
using UnityEngine.UI;

public class ScoreHud : MonoBehaviour
{
    public void Reset()
    {
        _score.text = string.Empty;
        _score.gameObject.SetActive(false);
        _perfect.gameObject.SetActive(false);
    }

    public void RefreshScore(int score)
    {
        _score.gameObject.SetActive(true);
        _score.text = score.ToString();
    }

    public void SetPerfect()
    {
        _perfect.SetActive(true);
    }

    [SerializeField]
    private Text _score;
    [SerializeField]
    private GameObject _perfect;
}
