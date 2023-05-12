using UnityEngine;

namespace Player
{
    public class KeyBoardManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager characterManager; // 获取角色控制管理器

        private bool _mouseLock = true; // 是否启用鼠标视角控制

        private float
            _moveHorizontal,
            _moveVertical;

        private float
            _viewHorizontal,
            _viewVertical;

        [SerializeField] private KeyCode
            forward = KeyCode.W,
            back = KeyCode.S,
            left = KeyCode.A,
            right = KeyCode.D,
            jump = KeyCode.Space,
            mouseLockIn = KeyCode.I;

        private void Start()
        {
            characterManager = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            //TODO: 要处理触控输入冲突
            if (Input.anyKey)
            {
                KeyBoardMove();
                JumpKeyDown();
            }
            else
            {
                KeyBoardNoMove();
            }

            MouseLookState();

            if (_mouseLock)
            {
                MouseMove();
            }
        }

        // 键盘输入监控
        private void KeyBoardMove()
        {
            if (Input.GetKey(forward) || Input.GetKey(back))
            {
                _moveVertical = Input.GetAxis($"Vertical");
            }
            else
            {
                _moveVertical = 0f;
            }

            if (Input.GetKey(left) || Input.GetKey(right))
            {
                _moveHorizontal = Input.GetAxis($"Horizontal");
            }
            else
            {
                _moveHorizontal = 0f;
            }

            characterManager.SetCharacterMove(_moveHorizontal, _moveVertical);
        }

        // 鼠标输入关闭时
        private void MouseLookState()
        {
            if (Input.GetKey(mouseLockIn))
            {
                if (_mouseLock == true)
                {
                    _mouseLock = false;

                    _viewHorizontal = 0f;
                    _viewVertical = 0f;

                    characterManager.SetCameraRotate(_viewHorizontal, _viewVertical);
                }
                else
                {
                    _mouseLock = true;
                }
            }
        }

        // 鼠标输入监控
        private void MouseMove()
        {
            // 当检测到鼠标右键按住时
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标指针

                _viewHorizontal = Input.GetAxis("Mouse X");
                _viewVertical = Input.GetAxis("Mouse Y");

                characterManager.SetCameraRotate(_viewHorizontal, _viewVertical);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None; // 解锁鼠标指针
                characterManager.SetCameraRotate(0, 0);
            }
        }

        // 键盘输入关闭时
        private void KeyBoardNoMove()
        {
            if (_moveHorizontal != 0 || _moveVertical != 0)
            {
                _moveHorizontal = 0f;
                _moveVertical = 0;

                characterManager.SetCharacterMove(_moveHorizontal, _moveVertical);
            }
        }

        // 按下跳跃键时
        // 未来拆分成 Skill 和 Input
        private void JumpKeyDown()
        {
            if (Input.GetKeyDown(jump) && characterManager.IsGround)
            {
                characterManager.IsJump = true;
            }
        }
    }
}