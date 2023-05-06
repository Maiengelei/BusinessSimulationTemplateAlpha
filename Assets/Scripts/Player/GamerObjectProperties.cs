using Inventory.InventoryObject;
using UnityEngine;

namespace Player
{
    public class GamerObjectProperties : MonoBehaviour
    {
        [SerializeField] private InventoryObject inventoryObject;
        
        public InventoryObject InventoryObject => inventoryObject;
    }
}