/*
 * 背包界面的按钮功能
 * 取消装备道具
 */

using Inventory.InventoryManager;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.ButtonFunction.MenuButton
{
    public class UnEquip : MonoBehaviour
    {
        // 按钮对象
        private Button _btn;
        
        // 背包UI界面
        private InventoryUIManager _inventoryUIManager;

        private void Start()
        {
            // 获取背包 UI 界面脚本
            _inventoryUIManager = transform.parent.parent.GetComponent<InventoryUIManager>();

            // 获取 Button 组件
            _btn = GetComponent<Button>();
            
            // 监听 OnClick 事件
            _btn.onClick.AddListener(OnClick);
        }

        // 点击事件
        private void OnClick()
        {
            // 点击后调用方法
            _inventoryUIManager.FuncUnEquip();
        }
    }
}