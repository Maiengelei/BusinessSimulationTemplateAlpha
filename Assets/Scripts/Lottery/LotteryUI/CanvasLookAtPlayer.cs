/*
 * 让抽奖机的屏幕UI始终对准玩家
 */

using UnityEngine;

namespace Lottery.LotteryUI
{
    public class CanvasLookAtPlayer : MonoBehaviour
    {
        private Camera _cam;

        private void Awake()
        {
            // 获取主相机
            _cam = Camera.main;
        }

        private void LateUpdate()
        {
            // 获取摄像机和当前对象之间的方向
            Vector3 direction = _cam.transform.position - transform.position;
            
            // 创建一个新的四元数代表旋转，使得y轴朝向摄像机
            Quaternion rotation = Quaternion.LookRotation(direction);

            // 旋转当前对象以面向摄像机
            transform.rotation = rotation;
        }
    }
}
