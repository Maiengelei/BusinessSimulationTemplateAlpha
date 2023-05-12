using System;
using System.Collections.Generic;
using Inventory.InventoryScriptableObject;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Reward
{
    public class ClickerManager : MonoBehaviour
    {
        // 奖励值
        [SerializeField] private int rewardValue;

        // 玩家背包
        [SerializeField] private InventoryObject inventoryObject;

        // 奖励UI对象池
        [SerializeField] private RewardObjPool rewardObjPool;

        // 屏幕区域尺寸
        private Vector3 _screenBounds;

        // 玩家背包
        private InventoryObject _inventoryObject;
        
        // 长按多久算作一次长按
        public float holdTime = 0.1f; 
        
        // 每隔多久调用一次函数
        public float interval = 1f; 
        
        // 点击累计时间
        private float _timer = 0.0f;
        
        // 是否在长按
        private bool _isHolding = false;

        private void OnEnable()
        {
            _inventoryObject = GameObject.FindWithTag("Player").GetComponent<GamerObjectProperties>().InventoryObject;
        }

        private void Start()
        {
            _screenBounds =
                Camera.main.ScreenToWorldPoint(
                    new Vector3(
                        Screen.width,
                        Screen.height,
                        Camera.main.transform.position.z)
                );
        }

        private void Update()
        {
            // 点击
            ClickScreen();
        }

        private void ClickScreen()
        {
            // 判断鼠标左键或触摸屏幕
            if (Input.GetMouseButtonDown(0))
            {
                // 如果点击的位置没有UI
                if (!IsPointerOverUIObject())
                {
                    // 单点时调用
                    if (_isHolding == false)
                    {
                        GetReward();
                    }
                    
                    _isHolding = true;
                    _timer = 0.0f;
                }
            }

            // 抬起
            if (Input.GetMouseButtonUp(0))
            {
                _isHolding = false;
            }

            // 如果正在长按屏幕
            if (_isHolding)
            {
                _timer += Time.deltaTime; // 累加时间
                if (_timer >= interval) // 判断是否达到设定的时间
                {
                    _timer = 0.0f; // 重置计时器
                    GetReward(); // 调用函数
                }
            }
        }

        // 检测是否点击了UI
        private bool IsPointerOverUIObject()
        {
            // 创建一个新的PointerEventData实例
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

            // 设置PointerEventData的位置为当前鼠标的位置
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            // 创建一个用来存储所有被点击的对象的列表
            List<RaycastResult> results = new List<RaycastResult>();

            // 使用EventSystem来发射一条光线并获取所有的结果。这条光线将会穿过所有的UI元素，然后返回所有被点击的对象
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            // 如果结果列表中有元素，那么鼠标点击的位置就有UI元素，返回true；否则，返回false
            return results.Count > 0;
        }


        // 获取一次奖励
        public void GetReward()
        {
            inventoryObject.AddMoney(rewardValue + _inventoryObject.GetEquippedSum());
            GameObject obj = rewardObjPool.GetRewardObject();

            // 给予随机位置
            obj.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(
                    Random.Range(-_screenBounds.x - Screen.width / 4f, _screenBounds.x + Screen.width / 4f),
                    Random.Range(-_screenBounds.y - Screen.height / 4f, _screenBounds.y + Screen.height / 4f)
                );

            // 启用组件
            obj.SetActive(true);
        }
    }
}