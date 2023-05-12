using Inventory.InventoryScriptableObject;
using Player;
using Tips;
using Unity.VisualScripting;
using UnityEngine;

namespace Lottery.Manager
{
    public class LotteryManager : MonoBehaviour
    {
        // 抽奖机的UI界面 旋转父物体
        public Transform uiRotatePoint;
        
        // 抽奖机碰撞组件
        public LotteryTrigger colliderTrigger;

        // ---------------------------------------------------------------
        
        // 抽奖价格
        [SerializeField] private int price = 50;
        
        // 玩家背包
        private InventoryObject _player;
        
        // 抽奖机数据存储对象
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

        // 当玩家进入 Trigger 时
        private void PlayerEnter(Collider other)
        {
            _player = other.GameObject()
                .GetComponent<GamerObjectProperties>()
                .InventoryObject;
            uiRotatePoint.gameObject.SetActive(true);
        }

        // 当玩家离开 Trigger 时
        private void PlayerExit(Collider other)
        {
            _player = null;
            uiRotatePoint.gameObject.SetActive(false);
        }

        // ---------------------------------------------------------------
        
        // 向玩家背包中新增道具
        public void GetLotteryItem()
        {
            if (_player.GetMoney() >= price)
            {
                // 检查玩家背包是否有足够的空间
                if (_player != null && _player.CheckItemList())
                {
                    _player.ReduceMoney(price);
                    var item = _lotteryData.GetRandomItem();
                    _player.AddItemToList(item);
                }
                else
                {
                    Debug.Log($"你的背包满了，背包最大容量为{_player.inventoryLenght}");
                    TipsManager.Instance.AddTips($"Your backpack is full. The maximum capacity of the backpack is {_player.inventoryLenght}.");
                }
            }
            else
            {
                Debug.Log($"你的钱不够，需要{price}个金币，你有{_player.GetMoney()}个金币");
                TipsManager.Instance.AddTips($"You don't have enough money. It costs {price} gold coins, and you only have {_player.GetMoney()} gold coins.");
            }
        }
    }
}