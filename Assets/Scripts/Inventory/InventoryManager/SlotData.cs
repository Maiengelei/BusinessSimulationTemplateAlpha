using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.InventoryManager
{
    public class SlotData : MonoBehaviour
    {
        // 道具图标
        [SerializeField] public Image itemImage;

        // 道具属性
        [SerializeField] public TextMeshProUGUI itemValue;

        // 道具格子编号
        public int gridSlotConst;

        private void Awake()
        {
            // 为当前的格子编号 0开始 对应 itemList
            gridSlotConst = transform.GetSiblingIndex();
        }
    }
}