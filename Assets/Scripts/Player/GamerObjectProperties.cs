using UnityEngine;

namespace Player
{
    public class GamerObjectProperties : MonoBehaviour
    {
        [SerializeField] private Inventory.InventoryObject.Inventory inventory;
        
        public Inventory.InventoryObject.Inventory Inventory => inventory;
    }
}