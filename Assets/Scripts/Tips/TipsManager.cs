using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tips
{
    public class TipsManager : MonoBehaviour
    {
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
            if (_isEnable)
            {
                _currentTime += Time.deltaTime;
                
                if (_currentTime >= _tipsTime)
                {
                    CloseTips();
                }
            }
        }

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

        private void CloseTips()
        {
            if (tipsObject.activeSelf)
            {
                tipsText.text = "";
                tipsObject.SetActive(false);
                _isEnable = false;
            }
        }
    }
}