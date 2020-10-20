using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScrpit : MonoBehaviour
{
    private int hp = 2;
    public MapManager mapManager;
    public GameObject diamond;
    private Vector2 wallVector;//声明当前障碍物位置
    public Vector2 WeiZhi;


    public Sprite wallSprite;
    public Sprite damageSprite;//墙体受损图片




    //自身受到攻击的时候

    private void Start()
    {
        wallVector = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
    }


    public void TakeDamage()
    {
        hp -= 1;
        GetComponent<SpriteRenderer>().sprite = damageSprite;
        if (hp <= 0)
        {
            
            Destroy(this.gameObject);
            //Spirte();
        }
    }

    //public void Spirte()
    //{
    //    if (mapManager.diamondVector2.Contains(wallVector))
    //    {
    //        Instantiate(diamond, this.gameObject.transform.position, Quaternion.identity);
    //        Debug.Log("diaoyong");
    //    }
    //}







}
