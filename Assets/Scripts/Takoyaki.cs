using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Takoyaki : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler

{
    public CookingState currentState = 0;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject minusTime;
    [SerializeField] List<Texture2D> cookingTextures;
    private bool isPenaltized = false;
    private bool isSelectable = false;
    private bool isCooking = false;
    private float cook = 0;
    private float cookingSpeed = 1;
    private Vector3 startPos;
    private GameObject takoyakiGrey;
    private Timer timer;
    private RectTransform rectTransform;
    private RawImage currentTexture;
    private Animator animator;
    private CanvasGroup canvasGroup;

    public enum CookingState{ Raw, Rare, Cooked, OverCooked };

    private void Awake()
    {
        isSelectable = false;
        cook = 0;
        currentState = 0;
        animator = GetComponent<Animator>();
        currentTexture = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
        timer = FindObjectOfType<Timer>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    #region Pour Animation
    private void Start() => StartCoroutine(Pour());

    IEnumerator Pour()
    {
        animator.Play("Pour");
        yield return new WaitForSeconds(1);
        isCooking = true;
    }
    #endregion

    #region Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSelectable)
        {
            startPos = transform.position;
            takoyakiGrey = transform.parent.gameObject;
            transform.SetParent(GameObject.Find("Canvas").transform);
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSelectable) 
        {
            isCooking = false;
            eventData.pointerDrag.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y); 
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        canvasGroup.blocksRaycasts = true;
        ReturnOverCooked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Pin").GetComponent<RawImage>().enabled = true;
        Cursor.visible = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Find("Pin").GetComponent<RawImage>().enabled = false;
        Cursor.visible = true;
    }
    #endregion

    #region Penalty
    void MinusTime()
    {
        Instantiate(minusTime, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        timer.MinusTime();
    }

    public void ReturnOverCooked()
    {
        transform.SetParent(takoyakiGrey.transform);
        transform.position = startPos;
        isCooking = true;
    }

    void Smoke() =>  Instantiate(smoke, gameObject.transform.position + new Vector3(rectTransform.rect.size.x / 3, rectTransform.rect.size.y/2, 0), Quaternion.identity, gameObject.transform);
    #endregion

    #region Cooking
    public CookingState ReturnCookingState() => currentState;

    float RandomCookingSpeed()
    {
        cookingSpeed = Random.Range(0, 20);
        return cookingSpeed;
    }

    private float Cook() => cook += Time.deltaTime * RandomCookingSpeed();

    private void ChangeCookingState(float cook)
    {
        if(0 <= cook && cook <50)
        {
            currentState = CookingState.Raw;
            currentTexture.texture = cookingTextures[0];
        }
        else if(25 <= cook && cook < 100)
        {
            currentState = CookingState.Rare;
            currentTexture.texture = cookingTextures[1];
        }
        else if(50 <= cook && cook < 150)
        {
            currentState = CookingState.Cooked;
            currentTexture.texture = cookingTextures[2];
            isSelectable = true;
        }
        else
        {
            currentState = CookingState.OverCooked;
            currentTexture.texture = cookingTextures[3];
            isPenaltized = true;
            Smoke();
            MinusTime();
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if(isCooking && isPenaltized == false) { ChangeCookingState(Cook()); }
    }
}
