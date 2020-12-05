using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    [SerializeField] Fish fishPrefab;
    [SerializeField] Fish.FishType[] fishtypes;


    private void Awake()
    {
        for (int i = 0; i < fishtypes.Length; i++)
        {
            int num = 0;
            while (num < fishtypes[i].fishCount)
            {
                Fish fish = Object.Instantiate<Fish>(fishPrefab);
                fish.Type = fishtypes[i];
                fish.ResetFish();
                num++;
            }
        }
    }
}
