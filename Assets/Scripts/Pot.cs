using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public GameObject[] potionPrefabs; // 여러 개의 포션 프리팹 배열
    public int potionCount;      // 생성할 포션 개수
    public float dropChance; // 포션이 나올 확률
    public int health;
    private int currentHealth;



    public void Start()
    {
        currentHealth = health;
    }

    public void DestroyPot()
    {
        // 포션 생성
        for (int i = 0; i < potionCount; i++)
        {
            if (Random.value <= dropChance) // dropChance 확률로 포션 생성
            {
                GameObject randomPotion = potionPrefabs[Random.Range(0, potionPrefabs.Length)];
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * 0.5f;
                Instantiate(randomPotion, spawnPosition, Quaternion.identity);
            }
        }

        // 항아리 오브젝트 삭제
        Destroy(gameObject);
    }
}
