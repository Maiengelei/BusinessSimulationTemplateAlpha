using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Lottery.Manager
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

        /// <summary>
        /// 更新抽奖机奖品界面
        /// </summary>
        private void UpdateGrid()
        {
            foreach (var item in _lotteryList.GetLotteryList)
            {
                var itemObj = Instantiate(lotterySlotPrefab,lotteryGrid);
                itemObj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.item.itemSprite;
            }
        }
    }
}