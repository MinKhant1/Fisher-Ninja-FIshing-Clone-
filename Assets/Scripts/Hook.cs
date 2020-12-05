using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Hook : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Transform hookTransform;
    Collider2D col;

    int length;
    int strength=3;
    int fishCount;

    bool canMove = true;

    Tweener cameraTween;


    public List<Fish> hookedFishes;


    private void Awake()
    {
        mainCamera = Camera.main;
        col = GetComponent<Collider2D>();
        hookedFishes = new List<Fish>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Input.GetMouseButton(0))
        {
            Vector3 vector = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            position.x = vector.x;
            transform.position = position;
        }
    }


    public void StartFishing()
    {
        length = IdleManager.instance.length - 20;
        strength = IdleManager.instance.strength;
        fishCount = 0;


        float time = (-length) * 0.1f;

        cameraTween = mainCamera.transform.DOMoveY(length, 1 + time * 0.25f, false).OnUpdate(delegate
         {
             if (mainCamera.transform.position.y <= -11)
             {
                 transform.SetParent(mainCamera.transform);
             }
         }).OnComplete(delegate
         {
             col.enabled = true;
             cameraTween = mainCamera.transform.DOMoveY(0, time * 5, false).OnUpdate(delegate
             {
                 if (mainCamera.transform.position.y >= -25)
                 {
                     StopFishing();
                 }
             });


         });

        ScreenManager.instance.ChangeScreen(Screens.GAME);
       col.enabled = false;
        canMove = true;
        hookedFishes.Clear();
    }

    private void StopFishing()
    {
        canMove = false;
        cameraTween.Kill(false);


        cameraTween = mainCamera.transform.DOMoveY(0, 2, false).OnUpdate(
            delegate
            {
                if (mainCamera.transform.position.y >= -11)
                {
                    transform.SetParent(null);
                    transform.position = new Vector2(transform.position.x, -6);

                }
            }
            ).OnComplete(
            delegate
            {
                transform.position = Vector2.down * 6;
                col.enabled = true;
                int num = 0;

                for (int i = 0; i < hookedFishes.Count; i++)
                {
                    hookedFishes[i].transform.SetParent(null);
                    hookedFishes[i].ResetFish();
                    num += hookedFishes[i].Type.price;

                }
                IdleManager.instance.totalGain = num;
                //Clearing out the hook from fish
                //IdleManager totalgain=num
                //screenmanager end screen
                ScreenManager.instance.ChangeScreen(Screens.END);
            }
            );


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish") && fishCount != strength)
        {

            fishCount++;
            Fish compoenent = collision.GetComponent<Fish>();
            compoenent.Hooked();
            hookedFishes.Add(compoenent);

            collision.transform.SetParent(transform);
            collision.transform.position = hookTransform.position;
            collision.transform.rotation = hookTransform.rotation;
            collision.transform.localScale = Vector3.one;

            collision.transform.DOShakeRotation(5, Vector3.forward * 45, 10, 90, false).SetLoops(1, LoopType.Yoyo).OnComplete(delegate
             {
                 collision.transform.rotation = Quaternion.identity;
             });

            if (fishCount == strength)
            {
                StopFishing();
            }



        }
    }
}
