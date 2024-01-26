using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

    public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image image;

        public event Action<ItemData> OnRightClickEvent;

        private ItemData _item;
        public ItemData Item
        {
            get { return _item; }
            set
            {
                _item = value;

                if (_item == null)
                {
                    image.enabled = false;
                }
                else
                {
                    image.sprite = _item.Icon;
                    image.enabled = true;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                if (Item != null && OnRightClickEvent != null)
                    OnRightClickEvent(Item);
            }
        }

        protected virtual void OnValidate()
        {
            if (image == null)
                image = GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ItemTooltip.Instance.gameObject.SetActive(true);
            ItemTooltip.Instance.ShowTooltip(Item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ItemTooltip.Instance.HideTooltip();
        }
}
