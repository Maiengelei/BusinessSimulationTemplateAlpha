using Inventory.InventoryManager;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.ButtonFunction
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
