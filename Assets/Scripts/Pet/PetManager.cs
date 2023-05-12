/*
 * 宠物管理脚本，用于实例化、控制距离、删除宠物
 */


using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pet
{
    public class PetManager : MonoBehaviour
    {
        private GameObject _player;
        public float followDistance = 1.0f;
        private List<GameObject> _pets = new List<GameObject>();
        public float petSpacing = 1.0f;

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
            GameObject newPet = Instantiate(prefabObject,
                _player.transform.position - new Vector3(0, 0, petSpacing * (_pets.Count + 1)), Quaternion.identity);
            newPet.GetComponent<PetObject>().Initialize(_player, new Vector3(0, 0, -petSpacing * (_pets.Count + 1)));
            _pets.Add(newPet);
        }

        // 更新预制件的位置，使其跟随玩家
        private void UpdatePrefabPositions()
        {
            if (_pets.Count > 0)
            {
                float distance = Vector3.Distance(_player.transform.position, _pets[0].transform.position);
                if (distance > followDistance)
                {
                    for (int i = 0; i < _pets.Count; i++)
                    {
                        _pets[i].GetComponent<PetObject>().offset = new Vector3(0, 0, -petSpacing * (i + 1));
                    }
                }
            }
        }

        private void DeletePrefab(int index)
        {
            if(index < 0 || index >= _pets.Count)
            {
                return;
            }

            GameObject petToRemove = _pets[index];
            _pets.RemoveAt(index);
            Destroy(petToRemove);

            for(int i = index; i < _pets.Count; i++)
            {
                _pets[i].GetComponent<PetObject>().offset = new Vector3(0, 0, -petSpacing * (i + 1));
            }
        }
    }
}