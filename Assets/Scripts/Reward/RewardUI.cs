using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Reward
{
    public class RewardUI : MonoBehaviour
    {
        /// <summary>
        /// 图标消失时间
        /// </summary>
        public float intervalInSeconds = 10f;

        private Image _color;
        private Color _startingColor;
        private Color _targetColor;

        private Coroutine _fadeCoroutine;
        private bool _isFading;

        private void OnEnable()
        {
            _color = this.gameObject.GetComponent<Image>();

            // 还原颜色状态
            _startingColor = new Color(_color.color.r, _color.color.g, _color.color.b, 1);
            _color.color = _startingColor;

            // 设置目标状态
            _targetColor = new Color(_color.color.r, _color.color.g, _color.color.b, 0);

            if (!_isFading)
            {
                if (_fadeCoroutine != null)
                {
                    StopCoroutine(_fadeCoroutine);
                }

                _fadeCoroutine = StartCoroutine(ExecuteFunction());
            }
        }

        /// <summary>
        /// 启用协程
        /// </summary>
        IEnumerator ExecuteFunction()
        {
            _isFading = true;
            float currentTime = 0f;

            // 渐变
            while (currentTime < intervalInSeconds)
            {
                currentTime += Time.deltaTime;
                _color.color = Color.Lerp(_startingColor, _targetColor, currentTime / intervalInSeconds);
                yield return null;
            }

            gameObject.SetActive(false);
            _isFading = false;
        }
    }
}