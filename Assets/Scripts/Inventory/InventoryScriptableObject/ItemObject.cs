using UnityEngine;

namespace Inventory.InventoryScriptableObject
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class ItemObject : ScriptableObject
    {
        // 道具名称
        public string itemName;

        // 道具图标
        public Sprite itemSprite;

        // 道具加成值(Max)
        public int itemValueMax;

        // 道具加成值(Min)
        public int itemValueMin;

        // 道具对应模型
        public GameObject itemModelPrefab;
    }
}