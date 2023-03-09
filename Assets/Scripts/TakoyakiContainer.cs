using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TakoyakiContainer : MonoBehaviour, IDropHandler
{
    [SerializeField] private List<GameObject> takoyakiList;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject containerCanvas;
    private bool isServable = false;
    private int index = -1;

    void ShowTakoyaki(int index) => takoyakiList[index].SetActive(true);

    public void Serve()
    {
        if (isServable) 
        { 
            HideTakoyaki(); 
            index = -1;
            isServable = false;
            SpawnContainer();
        }
    }

    void SpawnContainer() => Instantiate(container,containerCanvas.transform);

    void AddIndex() => index++; 

    void HideTakoyaki()
    {
        for (int ith = 0; ith < takoyakiList.Capacity; ith++) { takoyakiList[ith].SetActive(false); }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Takoyaki takoyaki = eventData.pointerDrag.GetComponent<Takoyaki>();
        if(takoyaki.ReturnCookingState() == Takoyaki.CookingState.Cooked && isServable == false)
        {
            Destroy(eventData.pointerDrag);
            AddIndex();
            GameManager.Instance.Add();
        }
        else if(takoyaki.ReturnCookingState() == Takoyaki.CookingState.OverCooked)
        {
            takoyaki.ReturnOverCooked();
        }
        ShowTakoyaki(index);
        isServable = (index == 7) ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.takoyakiDone == 0) { return; }
    }
}
