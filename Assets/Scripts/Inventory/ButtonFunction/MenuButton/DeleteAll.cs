/*
 * 背包界面的按钮功能
 * 删除所有未装备的道具
 */

using Inventory.InventoryManager;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.ButtonFunction.MenuButton
{
    public class DeleteAll : MonoBehaviour
    {
        private Button _btn;
        private InventoryUIManager _inventoryUIManager;

        private void Start()
        {
            _inventoryUIManager = transform.parent.parent.GetComponent<InventoryUIManager>();

            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _inventoryUIManager.FuncDeleteAll();
        }
    }
}
