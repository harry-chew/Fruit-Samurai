using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UpdateScoreText : MonoBehaviour
{
    private TextMeshProUGUI score;

    public Color startingColor;
    public Color updatingColor;

    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();

        GameEvents.FruitEvent += OnFruitEvent;
    }

    private void OnDisable()
    {
        GameEvents.FruitEvent -= OnFruitEvent;
    }

    private void OnFruitEvent(object sender, FruitEventArgs e)
    {
        if (e.EventType == FruitEventType.Cut)
        {
            score.text = GameManager.Instance.Score.ToString();
            ScoreUpdateAnimation();
        }
    }

    private void ScoreUpdateAnimation()
    {
        Sequence anim = DOTween.Sequence(score);
        anim.Append(score.transform.DOScale(1.1f, 0.1f));
        anim.Join(score.DOColor(updatingColor, 0.1f));
        anim.Append(score.transform.DOScale(1f, 0.1f));
        anim.Join(score.DOColor(startingColor, 0.1f));
        anim.Play();
    }
}