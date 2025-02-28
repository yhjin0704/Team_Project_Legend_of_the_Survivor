using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pot : MonoBehaviour
{
    public GameObject[] potionPrefabs; // ���� ���� ���� ������ �迭
    public int potionCount = 1;      // ������ ���� ����
    public float dropChance = 0.25f; // ������ ���� Ȯ��
    public int health;
    private int currentHealth;
    public Tilemap tilemap;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        tilemap = FindObjectOfType<Tilemap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        currentHealth = health;
    }

    public void LateUpdate()
    {
        Vector3Int cellPos = GameManager.Instance.ChangeToCellPosition(spriteRenderer.transform.position);
        spriteRenderer.sortingOrder = -(int)cellPos.y;
    }

    public void DestroyPot()
    {
        // ���� ����
        for (int i = 0; i < potionCount; i++)
        {
            int randomPotion = Random.Range(0, potionPrefabs.Length);
            if (Random.value <= dropChance) // dropChance Ȯ���� ���� ����
            {
                float vec;
                float vec2;
                do
                {
                    vec = Random.Range(-1f, 1f);
                    vec2 = Random.Range(-1f, 1f);
                }
                while (!GameManager.Instance.IsOnTilemap(transform.position + new Vector3(vec, vec2, 0)));
                Instantiate(potionPrefabs[randomPotion], transform.position + new Vector3(vec, vec2, 0), Quaternion.identity);
            }
        }

        // �׾Ƹ� ������Ʈ ����
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            DestroyPot();
        }
    }
}
