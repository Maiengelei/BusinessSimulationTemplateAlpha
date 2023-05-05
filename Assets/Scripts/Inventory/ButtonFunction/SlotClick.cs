using Inventory.InventoryManager;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.ButtonFunction
{
    public class SlotClick : MonoBehaviour
    {
        /// <summary>
        /// 格子点击按钮
        /// </summary>
        private Button _btn;

        /// <summary>
        /// 格子中的 Item 道具子物体
        /// </summary>
        private Transform _item;

        /// <summary>
        /// 背包 Bag 上的 Inventory UI Manager 脚本
        /// </summary>
        private InventoryUIManager _bagObject;

        private void Start()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(OnClick);

            _item = transform.Find("Item");

            _bagObject = FindObjectOfType<Canvas>().transform.Find("Bag").GetComponent<InventoryUIManager>();
        }

        private void OnClick()
        {
            _bagObject.CheckSlot(GetComponent<SlotData>().gridSlotConst);
        
#if UNITY_EDITOR
            Debug.Log(_item.gameObject.activeInHierarchy
                ? $"格子中有东西，格子的ID是{GetComponent<SlotData>().gridSlotConst}"
                : "格子里没有东西");
#endif
        }
    }
}