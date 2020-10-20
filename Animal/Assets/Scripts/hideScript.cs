using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideScript : MonoBehaviour
{
    public GameObject exitPanel;
    //private SpriteRenderer diamondSpriteRenderer;
    private GameObject[] diamond;
    private SpriteRenderer animalsSpriteRender;
    //private playerScript playerScript;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        diamond = GameObject.FindGameObjectsWithTag("Diamond");
        animalsSpriteRender = GameObject.FindGameObjectWithTag("Animal").gameObject.GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;
        if (time < 2)
        {
            for (int i = 0; i < diamond.Length; i++)
            {
                diamond[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            //try
            //{
            //    diamondSpriteRenderer.sortingOrder = 1;
            //}
            //catch
            //{

            //}

            try
            {
                animalsSpriteRender.sortingOrder = 1;
            }
            catch
            {

            }

        }

        else
        {
            for (int i = 0; i < diamond.Length; i++)
            {
                diamond[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
            //try
            //{
            //    diamondSpriteRenderer.sortingOrder = -1;

            //}
            //catch
            //{

            //}
            try
            {
                animalsSpriteRender.sortingOrder = -1;
            }
            catch
            {

            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }   
}
