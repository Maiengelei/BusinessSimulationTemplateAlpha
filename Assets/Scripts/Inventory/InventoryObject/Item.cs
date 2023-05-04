using UnityEngine;

namespace Inventory.InventoryObject
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        /// <summary>
        /// 道具名称
        /// </summary>
        public string itemName;

        /// <summary>
        /// 道具图标
        /// </summary>
        public Sprite itemSprite;

        /// <summary>
        /// 道具加成值(Max)
        /// </summary>
        public int itemValueMax;

        /// <summary>
        /// 道具加成值(Min)
        /// </summary>
        public int itemValueMin;

        /// <summary>
        /// 道具对应模型
        /// </summary>
        public GameObject itemModelPrefab;
    }
}