using System.Collections.Generic;
using UnityEngine;


public class CreateManager : OSK.SingletonMono<CreateManager>
{
    /* ------------------------ Inspector Assigned Fields ----------------------- */
    public DamagePopup damagePopupPrefab;

    /* ----------------------------- Public Methods ----------------------------- */

    #region DamagePopup
    public DamagePopup CreateTextDamagePopup(Vector3 position, int damageAmount, DamagePopup.ETypeDamage typeDamage)
    {
        DamagePopup damagePopup = OSK.PoolManager.Instance.SpawnObject(damagePopupPrefab, position, Quaternion.identity);
        damagePopup.gameObject.SetActive(true);
        damagePopup.Setup(damageAmount, typeDamage);
        return damagePopup;
    }

    public DamagePopup CreateTextPopup(Vector3 position, string text, Color color, float size = 25, float timeShow = 0)
    {
        DamagePopup damagePopup = OSK.PoolManager.Instance.SpawnObject(damagePopupPrefab, position, Quaternion.identity);
        damagePopup.gameObject.SetActive(true);
        damagePopup.Setup(text, color, size, timeShow);
        return damagePopup;
    }
    #endregion

    /* -------------------------------------------------------------------------- */
}
