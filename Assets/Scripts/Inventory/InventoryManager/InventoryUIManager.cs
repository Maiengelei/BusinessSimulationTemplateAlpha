using Player;
using UnityEngine;

namespace Inventory.InventoryManager
{
    public class InventoryUIManager : MonoBehaviour
    {
        /// <summary>
        /// 对应背包
        /// </summary>
        public InventoryObject.Inventory inventory;

        /// <summary>
        /// 点击的格子ID
        /// </summary>
        private int _slotListID;

        /// <summary>
        /// 生成的数据参考，用于承载显示在界面中的内容
        /// </summary>
        private SlotData _slotData;

        /// <summary>
        /// 格子父类
        /// </summary>
        private Transform _grid;

        // -----------------------------------------------------------------------------------

        private void OnEnable()
        {
            // 通过 Tag 获取玩家对象
            GameObject player = GameObject.FindWithTag("Player");
            // 获取到玩家的背包
            inventory = player.GetComponent<GamerObjectProperties>().Inventory;
            
            // 增加委托
            inventory.UpdateUI += UpdateItem;
        }

        private void OnDisable()
        {
            // 移除委托
            inventory.UpdateUI -= UpdateItem;
        }

        // -----------------------------------------------------------------------------------

        /// <summary>
        /// 根据指定的列表进行更新数据
        /// </summary>
        private void UpdateItem()
        {
            // 将所有的子物体取消激活
            for (int i = 0; i < _grid.childCount; i++)
            {
                for (int j = 0; j < _grid.GetChild(i).childCount; j++)
                {
                    _grid.GetChild(i).GetChild(j).gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < inventory.itemList.Count; i++)
            {
                // 获取指定格子的数据
                _slotData = _grid.GetChild(i).GetComponent<SlotData>();

                // 增加图标
                _slotData.itemImage.sprite =
                    inventory.itemList[i].Item.itemSprite;

                // 增加数值
                _slotData.itemValue.text =
                    inventory.itemList[i].Attributes.ItemValue.ToString();

                // 判断是否为已装备道具
                if (inventory.itemList[i].Attributes.IsEquipped)
                {
                    // 启用 已装备 图标
                    _grid.GetChild(i).Find("IsEquipped").gameObject.SetActive(true);
                }

                // 启用这个格子
                _grid.GetChild(i).Find("Item").gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 高亮/取消高亮一个的物品栏
        /// </summary>
        /// <param name="slotListID">指定的格子ID</param>
        /// <param name="condition">指定的状态</param>
        private void CheckSlotFrame(int slotListID, bool condition)
        {
            // 关闭
            if (condition == false)
            {
                // 检查上一个物品栏
                if (slotListID != -1 && _grid.GetChild(slotListID).Find("Frame").gameObject.activeInHierarchy)
                {
                    // 取消高亮
                    _grid.GetChild(slotListID).Find("Frame").gameObject.SetActive(false);
                }
            }
            else
            {
                // 检查上一个物品栏
                if (slotListID != -1 && _grid.GetChild(slotListID).Find("Frame").gameObject.activeInHierarchy == false)
                {
                    // 取消高亮
                    _grid.GetChild(slotListID).Find("Frame").gameObject.SetActive(true);
                }
            }
        }

        // -----------------------------------------------------------------------------------

        public void FuncEquip()
        {
            inventory.IsEquippedOn(_slotListID);
        }

        public void FuncUnEquip()
        {
            inventory.IsEquippedOff(_slotListID);
        }

        public void FuncDelete()
        {
            inventory.RemoveItemToList(_slotListID);
        }

        public void FuncDeleteAll()
        {
            inventory.RemoveItemToListAll();
        }
    }
}