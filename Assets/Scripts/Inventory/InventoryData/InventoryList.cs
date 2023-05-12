/*
 * 自定义 List 结构，用于满足实际需求
 */

namespace Inventory.InventoryData
{
    // 包含两个参数的 List
    [System.Serializable]
    public class InventoryList<T1, T2>
    {
        public T1 Item { get; set; }
        public T2 Attributes { get; set; }

        public InventoryList(T1 item, T2 attributes)
        {
            this.Item = item;
            this.Attributes = attributes;
        }
    }

    // 道具属性结构体
    [System.Serializable]
    public struct ItemAttributes
    {
        public int ItemValue { get; set; }
        public bool IsEquipped { get; set; }

        // 结构体构造函数
        public ItemAttributes(int itemValue, bool isEquipped)
        {
            this.ItemValue = itemValue;
            this.IsEquipped = isEquipped;
        }
    }
}