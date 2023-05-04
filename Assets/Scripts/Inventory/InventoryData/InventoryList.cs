/*
 * 自定义 List 结构，用于满足实际需求
 */

namespace Inventory.InventoryData
{
    /// <summary>
    /// 包含两个参数的 List
    /// </summary>
    /// <typeparam name="T1">item</typeparam>
    /// <typeparam name="T2">Attributes</typeparam>
    [System.Serializable]
    public class InventoryList<T1, T2>
    {
        public T1 Item { get; set; }
        public T2 Attributes { get; set; }

        public InventoryList(T1 item, T2 Attributes)
        {
            this.Item = item;
            this.Attributes = Attributes;
        }
    }

    /// <summary>
    /// 道具属性结构体
    /// </summary>
    [System.Serializable]
    public struct ItemAttributes
    {
        public int ItemValue { get; set; }
        public bool IsEquipped { get; set; }

        /// <summary>
        /// 结构体构造函数
        /// </summary>
        /// <param name="itemValue">道具的属性值</param>
        /// <param name="isEquipped">道具是否已经装备</param>
        public ItemAttributes(int itemValue, bool isEquipped)
        {
            this.ItemValue = itemValue;
            this.IsEquipped = isEquipped;
        }
    }
}