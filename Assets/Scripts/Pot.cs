using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pot : MonoBehaviour
{
    public GameObject[] potionPrefabs; // 여러 개의 포션 프리팹 배열
    public int potionCount = 1;      // 생성할 포션 개수
    public float dropChance = 0.25f; // 포션이 나올 확률
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
        // 포션 생성
        for (int i = 0; i < potionCount; i++)
        {
            int randomPotion = Random.Range(0, potionPrefabs.Length);
            if (Random.value <= dropChance) // dropChance 확률로 포션 생성
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

        // 항아리 오브젝트 삭제
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
