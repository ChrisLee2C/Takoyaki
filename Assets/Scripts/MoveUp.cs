using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MoveUp : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 1f;
    private RectTransform rect;

    private void Awake() => rect= GetComponent<RectTransform>();

    // Start is called before the first frame update
    void Start() => Invoke("Despawn", 1);

    void Despawn() => Destroy(gameObject);

    void MovingUp() =>  rect.Translate(new Vector3(0, Time.deltaTime * movingSpeed, 0));

    // Update is called once per frame
    void Update() => MovingUp();
}
