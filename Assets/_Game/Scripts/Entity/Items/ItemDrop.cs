using DG.Tweening;
using TMPro;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private BaseItem item;

    [SerializeField] GameObject textNotif;

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
                var text = Instantiate(textNotif);
                text.transform.parent = p.transform;
                text.transform.localPosition = new Vector3(0, 1.25f, 1);
                text.GetComponentInChildren<TextMeshProUGUI>().text = $"+1 {item.itemName}";
                //text.transform.GetChild(0).transform.DOLocalMoveY(-1, 0.5f);

                Destroy(text, 1);
                item.Action(p);
                Destroy(gameObject);
            }
        }
    }
}
