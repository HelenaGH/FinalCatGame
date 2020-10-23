using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestClosed, chestOpen, coin;

    public void Start()
    {
        chestClosed.SetActive(true);
        chestOpen.SetActive(false);
        coin.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        chestClosed.SetActive(false);
        chestOpen.SetActive(true);

        if (chestOpen == true)
        {
            coin.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        chestClosed.SetActive(true);
        chestOpen.SetActive(false);
    }
}
