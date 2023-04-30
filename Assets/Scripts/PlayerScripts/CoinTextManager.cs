using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI coinText;

    void Start()
    {
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        coinText.text = playerInventory.coins.ToString("0000");
    }
}
