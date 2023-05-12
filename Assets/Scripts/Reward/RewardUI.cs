using UnityEngine;
using UnityEngine.UI;

namespace Reward
{
    public class RewardUI : MonoBehaviour
    {
        // 图标消失时间
        public float intervalInSeconds = 10f;

        // 初始归属父对象
        public RewardObjPool poolObject;

        // 图片组件
        private Image _color;

        // 初始图片颜色
        private Color _startingColor;

        // 目标图片颜色
        private Color _targetColor;

        // 是否正在淡出
        private bool _isFading;

        // 当前计时器
        private float _currentTime = 0f;

        private void OnEnable()
        {
            // 获取RewardObjPool组件
            poolObject = transform.parent.gameObject.GetComponent<RewardObjPool>();

            // 获取Image组件
            _color = this.gameObject.GetComponent<Image>();

            // 还原颜色状态
            _startingColor = new Color(_color.color.r, _color.color.g, _color.color.b, 1);
            _color.color = _startingColor;

            // 设置目标状态为完全透明
            _targetColor = new Color(_color.color.r, _color.color.g, _color.color.b, 0);

            // 归零计时器
            _currentTime = 0f;

            // 开始淡出效果
            StartFading();
        }

        // 每帧更新
        void Update()
        {
            if (_isFading)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= intervalInSeconds)
                {
                    // 将该对象返回对象池
                    poolObject.ReturnRewardObject(this.gameObject);
                    _isFading = false;
                    _currentTime = 0f;
                }
                else
                {
                    // 在给定时间内使用插值改变图片颜色，实现淡出效果
                    _color.color =
                        Color.Lerp(
                            _startingColor,
                            _targetColor,
                            _currentTime / intervalInSeconds);
                }
            }
        }

        // 开始图标淡出效果
        private void StartFading()
        {
            _isFading = true;
        }
    }
}