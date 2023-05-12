using Lottery.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Lottery
{
    public class LotteryClick : MonoBehaviour
    {
        private Button _btn;

        public LotteryManager lottery;


        private void Start()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            lottery.GetLotteryItem();
        }
    }
}