using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SantasGiftsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject GiftPrefab;
    [SerializeField] private float timer;
    
    
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 1)
        {
            GameObject go = Instantiate(GiftPrefab, transform.position, Quaternion.identity);
            Destroy(go, 5);
            timer = 0;
        }
    }
}
