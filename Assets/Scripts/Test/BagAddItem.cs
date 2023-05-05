using Inventory.InventoryObject;
using Player;
using UnityEngine;

namespace Test
{
    public class BagAddItem : MonoBehaviour
    {
        private Inventory.InventoryObject.Inventory _inventory;
        public Item item;
        private void Awake()
        {
            // 通过 Tag 获取玩家对象
            GameObject player = GameObject.FindWithTag("Player");
            // 获取到玩家的背包
            _inventory = player.GetComponent<GamerObjectProperties>().Inventory;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                _inventory.AddItemToList(item);
            }
        }
    }
}