using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Takoyaki : MonoBehaviour
{
    public CookingState currentState = 0;
    [SerializeField] GameObject pin;
    [SerializeField] GameObject smoke;
    [SerializeField] List<Texture2D> cookingTextures;
    private bool isSelectable = false;
    private bool isCooking = false;
    private float cook = 0;
    private float cookingSpeed = 1;
    private RectTransform rectTransform;
    private RawImage currentTexture;
    private Animator animator;
    private Camera mainCamera;

    public enum CookingState{
        Raw,
        Rare,
        Cooked,
        OverCooked
    };

    private void Awake()
    {
        mainCamera = Camera.main;
        isSelectable = false;
        cook = 0;
        currentState = 0;
        animator = GetComponent<Animator>();
        currentTexture = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnMouseDrag()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        print(mouseX+ ", " + mouseY);
        Vector3 mouseToScreenPosition = mainCamera.ScreenToWorldPoint(new Vector3( mouseX, mouseY));
        print(mouseToScreenPosition);
        if (isSelectable) { gameObject.transform.position = mouseToScreenPosition; }
    }

    private void OnMouseOver()
    {
        print("Mouse Over");
        isSelectable = (currentState == CookingState.Cooked || currentState == CookingState.OverCooked) ? true : false;
        GameObject canvas = GameObject.Find("Canvas");
        GameObject pinPrefab = Instantiate(pin.gameObject, canvas.transform);
        if (isSelectable != true) { Destroy(pinPrefab); }
    }

    private void Start() => StartCoroutine(Pour());  

    IEnumerator Pour()
    {
        animator.Play("Pour");
        yield return new WaitForSeconds(1);
        isCooking = true;
    }

    float RandomCookingSpeed()
    {
        cookingSpeed = Random.Range(0,5);
        return cookingSpeed;
    }

    private float Cook() => cook += Time.deltaTime * RandomCookingSpeed(); 

    public CookingState GetCurrentState() => currentState; 

    void Smoke()
    {
        Instantiate(smoke, gameObject.transform.position + new Vector3(rectTransform.rect.size.x / 2, rectTransform.rect.size.y/2, 0), Quaternion.identity, gameObject.transform);
    }

    private void ChangeCookingState(float cook)
    {
        if(0 <= cook && cook <25)
        {
            currentState = CookingState.Raw;
            currentTexture.texture = cookingTextures[0];
        }
        else if(25 <= cook && cook < 50)
        {
            currentState = CookingState.Rare;
            currentTexture.texture = cookingTextures[1];
        }
        else if(50 <= cook && cook < 75)
        {
            currentState = CookingState.Cooked;
            currentTexture.texture = cookingTextures[2];
        }
        else
        {
            currentState = CookingState.OverCooked;
            currentTexture.texture = cookingTextures[3];
            Smoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooking) { ChangeCookingState(Cook()); }
    }
}
