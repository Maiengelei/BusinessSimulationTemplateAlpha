using System.Collections.Generic;
using System.Linq;
using Inventory.InventoryData;
using UnityEngine;
using Random = UnityEngine.Random; // 命名空间冲突，使用 Using 解决冲突

namespace Inventory.InventoryScriptableObject
{
    [CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        // 创建委托
        public delegate void InventoryUI();
        public delegate void PetAdd(GameObject obj);
        public delegate void PetRemove(int index);

        // 用于更新背包 UI 的事件
        public event InventoryUI UpdateUI;

        // 更新金币事件
        public event InventoryUI UpdateMoneyUI;

        public event PetAdd PetAddList;

        public event PetRemove PetRemoveList;

        // -----------------------------------------------------------------------------------

        // 玩家的钱
        [SerializeField] private int money;

        // 背包最大空间
        public int inventoryLenght = 18;

        // 允许装备的最大数量
        public int isEquippedValue = 3;

        // 当前已经装备的装备数量
        public int isEquippedOnValue;

        // 已装备的道具总数值
        public int equippedSum;

        // 背包列表 List
        public List<InventoryList<ItemObject, ItemAttributes>>
            itemList = new List<InventoryList<ItemObject, ItemAttributes>>();

        // -----------------------------------------------------------------------------------

        // 检查该背包是否已经满了
        public bool CheckItemList()
        {
            // 如果当前列表中的值是否超过上限
            if (itemList.Count < inventoryLenght)
                return true;
            return false;
        }

        // 重新排序列表
        // 按照 已装备 未装备 分组
        public void ListSorting()
        {
            itemList = itemList
                .OrderByDescending(x => x.Attributes.IsEquipped) // 先判断是否为已经装备
                .ThenByDescending(x => x.Attributes.ItemValue) // 再根据数值降序排序
                .ToList();
        }

        // 向背包中增加道具
        public void AddItemToList(ItemObject itemObject)
        {
            // 检查列表是否已经满了
            if (CheckItemList())
            {
                // 计算道具属性值随机数
                int itemValueTemp =
                    Random.Range(itemObject.itemValueMin, itemObject.itemValueMax);

                // 填写道具属性
                ItemAttributes itemAttributes =
                    new ItemAttributes(itemValueTemp, false);

                // 根据传入道具和道具属性构建 List 元素
                // 道具默认不装备
                InventoryList<ItemObject, ItemAttributes> itemTemp =
                    new InventoryList<ItemObject, ItemAttributes>(itemObject, itemAttributes);

                // 将值填入 List
                itemList.Add(itemTemp);

                // 重新排序并更新UI
                ListSorting();
                if (UpdateUI != null)
                {
                    UpdateUI();
                }
            }
        }

        // 移除列表中的某个元素
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
            ListSorting();
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        // 移除列表中所有 IsEquipped 为 False 的元素
        public void RemoveItemToListAll()
        {
            itemList.RemoveAll(inventoryList => inventoryList.Attributes.IsEquipped == false);

            // 重新排序并更新UI
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        // 装备指定的道具
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
            equippedSum = UpdateEquippedSum();

            if (PetAddList != null)
            {
                PetAddList(itemList[itemID].Item.itemModelPrefab);
            }

            // 重新排序并更新UI
            ListSorting();
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        // 解除装备指定的道具
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

            if (PetRemoveList != null)
            {
                PetRemoveList(itemID);
            }

            // 更新总值
            equippedSum = UpdateEquippedSum();

            // 重新排序并更新UI
            ListSorting();
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        // 查找已经装备的道具
        public List<InventoryList<ItemObject, ItemAttributes>> GetIsEquippedItemList()
        {
            // 构造临时 List
            List<InventoryList<ItemObject, ItemAttributes>> items =
                new List<InventoryList<ItemObject, ItemAttributes>>();

            // 检查 List 元素，如果当前 List 元素已经装备，则压入临时 List
            for (int i = 0; i < isEquippedValue; i++)
            {
                if (itemList[i].Attributes.IsEquipped)
                {
                    items.Add(itemList[i]);
                }
            }

            // 返回临时 List
            return items;
        }

        // 获取背包中所有已装备的道具的值的总和
        private int UpdateEquippedSum()
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

        // 增加金钱
        public void AddMoney(int money)
        {
            this.money += money;

            if (UpdateMoneyUI != null)
            {
                UpdateMoneyUI();
            }
        }

        // 减少金钱
        public bool ReduceMoney(int money)
        {
            if (this.money >= money)
            {
                this.money -= money;

                if (UpdateMoneyUI != null)
                {
                    UpdateMoneyUI();
                }

                return true;
            }

            return false;
        }

        // 获取当前金钱值
        public int GetMoney()
        {
            return money;
        }

        public int GetEquippedSum()
        {
            return equippedSum;
        }
    }
}