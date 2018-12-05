using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
   
    void Start()
    {
        var canvasObject = GameObject.FindGameObjectsWithTag("Canvas");
        int numCanvas = canvasObject.Length;
        if(numCanvas != 1)
        {
            Destroy(canvasObject[1].gameObject);
        } else
        {

            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}
