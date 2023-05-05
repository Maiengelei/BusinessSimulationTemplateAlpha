using Inventory.InventoryManager;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.ButtonFunction.MenuButton
{
    /// <summary>
    /// 背包界面的删除道具功能
    /// </summary>
    public class Delete : MonoBehaviour
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
            _inventoryUIManager.FuncDelete();
        }
    }
}
