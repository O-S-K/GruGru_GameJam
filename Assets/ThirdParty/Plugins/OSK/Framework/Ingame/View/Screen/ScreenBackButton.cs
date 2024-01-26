using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OSK
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenBackButton : MonoBehaviour
    {
        public float fadeDuration = 0.5f;
        private Button Button { get { return gameObject.GetComponent<Button>(); } }
        private CanvasGroup CanvasGroup { get { return gameObject.GetComponent<CanvasGroup>(); } }

        private void Start()
        {
            Button.onClick.AddListener(OnButtonClicked);
            CanvasGroup.alpha = 0f;
            ScreenManager.Instance.OnSwitchingScreens += OnSwitchingScreens;
        }

        private void OnButtonClicked()
        {
            ScreenManager.Instance.Back();
        }

        private void OnSwitchingScreens(UIScreen fromScreen, UIScreen toScreen)
        {
            PlayAnimation(UIAnimation.Alpha(gameObject, 0f, 1f, fadeDuration));
        }

        private void PlayAnimation(UIAnimation anim)
        {
            anim.style = UIAnimation.Style.EaseOut;
            anim.startOnFirstFrame = true;
            anim.Play();
        }
    }
}
