using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    private int damage = 3;          // 플레이어에게 줄 데미지
    private Coroutine repeatCoroutine;
    private bool isInside = false;
    private Collider2D player;

    private void OnTriggerEnter2D(Collider2D other)
   {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<BaseController>().Hit(damage);
        }
    }
}
