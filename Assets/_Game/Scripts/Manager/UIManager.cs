using OSK;
using OSK.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class UIManager : OSK.SingletonMono<UIManager>
{
    public PopupManager PopupMG;
    public ScreenManager ScreenMG;

    private void Start()
    {
        PopupMG.Setup();
        ScreenMG.Setup();
    }

    public T ShowScreen<T>(bool back = false, bool immediate = true) where T : UIScreen
    {
        return ScreenMG.Show<T>(back, immediate);
    }

    public T GetScreen<T>() where T : UIScreen
    {
        foreach (var item in ScreenMG.Screens)
        {
            if (item is T)
            {
                return item as T;
            }
        }
        Debug.LogError($"[Screens] No Screens exists in List");
        return null;
    }

    public T ShowCache<T>(object[] inData = null, bool isHideAllPopup = true, Popup.PopupClosed popupClosed = null) where T : Popup
    {
        return PopupMG.ShowCache<T>(inData, isHideAllPopup, popupClosed);
    }

    public T GetPopup<T>() where T : Popup
    {
        foreach (var item in PopupMG.Popups)
        {
            if (item is T)
            {
                return item as T;
            }
        }
        Debug.LogError($"[Popup] No Popup exists in List");
        return null;
    }
}
