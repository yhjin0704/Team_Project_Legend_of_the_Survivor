using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float healValue = 5.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
