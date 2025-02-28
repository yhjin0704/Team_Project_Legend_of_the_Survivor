using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
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
            GetComponent<Collider2D>().enabled = false;

            GetComponent<Animator>().Play("Coin_Destroy");
        }
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
