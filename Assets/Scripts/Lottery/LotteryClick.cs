using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lottery
{
    public class LotteryClick : MonoBehaviour
    {
        private Button _btn;

        private void Start()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            
        }
    }
}
