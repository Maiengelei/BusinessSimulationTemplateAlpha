using Inventory.InventoryScriptableObject;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Lottery.Manager
{
    public class LotteryManager : MonoBehaviour
    {
        /// <summary>
        /// 抽奖机的UI界面 旋转父物体
        /// </summary>
        public Transform uiRotatePoint;

        /// <summary>
        /// 抽奖机碰撞组件
        /// </summary>
        public LotteryTrigger colliderTrigger;

        // ---------------------------------------------------------------

        /// <summary>
        /// 抽奖价格
        /// </summary>
        [SerializeField] private int price = 50;
        
        /// <summary>
        /// 玩家背包
        /// </summary>
        private InventoryObject _player;

        /// <summary>
        /// 抽奖机数据存储对象
        /// </summary>
        private LotteryData _lotteryData;

        // ---------------------------------------------------------------

        private void OnEnable()
        {
            _lotteryData = GetComponent<LotteryData>();

            colliderTrigger.PlayerTriggerEnter += PlayerEnter;
            colliderTrigger.PlayerTriggerExit += PlayerExit;
        }

        private void OnDestroy()
        {
            colliderTrigger.PlayerTriggerEnter -= PlayerEnter;
            colliderTrigger.PlayerTriggerExit -= PlayerExit;
        }

        // ---------------------------------------------------------------

        /// <summary>
        /// 当玩家进入 Trigger 时
        /// </summary>
        /// <param name="other">玩家</param>
        private void PlayerEnter(Collider other)
        {
            _player = other.GameObject()
                .GetComponent<GamerObjectProperties>()
                .InventoryObject;
            uiRotatePoint.gameObject.SetActive(true);
        }

        /// <summary>
        /// 当玩家离开 Trigger 时
        /// </summary>
        /// <param name="other">玩家</param>
        private void PlayerExit(Collider other)
        {
            _player = null;
            uiRotatePoint.gameObject.SetActive(false);
        }

        // ---------------------------------------------------------------

        /// <summary>
        /// 向玩家背包中新增道具
        /// </summary>
        public void GetLotteryItem()
        {
            if (_player.money >= price)
            {
                // 检查玩家背包是否有足够的空间
                if (_player != null && _player.CheckItemList())
                {
                    _player.money -= price;
                    var item = _lotteryData.GetRandomItem();
                    _player.AddItemToList(item);
                }
                else
                {
                    Debug.Log($"你的背包满了，背包最大容量为{_player.inventoryLenght}");
                }
            }
            else
            {
                Debug.Log($"你的钱不够");
            }
        }
    }
}