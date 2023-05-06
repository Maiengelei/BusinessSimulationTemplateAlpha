using Inventory.InventoryScriptableObject;
using Player;
using UnityEngine;

namespace Test
{
    public class BagAddItem : MonoBehaviour
    {
        private InventoryObject _inventoryObject;
        public ItemObject itemObject;
        private void Awake()
        {
            // 通过 Tag 获取玩家对象
            GameObject player = GameObject.FindWithTag("Player");
            // 获取到玩家的背包
            _inventoryObject = player.GetComponent<GamerObjectProperties>().InventoryObject;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                _inventoryObject.AddItemToList(itemObject);
            }
        }
    }
}