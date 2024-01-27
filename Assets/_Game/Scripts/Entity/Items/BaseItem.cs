using UnityEngine;


public abstract class BaseItem : MonoBehaviour
{
    public EItem item;
    public string itemName = "";
    public Sprite icon;

    public abstract void OnEnable();
    public abstract void Action(Transform player);
    public abstract void DestroyItem();

    protected void OnValidate()
    {
        gameObject.name = item.ToString();
        itemName = item.ToString();
    }
}
