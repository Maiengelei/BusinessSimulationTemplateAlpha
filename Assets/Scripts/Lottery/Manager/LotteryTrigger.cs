using UnityEngine;

namespace Lottery.Manager
{
    public class LotteryTrigger : MonoBehaviour
    {
        // 委托
        public delegate void LotteryPlayerDelegate(Collider other);

        // 事件
        public event LotteryPlayerDelegate PlayerTriggerEnter;
        public event LotteryPlayerDelegate PlayerTriggerExit;

        // ---------------------------------------------------------------

        // 当玩家进入碰撞盒时
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (PlayerTriggerEnter != null)
                {
                    PlayerTriggerEnter(other);
                }
            }
        }

        // 当玩家退出碰撞盒时
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (PlayerTriggerExit != null)
                {
                    PlayerTriggerExit(other);
                }
            }
        }
    }
}