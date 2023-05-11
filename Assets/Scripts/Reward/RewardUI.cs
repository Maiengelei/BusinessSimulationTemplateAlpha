using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Reward
{
    public class RewardUI : MonoBehaviour
    {
        /// <summary>
        /// 图标消失时间
        /// </summary>
        public float intervalInSeconds = 10f;

        /// <summary>
        /// 初始归属父对象
        /// </summary>
        public RewardObjPool poolObject;

        private Image _color;
        private Color _startingColor;
        private Color _targetColor;

        private Coroutine _fadeCoroutine;
        private bool _isFading;
        
        private float _currentTime = 0f;

        private void OnEnable()
        {
            poolObject = transform.parent.gameObject.GetComponent<RewardObjPool>();
            
            _color = this.gameObject.GetComponent<Image>();

            // 还原颜色状态
            _startingColor = new Color(_color.color.r, _color.color.g, _color.color.b, 1);
            _color.color = _startingColor;

            // 设置目标状态
            _targetColor = new Color(_color.color.r, _color.color.g, _color.color.b, 0);

            // 归零计数器
            _currentTime = 0f;

            StartFading();
        }


        void Update()
        {
            if (_isFading)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= intervalInSeconds)
                {
                    poolObject.ReturnRewardObject(this.gameObject);
                    _isFading = false;
                    _currentTime = 0f;
                }
                else
                {
                    _color.color = Color.Lerp(_startingColor, _targetColor, _currentTime / intervalInSeconds);
                }
            }
        }

        private void StartFading()
        {
            _isFading = true;
        }
    }
}