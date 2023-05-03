﻿using System;
using Equipment;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Character.UI.Inventory
{
    public abstract class UIItemSlot: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public static event Action<ItemSlot> OnPointerEntered;
        public static event Action OnPointerExited;
        
        [field: SerializeField][field: ReadOnly] public int ItemSlotID { get; private set; } = 0;
        [SerializeField] protected Image _itemIcon;
        [SerializeField] protected TextMeshProUGUI _itemAmount;

        private static int _currentMaxInstanceId = 0;
        protected ItemSlot _currentItemSlot;
        public ItemSlot CurrentItemSlot
        {
            get => _currentItemSlot;
            set
            {
                _currentItemSlot = value;
                if (_currentItemSlot != null)
                {
                    _itemIcon.sprite = _currentItemSlot.CurrentItem.ItemIcon;
                    _itemIcon.color = Color.white;
                    var itemAmount = _currentItemSlot.Amount;
                    _itemAmount.text = itemAmount > 1 ? itemAmount.ToString() : "";
                }
                else
                {
                    _itemIcon.sprite = null;
                    _itemIcon.color = Color.black;
                    _itemAmount.text = "";
                }
            }
        }

        private void Awake()
        {
            
        }

        public int SetItemSlotID()
        {
            ItemSlotID = _currentMaxInstanceId;
            _currentMaxInstanceId += 1;
            return ItemSlotID;
            
        }
        protected void OnDisable()
        {
            CurrentItemSlot = null;
            _itemIcon.sprite = null;
            _itemAmount.text = "";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEntered?.Invoke(CurrentItemSlot);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExited?.Invoke();
        }
    }
}