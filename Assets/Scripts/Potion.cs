using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float healValue = 5.0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LateUpdate()
    {
        Vector3Int cellPos = GameManager.Instance.ChangeToCellPosition(spriteRenderer.transform.position);
        spriteRenderer.sortingOrder = -(int)cellPos.y;
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("PlayerRange"))
        {
            _collision.GetComponentInParent<PlayerController>().Healed(healValue);

            Destroy(gameObject);
        }
    }
}
