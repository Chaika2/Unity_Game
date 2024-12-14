using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Sounds
{
    private void OnTriggerEnter2D(Collider2D collusion)
    {
        if (collusion.CompareTag("Player"))
        {
            MoneyText.Coin += 1; 
            PlaySound(sounds[0]);
            gameObject.GetComponent<Renderer>().enabled = false;
            Invoke("DestroyCoin", 0.5f);
        }
    }
    void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
