using UnityEngine;
using UnityEngine.UI;

namespace Lottery.LotteryUI
{
    public class LotteryUIManager : MonoBehaviour
    {
        /// <summary>
        /// 抽奖机对象
        /// </summary>
        public GameObject lottery;

        /// <summary>
        /// 抽奖机格子父对象
        /// </summary>
        public Transform lotteryGrid;

        /// <summary>
        /// 抽奖机格子预制体
        /// </summary>
        public GameObject lotterySlotPrefab;

        /// <summary>
        /// 接受 LotteryList
        /// </summary>
        private LotteryData _lotteryList;

        private void OnEnable()
        {
            // 启用UI时获取抽奖机奖池
            _lotteryList = lottery.GetComponent<LotteryData>();

            // 启动时更新界面
            UpdateGrid();
        }

        private void OnDisable()
        {
            // 清空UI界面元素
            DropGrid();
        }

        /// <summary>
        /// 更新抽奖机奖品界面
        /// </summary>
        private void UpdateGrid()
        {
            foreach (var item in _lotteryList.GetLotteryList)
            {
                var itemObj = Instantiate(lotterySlotPrefab,lotteryGrid);
                itemObj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemObject.itemSprite;
            }
        }

        /// <summary>
        /// 清空抽奖机界面
        /// </summary>
        private void DropGrid()
        {
            for (int i = 0; i < lotteryGrid.childCount; i++)
            {
                Destroy(lotteryGrid.GetChild(i).gameObject);
            }
        }
    }
}