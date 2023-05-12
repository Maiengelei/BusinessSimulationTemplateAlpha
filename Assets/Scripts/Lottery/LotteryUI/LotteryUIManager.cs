/*
 * 抽奖机界面管理器
 */

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Lottery.LotteryUI
{
    public class LotteryUIManager : MonoBehaviour
    {
        // 抽奖机对象
        public GameObject lottery;

        // 抽奖机格子父对象
        public Transform lotteryGrid;

        // 抽奖机格子预制体
        public GameObject lotterySlotPrefab;

        // 接受 LotteryList
        private LotteryData _lotteryList;

        private void OnEnable()
        {
            if (lotteryGrid.childCount == 0)
            {
                // 启用UI时获取抽奖机奖池
                _lotteryList = lottery.GetComponent<LotteryData>();

                // 启动时更新界面
                UpdateGrid();
                
                // 加载完后关闭
                gameObject.SetActive(false);
            }
        }

        // 更新抽奖机奖品界面
        private void UpdateGrid()
        {
            foreach (var item in _lotteryList.GetLotteryList)
            {
                // 实例化一个抽奖机中的对象
                var itemObj = Instantiate(lotterySlotPrefab,lotteryGrid);
                
                // 显示这个对象
                itemObj.transform.Find("ItemSprite").GetComponent<Image>().sprite = item.itemObject.itemSprite;
            }
        }
    }
}