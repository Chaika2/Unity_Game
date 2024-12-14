using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    private GameObject player;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 10)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
    }
}
