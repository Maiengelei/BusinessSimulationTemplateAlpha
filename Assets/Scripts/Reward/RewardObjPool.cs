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
        public GameObject rewardObjectPrefab;

        public int poolSize = 16;

        public bool expandPool = true;

        private List<GameObject> _pooledObjects;

        // ------------------------------------------------------

        private void Start()
        {
            _pooledObjects = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(rewardObjectPrefab,transform);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }

        /// <summary>
        /// 构建对象池
        /// </summary>
        /// <returns>目标对象</returns>
        public GameObject GetRewardObject()
        {
            for (int i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    return _pooledObjects[i];
                }
            }

            if (expandPool)
            {
                GameObject obj = Instantiate(rewardObjectPrefab,transform);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
                return obj;
            }

            return null;
        }
    }
}