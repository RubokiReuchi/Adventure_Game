using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
        UpdateHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.currentValue; i++)
        {
            if (i < hearts.Length)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = fullHeart;
            }
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.currentValue / 2;

        for (int i = 0; i < heartContainers.currentValue; i++)
        {
            if (i <= tempHealth - 1)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                hearts[i].sprite = halfHeart;
            }
        }
    }

    public void AddContainer()
    {
        hearts[(int)heartContainers.currentValue - 1].gameObject.SetActive(true);
        hearts[(int)heartContainers.currentValue - 1].sprite = fullHeart;
    }
}
