using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
    public float startAlpha = 1f;
    public float endAlpha = 0f;

    public float delay = 0f;
    public float fadeTime = 1f;

    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = startAlpha;
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<CanvasGroup>().DOFade(endAlpha, fadeTime);
    }
}