using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EstusManager : MonoBehaviour
{
    [SerializeField] private GameObject estus;
    [SerializeField] private GameObject cinders;
    [SerializeField] private TextMeshProUGUI estusUsesText;
    [SerializeField] private TextMeshProUGUI cindersUsesText;

    [Header("UI_Link")]
    [SerializeField] private BoolValue estusUnlock;
    [SerializeField] private EstusValue estusUses;
    [SerializeField] private BoolValue cindersUnlock;
    [SerializeField] private EstusValue cindersUses;
    [SerializeField] private Signal estusSignal;
    [SerializeField] private Signal cindersSignal;

    void Start()
    {
        if (estusUnlock.currentValue)
        {
            estus.SetActive(true);
            estusUsesText.text = "" + estusUses.currentValue;
        }

        if (cindersUnlock.currentValue)
        {
            cinders.SetActive(true);
            cindersUsesText.text = "" + cindersUses.currentValue;
        }
    }

    void Update()
    {
        if (estusUnlock.currentValue)
        {
            estus.SetActive(true);
            if (Input.GetButtonDown("UseEstus") && estusUses.currentValue > 0)
            {
                estusSignal.Raise();
            }
            estusUsesText.text = "" + estusUses.currentValue;
        }
        else
        {
            estus.SetActive(false);
        }

        if (cindersUnlock.currentValue)
        {
            cinders.SetActive(true);
            if (Input.GetButtonDown("UseCinders") && cindersUses.currentValue > 0)
            {
                cindersSignal.Raise();
            }
            cindersUsesText.text = "" + cindersUses.currentValue;
        }
        else
        {
            cinders.SetActive(false);
        }
    }
}
