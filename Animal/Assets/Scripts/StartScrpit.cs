using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScrpit : MonoBehaviour
{
    public GameObject XiaoTuBiao;
    private Vector2 MousePosition;
    public GameObject bu1;
    public GameObject bu2;
    private Animator AnPlayer;
    // Start is called before the first frame update
    void Start()
    {
        AnPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
           
            //AnPlayer.Play();
            AnPlayer.SetBool("isTrue", true);
        }
        //else
        //{
        //    AnPlayer.SetBool("isTrue",false);
        //}
        MousePosition = Input.mousePosition;
        if (MousePosition.y < bu1.transform.position.y && MousePosition.y > bu2.transform.position.y)
        {
            XiaoTuBiao.transform.position = new Vector2(XiaoTuBiao.transform.position.x, MousePosition.y);
        }

        //Mathf.Clamp(XiaoTuBiao.transform.position.y, -258, -119);
        //XiaoTuBiao.transform.position = MousePosition;
        //if (Input.GetMouseButton(0))
        //{
        //    Debug.Log("2222");

        //}
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        GameScripts.Instance.level = 0;   
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
