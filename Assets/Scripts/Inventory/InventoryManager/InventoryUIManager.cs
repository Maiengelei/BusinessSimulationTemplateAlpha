using Inventory.InventoryScriptableObject;
using Player;
using UnityEngine;

namespace Inventory.InventoryManager
{
    public class InventoryUIManager : MonoBehaviour
    {
        /// <summary>
        /// 对应背包
        /// </summary>
        private InventoryObject _inventoryObject;

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
        [SerializeField] private Transform grid;

        // -----------------------------------------------------------------------------------

        private void OnEnable()
        {
            // 通过 Tag 获取玩家对象
            GameObject player = GameObject.FindWithTag("Player");
            // 获取到玩家的背包
            _inventoryObject = player.GetComponent<GamerObjectProperties>().InventoryObject;

            // 增加委托
            _inventoryObject.UpdateUI += UpdateItem;
            
            // 打开界面时自动刷新
            UpdateItem();
        }

        private void OnDisable()
        {
            // 移除委托
            _inventoryObject.UpdateUI -= UpdateItem;
            
            CheckSlotFrame(_slotListID, false);
            _slotListID = -1;
        }

        // -----------------------------------------------------------------------------------

        /// <summary>
        /// 根据指定的列表进行更新数据
        /// </summary>
        private void UpdateItem()
        {
            // 将所有的子物体取消激活
            for (int i = 0; i < grid.childCount; i++)
            {
                for (int j = 0; j < grid.GetChild(i).childCount; j++)
                {
                    grid.GetChild(i).GetChild(j).gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < _inventoryObject.itemList.Count; i++)
            {
                // 获取指定格子的数据
                _slotData = grid.GetChild(i).GetComponent<SlotData>();

                // 增加图标
                _slotData.itemImage.sprite =
                    _inventoryObject.itemList[i].Item.itemSprite;

                // 增加数值
                _slotData.itemValue.text =
                    _inventoryObject.itemList[i].Attributes.ItemValue.ToString();

                // 判断是否为已装备道具
                if (_inventoryObject.itemList[i].Attributes.IsEquipped)
                {
                    // 启用 已装备 图标
                    grid.GetChild(i).Find("IsEquipped").gameObject.SetActive(true);
                }

                // 启用这个格子
                grid.GetChild(i).Find("Item").gameObject.SetActive(true);
            }
        }
        
        /// <summary>
        /// 检查点击的格子状态，修改点击状态
        /// </summary>
        /// <param name="gridSlotConst"></param>
        public void CheckSlot(int gridSlotConst)
        {
            // 检查上一个物品栏，如果正在高亮则取消高亮上一个物品栏
            CheckSlotFrame(_slotListID, false);

            // 更新点击格子
            _slotListID = gridSlotConst;

            // 高亮选中的格子
            CheckSlotFrame(gridSlotConst, true);

        }

        /// <summary>
        /// 高亮/取消高亮一个的物品栏
        /// </summary>
        /// <param name="slotListID">指定的格子ID</param>
        /// <param name="condition">指定的状态</param>
        private void CheckSlotFrame(int slotListID, bool condition = true)
        {
            // 关闭
            if (condition == false)
            {
                // 检查上一个物品栏
                if (slotListID != -1 && grid.GetChild(slotListID).Find("Frame").gameObject.activeInHierarchy)
                {
                    // 取消高亮
                    grid.GetChild(slotListID).Find("Frame").gameObject.SetActive(false);
                }
            }
            else
            {
                // 检查上一个物品栏
                if (slotListID != -1 && grid.GetChild(slotListID).Find("Frame").gameObject.activeInHierarchy == false)
                {
                    // 取消高亮
                    grid.GetChild(slotListID).Find("Frame").gameObject.SetActive(true);
                }
            }
        }

        // -----------------------------------------------------------------------------------

        public void FuncEquip()
        {
            _inventoryObject.IsEquippedOn(_slotListID);
        }

        public void FuncUnEquip()
        {
            _inventoryObject.IsEquippedOff(_slotListID);
        }

        public void FuncDelete()
        {
            _inventoryObject.RemoveItemToList(_slotListID);
        }

        public void FuncDeleteAll()
        {
            _inventoryObject.RemoveItemToListAll();
        }
    }
}