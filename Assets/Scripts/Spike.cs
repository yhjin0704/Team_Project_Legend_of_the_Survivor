using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Collider2D spikeCollider;

    private void Start()
    {
        spikeCollider = GetComponent<Collider2D>();
        spikeCollider.enabled = false; // �⺻ OFF
    }

    // �ִϸ��̼� �̺�Ʈ: �ݶ��̴� Ȱ��ȭ
    public void ActivateCollider()
    {
        spikeCollider.enabled = true;
    }

    // �ִϸ��̼� �̺�Ʈ: �ݶ��̴� ��Ȱ��ȭ
    public void DeactivateCollider()
    {
        spikeCollider.enabled = false;
    }

    // �浹 ���� (Player��)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player �±� Ȯ��
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                //player.Hit(); // �÷��̾��� Hit �Լ� ȣ��
            }
        }
    }
}
