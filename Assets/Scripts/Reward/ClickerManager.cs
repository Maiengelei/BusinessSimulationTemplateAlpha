using System;
using Inventory.InventoryScriptableObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Reward
{
    public class ClickerManager : MonoBehaviour
    {
        /// <summary>
        /// 奖励值
        /// </summary>
        [SerializeField] private int rewardValue;

        /// <summary>
        /// 长按触发间隔,单位秒
        /// </summary>
        [SerializeField] private float triggerInterval = 1 / 20f;

        /// <summary>
        /// 玩家背包
        /// </summary>
        [SerializeField] private InventoryObject inventoryObject;
        
        /// <summary>
        /// 奖励UI对象池
        /// </summary>
        [SerializeField] private RewardObjPool rewardObjPool;

        [SerializeField] private RectTransform rectTransform;

        private Vector3[] _corners = new Vector3[4];

        private void Start()
        {
            // 获取组件在区域中的四个角
            rectTransform.GetWorldCorners(_corners);
        }

        /// <summary>
        /// 获取一次奖励
        /// </summary>
        public void GetReward()
        {
            inventoryObject.money += rewardValue;
            GameObject obj = rewardObjPool.GetRewardObject();
            
            // 给予随机位置
            obj.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    Random.Range(_corners[3].x, _corners[1].x),
                    Random.Range(_corners[3].y, _corners[1].y)
                    );
            
            // 启用组件
            obj.SetActive(true);
        }
        
        
    }
}