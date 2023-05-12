using TMPro;
using UnityEngine;

namespace Tips
{
    public class TipsManager : MonoBehaviour
    {
        // 单例
        public static TipsManager Instance;

        // 提示条
        public GameObject tipsObject;

        // 提示文本
        public TextMeshProUGUI tipsText;

        // 提示显示时间
        private float _tipsTime = 2f;

        // 已经显示时间
        private float _currentTime = 0f;

        // 是否正在占用？
        private bool _isEnable;
        
        void Awake()
        {
            // 单例模式
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        private void OnEnable()
        {
            // 启动后默认关闭Tips
            tipsObject.SetActive(false);
        }

        private void Update()
        {
            // 如果tips已经启用
            if (_isEnable)
            {
                _currentTime += Time.deltaTime;
                
                // 当启用时间超过原计划时间时
                if (_currentTime >= _tipsTime)
                {
                    CloseTips();
                }
            }
        }

        // 启用Tips
        // 如果Tips正在启用，则更新Tips文本并重置计数器
        public void AddTips(string str)
        {
            // 重置计时器
            _currentTime = 0f;

            // 将提示转移到TextMesh上
            tipsText.text = str;

            // 如果提示条没有激活
            if (!tipsObject.activeSelf)
            {
                tipsObject.SetActive(true);
                _isEnable = true;
            }
        }

        // 关闭Tips
        private void CloseTips()
        {
            // 如果当前Tips正在启用则关闭
            if (tipsObject.activeSelf)
            {
                tipsText.text = "";
                tipsObject.SetActive(false);
                _isEnable = false;
            }
        }
    }
}