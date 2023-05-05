using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        // 参数配置区
        [SerializeField, Header("移动配置相关")] private float moveSpeed = 5; // 移动速度
        [SerializeField] private float jumpSpeed = 5; // 跳跃高度
        [SerializeField] private float jumpHeight = 5; // 跳跃高度
        [SerializeField] private Transform groundCheck; // 地面检测对象
        [SerializeField] private float groundHitDistance = 0.2f; // 地面检测距离
        [SerializeField] private LayerMask groundLayerMask; // 地面检测层级
        [SerializeField, Header("相机配置相关")] private Camera cam; // 相机
        [SerializeField] private Transform cameraFollow; // 跟随的目标物体
        [SerializeField] private float maxDistance = 5.0f; // 摄像机最远距离
        [SerializeField] private float minDistance; // 摄像机最近距离
        [SerializeField] private float maxRotate = 80f; // 视角最高角度
        [SerializeField] private float minRotate = -40f; // 视角最低角度
        [SerializeField] private float rotateSensitivity = 150.0f; // 旋转灵敏度
        [SerializeField, Range(0f, 1f)] private float smoothSpeed = 0.125f; // 相机移动的平滑速度
        [SerializeField] private LayerMask hitLayerMask; // 摄像机碰撞检测层级

        // 私有组件
        private CharacterController _character; // 角色控制器

        // 移动相关
        private Vector3 _moveDirection; // 移动方向向量
        private Vector3 _moveDir; // 最终移动方向
        private Vector3 _heightDirection; // 高度值向量
        private readonly float _gravity = -9.81f; // 重力
        private bool _isJump; // 是否在跳跃状态
        private bool _isGround; // 是否接地
        private readonly float _turnSmoothTime = 0.1f; // 角色转身所需近似时间，该值越小，速度越快
        private float _targetAngle; // 角色转身角度
        private float _angle; // 角色平滑度
        private float _turnSmoothVelocity; // 角色转身当前速度，Mathf.SmoothDampAngle 的第三个参数所需，该值在运行中会实时修改

        // 相机相关
        private Vector2 _viewRotation; // 相机输入轴
        private float _yaw; // 绕Y轴的旋转角度 
        private float _pitch; // 绕X轴的旋转角度
        private RaycastHit _hit; // 射线检测
        private float _outDistance; // 最终输出的相机距离


        // ---------------------------- 访问 ----------------------------
        public bool IsJump
        {
            get => _isJump;
            set => _isJump = value;
        }

        public bool IsGround => _isGround;

        // ---------------------------- 方法 ----------------------------
        private void Start()
        {
            _character = GetComponent<CharacterController>();

            _outDistance = maxDistance;
        }

        private void Update()
        {
            CheckGround();
            CharacterMove();
        }

        private void LateUpdate()
        {
            CameraMove();
        }

        private void CheckGround()
        {
            // 检查是否有命中特定的层级
            _isGround = Physics.Raycast(groundCheck.transform.position, -groundCheck.transform.up, groundHitDistance,
                groundLayerMask);

            // 当角色在地面上时，给高度 -2f ，防止角色在地上弹起导致地面射线判定失效或者物理引擎击飞
            if (_isGround && _heightDirection.y < 0)
            {
                _heightDirection.y = -2f;
            }
        }

        // 移动角色
        private void CharacterMove()
        {
            // 死区控制
            if (_moveDirection.magnitude >= 0.1f)
            {
                // 计算转身角度
                _targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg +
                               cam.transform.eulerAngles.y;
                // 转身平滑
                _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity,
                    _turnSmoothTime);
                // 转身
                transform.rotation = Quaternion.Euler(0f, _angle, 0f);

                // 计算角色朝向
                _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;

                // 角色移动
                _character.Move(_moveDir.normalized * (moveSpeed * Time.deltaTime));
            }

            // 当触发跳跃时
            if (_isJump && _isGround)
            {
                _isJump = false;
                _heightDirection.y += Mathf.Sqrt(jumpHeight * -3.0f * _gravity);
            }

            // 计算玩家跳跃
            _heightDirection.y += _gravity * jumpSpeed * Time.deltaTime;

            // 角色移动
            _character.Move(_heightDirection * Time.deltaTime);
        }

        private void CameraMove()
        {
            // 计算相机绕Y轴和X轴的旋转角度
            _yaw += _viewRotation.x;
            _pitch -= _viewRotation.y;
            _pitch = Mathf.Clamp(_pitch, minRotate, maxRotate); // 限制旋转角度

            // 进行碰撞检测，确定摄像机距离
            if (Physics.Raycast(cameraFollow.position, cam.transform.position - cameraFollow.position, out _hit,
                    maxDistance, hitLayerMask))
            {
                // 当 hit 命中时
                _outDistance = maxDistance - (maxDistance - _hit.distance);
                // 当摄像机距离小于最小距离时
                if (_outDistance < minDistance)
                {
                    _outDistance = minDistance;
                }
            }

            // 如果射线没有命中任何一个对象
            if (_hit.collider == null)
            {
                _outDistance = maxDistance;
            }

            // 计算相机的目标位置
            Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0.0f);
            Vector3 targetPosition = cameraFollow.position + rotation * Vector3.back * _outDistance;

            // 平滑移动相机
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, smoothSpeed);
            cam.transform.LookAt(cameraFollow); // 朝向目标物体
        }

        // -------------------------------- Public --------------------------------

        public void SetCharacterMove(float horizontal, float vertical)
        {
            _moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        }

        public void SetCameraRotate(float horizontal, float vertical)
        {
            _viewRotation = new Vector2(horizontal, vertical) * rotateSensitivity;
        }
    }
}