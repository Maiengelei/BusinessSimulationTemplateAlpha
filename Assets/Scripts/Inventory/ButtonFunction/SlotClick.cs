/*
 * 背包格子属性
 * 用于管理背包格子的属性传递和点击事件
 */

using Inventory.InventoryManager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inventory.ButtonFunction
{
    public class SlotClick : MonoBehaviour
    {
        // 格子点击按钮
        private Button _btn;

        // 格子中的 Item 道具子物体
        private Transform _item;

        // 背包 Bag 上的 Inventory UI Manager 脚本
        public InventoryUIManager bagObject;

        private void Start()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(OnClick);

            _item = transform.Find("Item");
        }

        private void OnClick()
        {
            bagObject.CheckSlot(GetComponent<SlotData>().gridSlotConst);
        }
    }
}