using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelC : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.gameObject.SetActive(false);
            this.gameObject.GetComponentInChildren<Text>().text = null;
            Time.timeScale = 1;
        }
    }
}
