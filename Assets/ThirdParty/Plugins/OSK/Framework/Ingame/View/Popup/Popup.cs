using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OSK
{
    public class Popup : MonoBehaviour
    {

        private bool isShowing;
        public bool IsShowing { get { return isShowing; } }

        private PopupClosed callback;

        public delegate void PopupClosed(bool cancelled, object[] outData);


        public virtual void Initialize()
        {
        }
         
        public virtual void Show(object[] inData, PopupClosed callback)
        {
            gameObject.SetActive(true);
            this.callback = callback;

            if (isShowing)
            {
                return;
            }

            isShowing = true;
        }

        public virtual void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Hide();
            }
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            isShowing = false;
        }

        public virtual void Destroyed(float timeDelay = 0)
        {
            isShowing = false;
            HidePopup();
            Destroy(gameObject, timeDelay);
        }
         
        public virtual void HidePopup()
        {
            UIManager.Instance.PopupMG.OnHidePopup(this);
        }

        public virtual void RefreshUI()
        {

        }
    }
}