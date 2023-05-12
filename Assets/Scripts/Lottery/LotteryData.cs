using System;
using System.Collections.Generic;
using Inventory.InventoryScriptableObject;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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

        // 获取列表
        public List<LotteryDataBase> GetLotteryList => lotteryList;

        // 从列表中返回一个随机Item 根据权重
        public ItemObject GetRandomItem()
        {
            // 总权重
            int totalWeight = 0;
            foreach (var item in lotteryList)
            {
                totalWeight += item.weighted;
            }
            
            // 生成一个0到总权重之间的随机数
            int randomWeight = Random.Range(0, totalWeight);

            // 遍历道具列表，根据权重选择道具
            foreach (var item in lotteryList)
            {
                randomWeight -= item.weighted;

                if (randomWeight <= 0)
                {
                    return item.itemObject;
                }
            }
            
            return null;
        }
    }
}
