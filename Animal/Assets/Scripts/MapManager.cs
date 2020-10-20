using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    //public static MapManager _mapManager;

    //public static MapManager mapManager
    //{
    //    get { return _mapManager; }
    //}

    

    public GameObject[] outWallArray;
    public GameObject[] floorArray;
    public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject[] animalsArray;
    public GameObject exitPrefab;
    public Sprite diamondSprite;//钻石图片

    public GameObject DiamondGame;
    


    public int rows = 10;
    public int cols = 10;

    public int minCountWall = 2;
    public int maxCountWall = 8;

    private Transform mapHolder;
    private List<Vector2> positionList = new List<Vector2>();
    [HideInInspector]public List<Vector2> wallVectorList = new List<Vector2>();//创建障碍物位置的列表
    [HideInInspector]public List<Vector2> diamondVector2 = new List<Vector2>();//创建钻石位置的列表

    private GameScripts gameScripts;



    // Start is called before the first frame update
    private void Start()
    {
       
    }

    

    //初始化地图
    public void InitMap()
    {  
        gameScripts = this.GetComponent<GameScripts>();
        mapHolder = new GameObject("Map").transform;
        //创建外墙和地板
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1)
                {
                    int index =UnityEngine. Random.Range(0, outWallArray.Length);
                    //初始化外墙
                    GameObject go= GameObject.Instantiate(outWallArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
                else 
                {
                    int index = UnityEngine.Random.Range(0, floorArray.Length);
                    GameObject go=GameObject.Instantiate(floorArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
            }
        }
        positionList.Clear();
        for (int x=2; x < cols-2; x++)
        {
            for (int y = 2; y < rows-2; y++)
            {
                positionList.Add(new Vector2(x, y));
            }
        }
        //创建障碍物、食物和敌人
        //创建障碍物
        int wallCount = UnityEngine.Random.Range(minCountWall, maxCountWall);//障碍物的个数
        instantiateItem(wallCount, wallArray);
        getDiamondVector2();

        //输出钻石的位置
        //foreach (var item in diamondVector2)
        //{
        //    Debug.Log(item);
        //} 

        //输出障碍位置数组
        //foreach (var item in wallVectorList)
        //{
        //    Debug.Log(item);
        //}        



        //创建食物
        int foodCount = UnityEngine.Random.Range(2, gameScripts.level * 2);
        instantiateItem(foodCount, foodArray);       
        //创建敌人
        int enemyCount = gameScripts.level/2;
        instantiateItem(enemyCount, enemyArray);
        //创建出口
        (Instantiate(exitPrefab, new Vector2(cols-2, rows-2), Quaternion.identity)).transform.SetParent(mapHolder);

    }

    /// <summary>
    /// 随机位置的方法
    /// </summary>
    /// <returns>返回一个二维向量</returns>
    private Vector2 RandomPosition()
    {
        int positionIndex = UnityEngine.Random.Range(0, positionList.Count);
        Vector2 pos = positionList[positionIndex];
        positionList.RemoveAt(positionIndex);
        return pos;
        
    }

    /// <summary>
    /// 随机prefab
    /// </summary>
    /// <param name="prefabs">传递prefabs的数组</param>
    /// <returns>返回一个prefab</returns>
    private GameObject RandomPrefab(GameObject[] prefabs)
    {
        int index = UnityEngine.Random.Range(0, prefabs.Length);
        return prefabs[index];
    }

    /// <summary>
    /// 创建物体
    /// </summary>
    /// <param name="count">创建物体的数量</param>
    /// <param name="prefabs">创建的物体</param>
    private void instantiateItem(int count,GameObject[] prefabs)
    {
        for (int i = 0; i<count; i++)
        {
            Vector2 pos = RandomPosition();
            GameObject Prefab = RandomPrefab(prefabs);
            (Instantiate(Prefab, pos, Quaternion.identity)).transform.SetParent(mapHolder);

            
            
            //如果数组是障碍物数组，每实例化一个添加实例化的位置
            if (prefabs == wallArray)
            {
                wallVectorList.Add(pos);               
            }
            
        }
    }


    /// <summary>
    /// 获取钻石位置的方法，将获得位置组成方法
    /// </summary>
    private void getDiamondVector2()
    {
        
        int DiamondCount = wallVectorList.Count/2;
        for(int i = 0; i <= DiamondCount; i++)
        {
            int Diamond = UnityEngine.Random.Range(0, wallVectorList.Count - 1);
            diamondVector2.Add(wallVectorList[Diamond]);
            
            wallVectorList.RemoveAt(Diamond);
        }
        for (int i = 0; i < diamondVector2.Count; i++)
        {
            if(i==diamondVector2.Count-1)
            {
                Instantiate(RandomPrefab(animalsArray), diamondVector2[diamondVector2.Count-1], Quaternion.identity);
            }
            else
            {
                Instantiate(DiamondGame, diamondVector2[i], Quaternion.identity);
            }
        }                  
        //diamondText.text = Convert.ToString(diamondVector2.Count);
    }
}
