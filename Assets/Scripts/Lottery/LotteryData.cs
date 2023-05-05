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
        private struct LotteryDataBase
        {
            public Item item;
            public float weighted;
        }

        [SerializeField] private List<LotteryDataBase> lotteryList;
    }
}
