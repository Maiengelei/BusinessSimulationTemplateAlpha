using System;
using System.Collections.Generic;
using Inventory.InventoryObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lottery
{
    public class LotteryData : MonoBehaviour
    {
        [Serializable]
        public struct LotteryDataBase
        {
            public Item item;
            public float weighted;
        }

        [SerializeField] 
        List<LotteryDataBase> lotteryList;

        /// <summary>
        /// 获取列表
        /// </summary>
        public List<LotteryDataBase> GetLotteryList => lotteryList;

        public Item GetRandomItem()
        {
            return null;
        }
    }
}
