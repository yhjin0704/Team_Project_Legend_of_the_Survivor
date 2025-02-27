using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    public int damage = 10;          // 플레이어에게 줄 데미지
    private Coroutine repeatCoroutine;
    private bool isInside = false;
    private Collider2D player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInside && other.CompareTag("Player"))
        {
            isInside = true;
            player = other;
            repeatCoroutine = StartCoroutine(RepeatTrapAttack());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BaseController>().Hit(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isInside && other.CompareTag("Player"))
        {
            isInside = false;
            StopCoroutine(RepeatTrapAttack());
            repeatCoroutine = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BaseController>().Hit(damage);
        }
    }

    private IEnumerator RepeatTrapAttack()
    {
        while (isInside)
        {
            Debug.Log("Testing");
            player.gameObject.GetComponent<BaseController>().Hit(damage);
            yield return new WaitForSeconds(1f);
        }
    }
}
