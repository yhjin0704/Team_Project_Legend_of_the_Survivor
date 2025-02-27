using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public GameObject[] potionPrefabs; // ���� ���� ���� ������ �迭
    public int potionCount;      // ������ ���� ����
    public float dropChance; // ������ ���� Ȯ��
    public int health;
    private int currentHealth;



    public void Start()
    {
        currentHealth = health;
    }

    public void DestroyPot()
    {
        // ���� ����
        for (int i = 0; i < potionCount; i++)
        {
            if (Random.value <= dropChance) // dropChance Ȯ���� ���� ����
            {
                GameObject randomPotion = potionPrefabs[Random.Range(0, potionPrefabs.Length)];
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * 0.5f;
                Instantiate(randomPotion, spawnPosition, Quaternion.identity);
            }
        }

        // �׾Ƹ� ������Ʈ ����
        Destroy(gameObject);
    }
}
