using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pin : MonoBehaviour
{
    private RawImage rawImage;

    // Update is called once per frame
    private void Awake() { rawImage = GetComponent<RawImage>(); }

    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        gameObject.transform.position = new Vector3(mouseX, mouseY);

        if (Input.GetMouseButton(0)) { rawImage.enabled = true; }
        if (Input.GetMouseButtonUp(0)) { rawImage.enabled = false; }
    }
}
