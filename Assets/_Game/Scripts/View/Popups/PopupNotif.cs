using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupNotif : OSK.Popup
{
    public TextMeshProUGUI textNotif;
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Show(object[] inData, PopupClosed callback)
    {
        base.Show(inData, callback);
    }

    public override void Update()
    {
    }

    public void ShowText(string text, float timeHide)
    {
        textNotif.text = text.ToString();
        Invoke(nameof(Hide), timeHide);   
    }

    public override void Hide()
    {
        base.Hide();
    }
}
