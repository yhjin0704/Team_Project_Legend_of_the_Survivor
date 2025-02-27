using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemRange : MonoBehaviour
{
    Player player;
    PlayerController playerController;
    private Collider2D col;

    public float Range = 1;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        playerController = transform.parent.GetComponent<PlayerController>();

        col = GetComponent<Collider2D>();

        col.transform.localScale = new Vector2(Range, Range);
    }

    private void Start()
    {
    }
}
