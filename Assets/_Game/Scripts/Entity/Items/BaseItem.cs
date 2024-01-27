using UnityEngine;


public abstract class BaseItem : MonoBehaviour
{
    public EItem item;
    public string itemName = "";
    public string des = "";
    public Sprite icon;

    public abstract void OnEnable();
    public abstract void Action(Player player);
    public abstract void DestroyItem();

    protected void OnValidate()
    {
        gameObject.name = item.ToString();
        itemName = item.ToString();
    }
}
