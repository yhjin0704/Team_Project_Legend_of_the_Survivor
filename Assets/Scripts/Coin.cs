using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
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
            _collision.GetComponentInParent<Player>().gold += 1;

            GameManager.Instance.UIManagerProperty.ChangeGold(_collision.GetComponentInParent<Player>().gold);

            GetComponent<Collider2D>().enabled = false;

            GetComponent<Animator>().Play("Coin_Destroy");
        }
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
