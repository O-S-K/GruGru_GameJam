using System;
using System.Collections.Generic;
using UnityEngine;

    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<ItemData> items;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private ItemSlot[] itemSlots;
        [SerializeField] private GameObject itemSlotPrefab;

        public event Action<ItemData> OnItemRightClickedEvent;

        private void Awake()
        {
            itemSlots = new ItemSlot[18];

            for (int i = 0; i < itemSlots.Length; i++)
            {
                var itemClone = Instantiate(itemSlotPrefab);
                itemClone.transform.parent = transform.GetChild(0).transform;
                itemClone.transform.localScale = Vector3.one;

                itemSlots[i] = itemClone.GetComponentInChildren<ItemSlot>();
            }
        }

        private void Start()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
            }
        }

        private void OnEnable()
        {
            if (itemsParent != null)
                itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

            RefreshUI();
        }

        private void RefreshUI()
        {
            int i = 0;
            for (; i < items.Count && i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = items[i];
            }

            for (; i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = null;
            }
        }

        public bool AddItem(ItemData item)
        {
            if (IsFull())
                return false;

            items.Add(item);
            RefreshUI();
            return true;
        }

        public bool RemoveItem(ItemData item)
        {
            if (items.Remove(item))
            {
                RefreshUI();
                return true;
            }
            return false;
        }

        public bool IsFull()
        {
            return items.Count >= itemSlots.Length;
    }
}
