using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScripts : MonoBehaviour
{
    public int a = 0;

    public static GameScripts _instance;

    public hideScript hide;

    public static GameScripts Instance
    {
        get
        {
            return _instance;
        }
    }

    public int level = 1; //初始化关卡
    public int food = 100;//食物数量
    public AudioClip dieClip;
    // Start is called before the first frame update

    [HideInInspector]public List<EnemyScripts> enemyList = new List<EnemyScripts>();
    [HideInInspector]public bool isEnd = false;//是否到达终点
    private bool sleepStep = true;
    private Text foodText;
    private Text failText;
    private Image dayImage;
    private Text dayText;
    private Text diamondText;
    private playerScript player;
    private MapManager mapManager;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        
    }
   

    public void InitGame()
    {
        //初始化地图
        mapManager = GetComponent<MapManager>();
        mapManager.wallVectorList.Clear();//清空上一关卡中障碍物数组
        mapManager.diamondVector2.Clear();//清空上一关卡中钻石数组
        mapManager.InitMap();
 
        //初始化UI
        foodText = GameObject.Find("FoodText").GetComponent<Text>();
        UpdateFoodText(0);
        failText = GameObject.Find("FailText").GetComponent<Text>();
        failText.enabled = false;
        player= GameObject.Find("Player").GetComponent<playerScript>();
        dayImage = GameObject.Find("DayImage").GetComponent<Image>();
        dayText = GameObject.Find("DayText").GetComponent<Text>();
        dayText.text = "Day  " + level;
        diamondText = GameObject.Find("DiamondText").GetComponent<Text>();
        diamondText.text = "Diamond"+" "+System.Convert.ToString(mapManager.diamondVector2.Count)+"/";
        Invoke("HideBlack", 1);
        //初始化参数
        isEnd = false;
        enemyList.Clear();
    }

    void UpdateFoodText(int foodChange)
    {
        if (foodChange == 0)
        {
            foodText.text = "Food:" + food;
        }
        else
        {
            string str = "";
            if (foodChange < 0)
            {
                str = foodChange.ToString();
            }
            else
            {
                str = "+" + foodChange;
            }
            foodText.text =str +"  "+ "Food:" + food;
            //foodText.text ="Food:" + food;
        }
        
    }

    // Update is called once per frame
    public  void AddFood(int count)
    {
        food += count;
        UpdateFoodText(count);
    }

    public  void ReduceFood(int count)
    {
        food -= count;
        UpdateFoodText(-count);
        if (food <= 0)
        {
            failText.enabled = true;
            AudioManager.Instance.StopBgMusic();
            AudioManager.Instance.RandomPlay(dieClip);
            //SceneManager.LoadScene("Start");
            //level = 0;
            //Destroy(this.gameObject);
            Invoke("Death", 3);
        }
    }

    public void OnPlayerMove()
    {
        if (sleepStep == true)
        {
            sleepStep = false;
        }
        else
        {
            foreach (var enemyScripts in enemyList)
            {
                enemyScripts.Move();
            }
            sleepStep = true;
        }
        //检测有没有到达终点
        if ((player.targetPos.x == mapManager.cols - 2 && player.targetPos.y == mapManager.rows - 2)&& player.DiamondCountGet==mapManager.diamondVector2.Count)
        {
            isEnd = true;

            //加载下一个关卡
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    private void OnLevelWasLoaded(int sceneLevel)
    {
        a++;
        level++;
        InitGame();//初始化游戏

    }

    void HideBlack()
    {
        dayImage.gameObject.SetActive(false);
    }

    public void Death()
    {
        SceneManager.LoadScene("Start");
        level = 0;
        Destroy(this.gameObject);
    }
}
