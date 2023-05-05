using System.Collections.Generic;
using System.Linq;
using Inventory.InventoryData;
using UnityEngine;
using Random = UnityEngine.Random; // 命名空间冲突，使用 Using 解决冲突

namespace Inventory.InventoryObject
{
    [CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory/Inventory")]
    public class Inventory : ScriptableObject
    {
        /// <summary>
        /// 创建一个委托
        /// </summary>
        public delegate void InventoryUI();

        /// <summary>
        /// 用于更新背包 UI 的事件
        /// </summary>
        public event InventoryUI UpdateUI;

        // -----------------------------------------------------------------------------------

        /// <summary>
        /// 背包最大空间
        /// </summary>
        public int inventoryLenght = 18;

        /// <summary>
        /// 允许装备的最大数量
        /// </summary>
        public int isEquippedValue = 3;

        /// <summary>
        /// 当前已经装备的装备数量
        /// </summary>
        public int isEquippedOnValue;

        /// <summary>
        /// 已装备的道具总数值
        /// </summary>
        public int equippedSum;

        /// <summary>
        /// 背包列表 List
        /// </summary>
        public List<InventoryList<Item, ItemAttributes>>
            itemList = new List<InventoryList<Item, ItemAttributes>>();

        // -----------------------------------------------------------------------------------

        /// <summary>
        /// 检查该背包是否已经满了
        /// </summary>
        /// <returns>背包已满时 False</returns>
        private bool CheckItemList()
        {
            // 如果当前列表中的值是否超过上限
            if (itemList.Count < inventoryLenght)
                return true;
            return false;
        }

        /// <summary>
        /// 重新排序列表
        /// 按照 已装备 未装备 分组
        /// </summary>
        public void ListSorting()
        {
            itemList = itemList
                .OrderByDescending(x => x.Attributes.IsEquipped) // 先判断是否为已经装备
                .ThenByDescending(x => x.Attributes.ItemValue) // 再根据数值降序排序
                .ToList();
        }

        /// <summary>
        /// 向背包中增加道具
        /// </summary>
        /// <param name="item">要增加的道具</param>
        public void AddItemToList(Item item)
        {
            // 检查列表是否已经满了
            if (CheckItemList())
            {
                // 计算道具属性值随机数
                int itemValueTemp = Random.Range(item.itemValueMin, item.itemValueMax);

                // 填写道具属性
                ItemAttributes itemAttributes = new ItemAttributes(itemValueTemp, false);

                // 根据传入道具和道具属性构建 List 元素
                // 道具默认不装备
                InventoryList<Item, ItemAttributes> itemTemp =
                    new InventoryList<Item, ItemAttributes>(item, itemAttributes);

                // 将值填入 List
                itemList.Add(itemTemp);

                // 重新排序并更新UI
                if (UpdateUI != null)
                {
                    UpdateUI();
                }
            }
        }

        /// <summary>
        /// 移除列表中的某个元素
        /// </summary>
        /// <param name="itemID">列表元素ID，一般来说与背包的格子顺序ID相同</param>
        public void RemoveItemToList(int itemID)
        {
            // 检查元素是否存在于 List
            if (itemID >= itemList.Count || itemID < 0)
            {
#if UNITY_EDITOR
                Debug.Log($"元素 {itemID} 不存在");
#endif
                return;
            }

            // 检查元素是否正在装备
            if (itemList[itemID].Attributes.IsEquipped)
            {
#if UNITY_EDITOR
                Debug.Log($"元素 {itemID} 正在装备中");
#endif
                return;
            }

            // 在列表中删除指定的元素
            itemList.RemoveAt(itemID);

            // 重新排序并更新UI
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// 移除列表中所有 IsEquipped 为 False 的元素
        /// </summary>
        public void RemoveItemToListAll()
        {
            itemList.RemoveAll(inventoryList => inventoryList.Attributes.IsEquipped == false);

            // 重新排序并更新UI
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// 装备指定的道具
        /// </summary>
        /// <param name="itemID">道具ID，一般 UI 格子排序就是 List 排序</param>
        public void IsEquippedOn(int itemID)
        {
            // 检查元素是否存在于 List
            if (itemID >= itemList.Count || itemID < 0)
            {
#if UNITY_EDITOR
                Debug.Log($"元素 {itemID} 不存在");
#endif
                return;
            }

            // 检查元素是否正在装备
            if (itemList[itemID].Attributes.IsEquipped)
            {
#if UNITY_EDITOR
                Debug.Log($"元素 {itemID} 正在装备中");
#endif
                return;
            }

            // 判断装备栏是否满了
            if (isEquippedOnValue >= isEquippedValue)
            {
#if UNITY_EDITOR
                Debug.Log($"装备栏满了");
#endif
                return;
            }

            // 修改属性
            // inventory.itemList[itemID].Attributes.ChangeStatus(true);
            // 注意，在 C# 中，结构体是一个值类型，这导致直接修改的是副本，所以装备道具不生效
            // 获取结构体
            var tempAttributes = itemList[itemID].Attributes;
            // 修改结构体属性
            tempAttributes.IsEquipped = true;
            // 重新赋值结构体，替换原本内容
            itemList[itemID].Attributes = tempAttributes;

            // 装备自增
            isEquippedOnValue += 1;

            // 更新总值
            equippedSum = GetEquippedSum();

            // 重新排序并更新UI
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// 解除装备指定的道具
        /// </summary>
        /// <param name="itemID">道具ID，一般 UI 格子排序就是 List 排序</param>
        public void IsEquippedOff(int itemID)
        {
            // 检查元素是否存在于 List
            if (itemID >= itemList.Count || itemID < 0)
            {
#if UNITY_EDITOR
                Debug.Log($"元素 {itemID} 不存在");
#endif
                return;
            }

            // 检查元素是否正在装备
            if (itemList[itemID].Attributes.IsEquipped == false)
            {
#if UNITY_EDITOR
                Debug.Log($"元素 {itemID} 没有在装备中");
#endif
                return;
            }

            // 修改属性
            // 获取结构体
            var tempAttributes = itemList[itemID].Attributes;
            // 修改结构体属性
            tempAttributes.IsEquipped = false;
            // 重新赋值结构体，替换原本内容
            itemList[itemID].Attributes = tempAttributes;

            // 装备自减
            isEquippedOnValue -= 1;
            if (isEquippedOnValue < 0)
            {
                isEquippedOnValue = 0;
            }

            // 更新总值
            equippedSum = GetEquippedSum();

            // 重新排序并更新UI
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// 获取背包中所有已装备的道具的值的总和
        /// </summary>
        private int GetEquippedSum()
        {
            int sum = 0;

            foreach (var t in itemList)
            {
                if (t.Attributes.IsEquipped)
                {
                    sum += t.Attributes.ItemValue;
                }
            }

            return sum;
        }
    }
}