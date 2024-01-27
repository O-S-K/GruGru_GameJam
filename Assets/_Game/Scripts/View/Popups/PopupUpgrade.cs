using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUpgrade : OSK.Popup
{
    public override void Show(object[] inData, PopupClosed callback)
    {
        Time.timeScale = 0;
        base.Show(inData, callback);
    }

    public override void Hide()
    {
        Time.timeScale = 1;
        base.Hide(); 
        GameManger.Instance.SpawnWave(2);
    }
}
