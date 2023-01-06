using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoyakiContainer : MonoBehaviour
{
    [SerializeField] List<GameObject> takoyaki;
    int index = 0;

    void ShowTakoyaki() 
    {
        takoyaki[index-1].SetActive(true);
    }

    public void Add()
    {
        index++;
    }

    void HideTakoyaki()
    {
        for (int ith = 0; ith < takoyaki.Capacity; ith++)
        {
            takoyaki[ith].SetActive(false);
        }
        index = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.takoyakiDone == 0)
        {
            return;
        }
        else
        {
            if (index <= takoyaki.Capacity)
            {
                ShowTakoyaki();
            }
            else
            {
                HideTakoyaki();
            }
        }
    }
}
