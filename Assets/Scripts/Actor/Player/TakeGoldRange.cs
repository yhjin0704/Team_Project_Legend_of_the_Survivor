using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeGoldRange : MonoBehaviour
{
    Player player;
    private Collider2D col;

    public float Range = 1;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        col = GetComponent<Collider2D>();

        col.transform.localScale = new Vector2(Range, Range);
    }

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Coin"))
        {
            player.gold += 1;

            _collision.gameObject.GetComponent<Collider2D>().enabled = false;

            _collision.gameObject.GetComponent<Animator>().Play("Coin_Destroy");
        }
    }
}
