using System;
using System.Collections.Generic;
using Inventory.InventoryScriptableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lottery
{
    public class LotteryData : MonoBehaviour
    {
        [Serializable]
        public struct LotteryDataBase
        {
            public ItemObject itemObject;
            public int weighted;
        }

        [SerializeField] 
        List<LotteryDataBase> lotteryList;

        /// <summary>
        /// 获取列表
        /// </summary>
        public List<LotteryDataBase> GetLotteryList => lotteryList;

        /// <summary>
        /// 从列表中返回一个随机Item 根据权重
        /// </summary>
        /// <returns>Item</returns>
        public ItemObject GetRandomItem()
        {
            // 总权重
            int totalWeight = 0;
            foreach (var item in lotteryList)
            {
                totalWeight += item.weighted;
            }
            
            return null;
        }
    }
}
