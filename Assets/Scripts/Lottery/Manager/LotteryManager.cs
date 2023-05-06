using UnityEngine;

/*
 * 1. 通过box碰撞来获得进去区域的玩家
 * 2. 当进去区域的玩家为Player时，为其打开抽奖按钮
 * 3. 当点击抽奖时，先检查玩家背包是否已经满了
 * 3.1 如果满了则提示玩家背包已满
 * 3.2 如果有空位则开始抽奖
 */
namespace Lottery.Manager
{
    public class LotteryManager : MonoBehaviour
    {
        public delegate void LotteryPlayerDelegate(Collider other);
        public event LotteryPlayerDelegate PlayerStatus;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (PlayerStatus != null)
                {
                    PlayerStatus(other);
                }
            }
        }
    }
}