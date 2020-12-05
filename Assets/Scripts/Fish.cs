using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fish : MonoBehaviour
{


    [SerializeField] Fish.FishType type;
    private CircleCollider2D col;
    private SpriteRenderer rend;
    private float screenLeft;
    private Tweener tweener;


    public Fish.FishType Type
    {
        get
        {
            return Type;
        }
        set
        {
            type = value;
            col.radius = type.colliderRadius;
            rend.sprite = type.sprite;
        }
    }

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
        screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        ResetFish();
    }


    public void ResetFish()
    {
        if (tweener != null)
        {
            tweener.Kill(false);
        }



        float num = UnityEngine.Random.Range(type.minLength, type.maxLength);
        col.enabled = true;

        Vector3 position = transform.position;
        position.x = screenLeft;
        position.y = num;
        transform.position = position;

        float num2 = 1;
        float y = UnityEngine.Random.Range(num - num2, num + num2);
        Vector2 v = new Vector2(-position.x,y);

        float num3 = 3;
        float delay = UnityEngine.Random.Range(0, 2 * num3);

        tweener = transform.DOMove(v, num3, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(delay).OnStepComplete(delegate
             {
                 Vector3 localScale = transform.localScale;
                 localScale.x = -localScale.x;
                 transform.localScale = localScale;
             });



    }
    public void Hooked()
    {
        col.enabled = false;
        tweener.Kill(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    public class FishType
    {
        public int price;
        public float fishCount;
        public float minLength;
        public float maxLength;
        public float colliderRadius;
        public Sprite sprite;

    }
}
