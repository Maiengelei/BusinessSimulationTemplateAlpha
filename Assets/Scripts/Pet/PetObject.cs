using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pet
{
    public class PetObject : MonoBehaviour
    {
        // 宠物需要追踪的目标
        public Transform player;
        // 追踪玩家的速度
        public float speed = 2.0f;
        // 与其他宠物保持的距离
        public float distanceFromOthers = 1.0f;
        
        // 与玩家保持的最小距离
        public float minimumDistanceFromPlayer = 1.5f;
        
        // 在地面以上的最小高度
        public float minimumHeightAboveGround = 0.5f;

        // 宠物管理器
        public PetManager petManager;

        private void Awake()
        {
            petManager = GameObject.Find("PetManager").GetComponent<PetManager>();
        }

        void Update()
        {
            // 如果玩家存在
            if (player != null)
            {
                // 让宠物朝向玩家
                transform.LookAt(player);

                // 使宠物向玩家移动
                transform.position += transform.forward * (speed * Time.deltaTime);
                
                // 检查与玩家的距离，如果太近就后退一点
                float distanceFromPlayer = Vector3.Distance(transform.position, player.position);
                if (distanceFromPlayer < minimumDistanceFromPlayer)
                {
                    transform.position -= transform.forward * (minimumDistanceFromPlayer - distanceFromPlayer);
                }

                // 遍历所有的宠物，保持距离
                foreach (PetObject otherPet in petManager.pets)
                {
                    // 确保不与自己进行比较
                    if (otherPet != this)
                    {
                        float distance = Vector3.Distance(transform.position, otherPet.transform.position);
                        // 如果距离太近
                        if (distance < distanceFromOthers)
                        {
                            // 从其他宠物方向向后移动
                            transform.position -= (otherPet.transform.position - transform.position).normalized * (distanceFromOthers - distance);
                        }
                    }
                }
                
                // 发射一个从宠物向下的射线
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit))
                {
                    // 如果射线碰到了地面，且宠物太接近地面
                    if (hit.distance < minimumHeightAboveGround)
                    {
                        // 将宠物向上移动一些距离，使其离地面有一定的高度
                        transform.position += transform.up * (minimumHeightAboveGround - hit.distance);
                    }
                }
            }
        }
    }
}