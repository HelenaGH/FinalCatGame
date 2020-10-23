using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int number; //the number of hearts

    public Image[] hearts; // an array
    public Sprite fullHeart;

    void Update()
    {
        health = GetComponent<PlayerMovement>().health;

        if (health > number)
        {
            health = number;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }

            if (i < number)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

}
