using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private BaseItem item;

    public void Initialize(BaseItem item)
    {
        this.item = item;
        spriteRenderer.sprite = item.icon;
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        var p = collision.GetComponent<Player>();
        if (p != null)
        {
            if (!p.IsDie)
            {
                item.Action(p);
                Destroy(gameObject);
            }
        }
    }
}
