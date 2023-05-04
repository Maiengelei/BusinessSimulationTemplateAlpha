using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotData : MonoBehaviour
{
    /// <summary>
    /// 道具图标
    /// </summary>
    [SerializeField] public Image itemImage;

    /// <summary>
    /// 道具属性
    /// </summary>
    [SerializeField] public TextMeshProUGUI itemValue;

    /// <summary>
    /// 道具格子编号
    /// </summary>
    public int gridSlotConst;

    private void Awake()
    {
        // 为当前的格子编号 0开始 对应 itemList
        gridSlotConst = transform.GetSiblingIndex();
    }
}