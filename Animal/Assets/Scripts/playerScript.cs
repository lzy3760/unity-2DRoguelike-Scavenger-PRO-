using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    

    private Text animalText;
    private Text panelText;

    public float smoothing = 1;
    public float restTime = 1;
    public AudioClip chop1Audio;
    public AudioClip chop2Audio;
    public AudioClip stepAudio1;
    public AudioClip stepAudio2;

    public AudioClip soda1Audio;
    public AudioClip soda2Audio;
    public AudioClip fruit1Audio;
    public AudioClip fruit2Audio;

    public GameObject Panel; 

    private float restTimer = 0;
    private Rigidbody2D rigidbody;                               
    [HideInInspector]public Vector2 targetPos = new Vector2(1, 1);
    private BoxCollider2D boxCollider;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public Text DiamondCount;//钻石的总数量
    public int DiamondCountGet = 0;//定义钻石的获取数量

    void Start()
    {      
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DiamondCount = GameObject.Find("DiamondCountGet").GetComponent<Text>();
        animalText = GameObject.FindGameObjectWithTag("Animal").gameObject.GetComponent<Text>();
        panelText = GameObject.Find("animalsText").gameObject.GetComponent<Text>();
        Panel = GameObject.Find("animalsPanel").gameObject;
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

       
        //Debug.Log(targetPos);
        rigidbody.MovePosition(Vector2.Lerp(rigidbody.position, targetPos, smoothing * Time.deltaTime*2));

        if (GameScripts.Instance.food <= 0 || GameScripts.Instance.isEnd == true)
        {
            return;
        }

        restTimer += Time.deltaTime;
        if (restTimer<restTime)
        {
            return;           
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");       
        
        
        if (h > 0)
        {
            v = 0;
            spriteRenderer.flipX = false;
        }
        if (h < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (h != 0 || v != 0)
        {
            GameScripts.Instance.ReduceFood(1);

            boxCollider.enabled = false;
            RaycastHit2D hit2D= Physics2D.Linecast(targetPos,targetPos+new Vector2(h,v),layerMask:9);
            boxCollider.enabled = true;
            //targetPos += new Vector2(h, v);

            if (hit2D.collider==null)
            {               
                targetPos += new Vector2(h, v);
                AudioManager.Instance.RandomPlay(stepAudio1, stepAudio2);
            }
            //targetPos += new Vector2(h, v);
            else
            {
                //Debug.Log(hit2D.collider.gameObject.name);
                switch (hit2D.collider.tag)
                {
                    case "outWall":
                        break;
                    case "wall":
                        animator.SetTrigger("Attack");
                        AudioManager.Instance.RandomPlay(chop1Audio, chop2Audio);
                        hit2D.collider.SendMessage("TakeDamage");
                        break;
                    case "Food":
                        GameScripts._instance.AddFood(10);
                        targetPos += new Vector2(h, v);
                        AudioManager.Instance.RandomPlay(stepAudio1, stepAudio2);          
                        Destroy(hit2D.transform.gameObject);
                        AudioManager.Instance.RandomPlay(fruit1Audio, fruit2Audio);
                        break;
                    case "Soda":
                        GameScripts._instance.AddFood(20);
                        targetPos += new Vector2(h, v);
                        AudioManager.Instance.RandomPlay(stepAudio1, stepAudio2);
                        Destroy(hit2D.transform.gameObject);
                        AudioManager.Instance.RandomPlay(soda1Audio, soda2Audio);
                        break;
                    case "Enemy":
                        break;
                    case "Diamond":   
                        targetPos += new Vector2(h, v);
                        Destroy(hit2D.transform.gameObject);
                        DiamondCountGet++;                       
                        break;
                    case "Animal":
                        
                        
                        targetPos += new Vector2(h, v);              
                        panelText.text = animalText.text;
                        Panel.SetActive(true);
                        Destroy(hit2D.transform.gameObject);
                        DiamondCountGet++;
                        
                        break;
                }
            }
            if (Panel.activeInHierarchy==true)//当动物说明出现时
            {
                //targetPos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                //GameScripts._instance.AddFood(1);
                Time.timeScale = 0;
            }

            DiamondCount.text = "" + DiamondCountGet;
            GameScripts.Instance.OnPlayerMove();
            restTimer = 0;//不管攻击移动都需要休息
        }
        
        
    }
    public void TakeDamage(int lossFood)
    {
        GameScripts.Instance.ReduceFood(lossFood);
        animator.SetTrigger("Damage");
    } 
}
