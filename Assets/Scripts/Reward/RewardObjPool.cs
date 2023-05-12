/*
 * 对象池组件，用于管理当点击屏幕后弹出的UI图标
 * 大量频繁的创建/移除组件会导致性能急剧下降，对象池可以有效的缓解这个问题
 */

using System.Collections.Generic;
using UnityEngine;

namespace Reward
{
    public class RewardObjPool : MonoBehaviour
    {
        // 奖励UI的预制体
        public GameObject rewardObjectPrefab;

        // 对象池大小
        public int poolSize = 16;

        // 当池子中的对象耗尽后是否自动拓展
        public bool expandPool = true;

        // 对象池列表
        private List<GameObject> _pooledObjects;

        // ------------------------------------------------------

        private void Start()
        {
            // 实例化对象池
            _pooledObjects = new List<GameObject>();

            // 对象池预先构建
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(rewardObjectPrefab,transform);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }

        // 获取对象池
        public GameObject GetRewardObject()
        {
            // 尝试从对象池中获取对象
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    return _pooledObjects[i];
                }
            }

            // 当对象不够时尝试在池中创建新的对象
            if (expandPool)
            {
                GameObject obj = Instantiate(rewardObjectPrefab,transform);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
                return obj;
            }

            return null;
        }

        // 回收对象
        public void ReturnRewardObject(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(this.transform,true);
        }
    }
}