using System;
using System.Collections.Generic;
using Inventory.InventoryScriptableObject;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pet
{
    public class PetManager : MonoBehaviour
    {
        // 玩家的引用
        private Transform _player;
        // 玩家背包类
        private InventoryObject _inventoryObject;
        // 所有宠物的引用列表
        public List<PetObject> pets = new List<PetObject>();

        private void OnEnable()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _inventoryObject = _player.GetComponent<GamerObjectProperties>().InventoryObject;

            _inventoryObject.PetAddList += CreatePet;
            _inventoryObject.PetRemoveList += DeletePet;
        }

        private void OnDestroy()
        {
            _inventoryObject.PetAddList -= CreatePet;
            _inventoryObject.PetRemoveList -= DeletePet;
        }

        private void Start()
        {
            // 当已经有装备物品且宠物列表为空
            if (_inventoryObject.isEquippedOnValue != 0 && pets.Count == 0)
            {
                for (int i = 0; i < _inventoryObject.isEquippedOnValue; i++)
                {
                    CreatePet(_inventoryObject.itemList[i].Item.itemModelPrefab);
                }
            }
        }

        // 创建宠物实例
        public void CreatePet(GameObject petPrefab)
        {
            // 创建一个新的宠物实例
            GameObject petInstance = Instantiate(petPrefab);
            // 防止与玩家重合，设置宠物的位置为玩家位置加一个随机偏移
            petInstance.transform.position = _player.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            // 获取宠物组件并设置宠物的目标为玩家
            PetObject pet = petInstance.GetComponent<PetObject>();
            pet.player = _player;
            // 将新的宠物添加到宠物列表中
            pets.Add(pet);
        }

        // 删除宠物实例
        public void DeletePet(int index)
        {
            // 确保下标有效
            if (index >= 0 && index < pets.Count)
            {
                // 获取要删除的宠物
                PetObject petToDelete = pets[index];
                // 从列表中移除这个宠物
                pets.RemoveAt(index);
                // 删除这个宠物对象
                Destroy(petToDelete.gameObject);

                // 进行补位，使得所有宠物保持合理的位置
                for (int i = index; i < pets.Count; i++)
                {
                    // 为了简单起见，这里仅仅是将每个宠物向前移动一个单位
                    // 在实际游戏中，可能需要更复杂的补位算法，例如寻找一个没有其他宠物的位置
                    pets[i].transform.position -= pets[i].transform.forward;
                }
            }
            else
            {
                Debug.LogError("Invalid index: " + index);
            }
        }
    }
}
