using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera= Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        Vector3 mouseToScreenPosition = mainCamera.ScreenToWorldPoint(new Vector3(mouseX, mouseY));
        gameObject.transform.position = mouseToScreenPosition;
    }
}
