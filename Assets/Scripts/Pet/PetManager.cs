/*
 * 宠物管理脚本，用于实例化、控制距离、删除宠物
 */


using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Pet
{
    public class PetManager : MonoBehaviour
    {
        private GameObject _player; // 玩家对象
        public float distance; // 预制件之间的距离

        private List<GameObject> _prefabList = new List<GameObject>();

        private void OnEnable()
        {
            _player = GameObject.FindWithTag("Player");
            
            _player.GetComponent<GamerObjectProperties>().InventoryObject.PetAddList += CreatePrefab;
            _player.GetComponent<GamerObjectProperties>().InventoryObject.PetRemoveList += DeletePrefab;
        }

        private void OnDestroy()
        {
            _player.GetComponent<GamerObjectProperties>().InventoryObject.PetAddList -= CreatePrefab;
            _player.GetComponent<GamerObjectProperties>().InventoryObject.PetRemoveList -= DeletePrefab;
        }

        private void Update()
        {
            UpdatePrefabPositions();
        }

        // 用于实例化预制件并将其添加到列表中
        private void CreatePrefab(GameObject prefabObject)
        {
            // 计算新预制件的位置
            Vector3 pos = _player.transform.position + new Vector3(_prefabList.Count * distance, 0, 0);

            // 实例化新预制件
            GameObject obj = Instantiate(prefabObject, pos, Quaternion.identity, transform);

            // 将新预制件添加到列表中
            _prefabList.Add(obj);
        }

        // 更新预制件的位置，使其跟随玩家
        private void UpdatePrefabPositions()
        {
            for (int i = 0; i < _prefabList.Count; i++)
            {
                Vector3 newPos = _player.transform.position + new Vector3(i * distance, 0, 0);
                _prefabList[i].transform.position = newPos;
            }
        }

        private void DeletePrefab(int index)
        {
            // 如果该元素存在
            if (index < _prefabList.Count)
            {
                // 移除该元素
                _prefabList.RemoveAt(index);
            }
        }
    }
}