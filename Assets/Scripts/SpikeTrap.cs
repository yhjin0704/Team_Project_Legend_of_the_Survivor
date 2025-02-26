using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Animator animator;        // ������ũ �ִϸ��̼� �����ϱ� ���� �ִϸ�����
    public bool isActive = false;    // ������ũ�� Ȱ��ȭ�Ǿ����� ����(true: �ھƳ�, false: ������)
    public int damage = 10;          // �÷��̾�� �� ������

    private void Start()
    {
        animator = GetComponent<Animator>();                // �ִϸ����� ��������
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))         // ������ũ�� Ȱ�� ������ ���� �÷��̾�� ������
        {
            Player player = other.GetComponent<Player>();   // �÷��̾� ü�� ������Ʈ ��������
            if (player != null)
            {
                //player.TakeDamage(damage);                            // �÷��̾�� ������ �ֱ�
                
            }
        }
    }

    // �ִϸ��̼� �̺�Ʈ�� ���� ������ũ�� �ö�� �� ȣ���
    public void ActivateSpike()
    {
        isActive = true;
    }

    // �ִϸ��̼� �̺�Ʈ�� ���� ������ũ�� ������ �� ȣ���
    public void DeactivateSpike()
    {
        isActive = false;
    }
}
