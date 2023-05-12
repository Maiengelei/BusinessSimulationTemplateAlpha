using Inventory.InventoryScriptableObject;
using UnityEngine;

namespace Player
{
    public class GamerObjectProperties : MonoBehaviour
    {
        // 背包对象
        [SerializeField] private InventoryObject inventoryObject;
        
        // 用于获取背包对象
        public InventoryObject InventoryObject => inventoryObject;
    }
}