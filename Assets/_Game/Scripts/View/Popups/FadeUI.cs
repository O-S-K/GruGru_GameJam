using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class FadeUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public void FadeIn(float time, float delay, Action cb)
    {
        // Fade in the CanvasGroup
        canvasGroup.DOFade(1f, time).SetDelay(delay).OnComplete(() =>
        {
            cb.Invoke();
        });
    }

    public void FadeOut(float time, float delay, Action cb)
    {
        // Fade out the CanvasGroup
        canvasGroup.DOFade(1, 0);
        canvasGroup.DOFade(0f, time).SetDelay(delay).OnComplete(() =>
        {
            cb.Invoke();
        });
    }
}
