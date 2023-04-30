using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [Header("UI to Change")]
    [SerializeField] private TextMeshProUGUI itemUsesText;
    [SerializeField] private Image itemImage;
    public Image selectedMark;
    [Header("Item variables")]
    public InventoryItem item;
    [SerializeField] private InventoryManager iManag;

    public void SelectItem()
    {
        iManag.UpdateItemInformation(item);
        UpdateUI();
        iManag.UnMarkItems();
        selectedMark.gameObject.SetActive(true);
    }

    void Start()
    {
        if (item.itemObtained.currentValue)
        {
            itemImage.sprite = item.itemSprite;
            if (item.unique)
            {
                itemUsesText.text = "";
            }
            else
            {
                itemUsesText.text = "" + item.currentUses.currentValue;
            }
        }
        else
        {
            itemImage.sprite = item.noItemSprite;
            itemUsesText.text = "";
        }
    }

    void Update()
    {
        if (item.itemObtained.currentValue)
        {
            itemImage.sprite = item.itemSprite;
            if (item.unique)
            {
                itemUsesText.text = "";
            }
            else
            {
                itemUsesText.text = "" + item.currentUses.currentValue;
            }
        }
    }

    public void UpdateUI()
    {
        if (item.itemObtained.currentValue)
        {
            if (item.unique)
            {
                itemUsesText.text = "";
            }
            else
            {
                itemUsesText.text = "" + item.currentUses.currentValue;
            }
        }
        else
        {
            itemImage.sprite = item.noItemSprite;
            itemUsesText.text = "";
        }
    }
}
