using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    private float HP = 100f;
    public Image Bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollsionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            HP -= 5;
            Bar.fillAmount = HP/100;
        }
    }
}
