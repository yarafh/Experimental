using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    public int money = 0;
    public AudioSource coinSound;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            money++;
            coinSound.Play();
            Destroy(other.gameObject);
            Debug.Log("I have this much money: "+money);
        }
    }
}

