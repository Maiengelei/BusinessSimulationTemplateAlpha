using UnityEngine;

namespace Lottery.Manager
{
    public class LotteryTrigger : MonoBehaviour
    {
        public delegate void LotteryPlayerDelegate(Collider other);

        public event LotteryPlayerDelegate PlayerTriggerEnter;
        public event LotteryPlayerDelegate PlayerTriggerExit;

        // ---------------------------------------------------------------

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