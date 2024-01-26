using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OSK
{
    public class PopupManager : MonoBehaviour
    {
        public List<Popup> Popups = null;
        public Transform canvas;

        public void Setup()
        {
            for (int i = 0; i < Popups.Count; i++)
            {
                Popups[i].Initialize();
            }
        }

        public T ShowCache<T>(object[] inData = null, bool isHideAllPopup = true, Popup.PopupClosed popupClosed = null) where T : Popup
        {
            Popup popup = GetPopup<T>();

            if (isHideAllPopup)
            {
            }

            if (popup != null)
            {
                popup.Show(inData, popupClosed);
            }
            else
            {
                Debug.LogErrorFormat($"[PopupController] Popup does not exist");
            }
            return (T)popup;
        }

        public T ShowRes<T>(string pathResourceNamePopup = "", object[] inData = null, bool isHideAllPopup = true, Popup.PopupClosed popupClosed = null) where T : Popup
        {
            if (isHideAllPopup)
            {
            }

            T popup = (T)Instantiate(Resources.Load<T>(pathResourceNamePopup), canvas);
            popup.name = pathResourceNamePopup;
            popup.Show(inData, popupClosed);
            Popups.Add(popup);
            return popup;
        }

        public void OnHidePopup(Popup popup)
        {
            for (int i = Popups.Count - 1; i >= 0; i--)
            {
                if (popup == Popups[i])
                {
                    Popups.RemoveAt(i);
                    break;
                }
            }
        }

        public void RefreshUI()
        {
            if(Popups != null)
            {
                foreach (var item in Popups)
                {
                    item.RefreshUI();
                }
            }
        }

        public T GetPopup<T>() where T : Popup
        {
            foreach (var item in Popups)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            return null;
        }
    }
}