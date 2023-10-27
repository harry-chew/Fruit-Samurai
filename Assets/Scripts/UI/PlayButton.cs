using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public Transform blackBox;
    public Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClick);
    }

    private void OnPlayButtonClick()
    {
        StartCoroutine(PlayButtonClickCoroutine());
    }

    private IEnumerator PlayButtonClickCoroutine()
    {
        blackBox.DOMove(new Vector3(-1308f, -916f, 0f), 0.5f);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Game");
    }
}