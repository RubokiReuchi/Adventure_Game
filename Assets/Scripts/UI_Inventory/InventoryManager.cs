using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("InventoryInfo")]
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject scrollContent;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public Signal UI_Signal;
    public InventoryItem currentItem;
    public InventoryItem estus;
    public InventoryItem cinders;

    public void UpdateItemInformation(InventoryItem item)
    {
        if (item.itemObtained.currentValue)
        {
            currentItem = item;
            nameText.text = item.itemName;
            descriptionText.text = item.itemDescription;
            
            if (currentItem.usable && currentItem.currentUses.currentValue > 0)
            {
                useButton.SetActive(true);
            }
            else
            {
                useButton.SetActive(false);
            }
        }
        else
        {
            currentItem = item;
            nameText.text = "Unknown";
            descriptionText.text = "Unknown";
            useButton.SetActive(false);
        }
    }

    public void CleanItemInformation()
    {
        foreach (Transform child in scrollContent.transform)
        {
            child.GetComponent<InventorySlot>().UpdateUI();
        }
        SetInfoNull();
    }

    public void SetInfoNull()
    {
        currentItem = null;
        nameText.text = "";
        descriptionText.text = "";
        useButton.SetActive(false);
        UnMarkItems();
    }

    void Start()
    {
        SetInfoNull();
    }

    public void UseButton()
    {
        if (currentItem != null)
        {
            if (currentItem.itemObtained.currentValue && currentItem.currentUses.currentValue > 0)
            {
                currentItem.Use();
                currentItem.currentUses.currentValue--;
                UI_Signal.Raise();
                if (currentItem.currentUses.currentValue <= 0)
                {
                    useButton.SetActive(false);
                }
            }
        }
    }

    public void UseEstus()
    {
        currentItem = estus;
        if (currentItem.itemObtained.currentValue && currentItem.currentUses.currentValue > 0)
        {
            currentItem.Use();
            currentItem.currentUses.currentValue--;
            UI_Signal.Raise();
        }
    }

    public void UseCinders()
    {
        currentItem = cinders;
        if (currentItem.itemObtained.currentValue && currentItem.currentUses.currentValue > 0)
        {
            currentItem.Use();
            currentItem.currentUses.currentValue--;
            UI_Signal.Raise();
        }
    }

    public void UnMarkItems()
    {
        foreach (Transform child in scrollContent.transform)
        {
            child.GetComponent<InventorySlot>().selectedMark.gameObject.SetActive(false);
        }
    }
}
