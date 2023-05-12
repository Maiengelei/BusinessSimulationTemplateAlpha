using Inventory.InventoryScriptableObject;
using TMPro;
using UnityEngine;

namespace Inventory.InventoryManager
{
    public class UpdateMoneyUI : MonoBehaviour
    {
        [SerializeField] private InventoryObject inventoryObject;
        [SerializeField] private TextMeshProUGUI moneyText;

        private void OnEnable()
        {
            moneyText.text = inventoryObject.GetMoney().ToString();
            inventoryObject.UpdateMoneyUI += UpdateUIText;
        }
        
        private void OnDestroy()
        {
            inventoryObject.UpdateMoneyUI += UpdateUIText;
        }

        private void UpdateUIText()
        {
            moneyText.text = inventoryObject.GetMoney().ToString();
        }
    }
}