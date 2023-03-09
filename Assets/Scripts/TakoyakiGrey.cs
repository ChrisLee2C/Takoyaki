using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoyakiGrey : MonoBehaviour
{
    [SerializeField] GameObject takoyaki;
    [SerializeField] GameObject kettle;
    private RectTransform rectTransform;
    private GameObject kettlePrefab;
    private bool isEmpty = true;

    private void Awake() { rectTransform = GetComponent<RectTransform>(); }

    public void CreateTakoyaki()
    {
        if (isEmpty)
        {
            Instantiate(takoyaki, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            kettlePrefab = Instantiate(kettle, gameObject.transform.position + new Vector3(rectTransform.rect.size.x/2, rectTransform.rect.size.y,0), Quaternion.identity, gameObject.transform);
            Invoke("DestroyKettle", 3);
        }
        isEmpty = false;
    }

    void DestroyKettle() { Destroy(kettlePrefab.gameObject); }

    private void Update()
    {
        isEmpty = (transform.childCount == 0)? true:false;
    }
}
