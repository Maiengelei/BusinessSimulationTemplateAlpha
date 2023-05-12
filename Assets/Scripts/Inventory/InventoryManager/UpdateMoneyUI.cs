using Inventory.InventoryScriptableObject;
using TMPro;
using UnityEngine;

namespace Inventory.InventoryManager
{
    public class UpdateMoneyUI : MonoBehaviour
    {
        // 背包对象
        [SerializeField] private InventoryObject inventoryObject;
        
        // 文本
        [SerializeField] private TextMeshProUGUI moneyText;

        private void OnEnable()
        {
            // 获取当前的钱
            moneyText.text = inventoryObject.GetMoney().ToString();
            
            // 增加事件
            inventoryObject.UpdateMoneyUI += UpdateUIText;
        }
        
        private void OnDestroy()
        {
            // 增加事件
            inventoryObject.UpdateMoneyUI += UpdateUIText;
        }

        private void UpdateUIText()
        {
            // 更新 UI 中的钱
            moneyText.text = inventoryObject.GetMoney().ToString();
        }
    }
}