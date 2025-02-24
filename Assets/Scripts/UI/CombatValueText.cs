using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatValueText : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f; // ���� �̵��ϴ� �ӵ�
    [SerializeField] private float destroyTime = 0.5f; // �ؽ�Ʈ ���� �ð�
    private TextMeshProUGUI combatValue; //CombatValue �ؽ�Ʈ

    private void Awake()
    {
        combatValue = GetComponent<TextMeshProUGUI>(); // �ؽ�Ʈ ������Ʈ ��������
    }

    private void Start()
    {
        StartCoroutine(MoveUp()); // ���� �̵� �ڷ�ƾ ����
    }

    public void SetDamageText(int damage)
    {
        combatValue.text = "-" + damage; // ����� ���� ǥ��
        combatValue.color = Color.white; // ������� ���
    }

    public void SetHealText(int heal)
    {
        combatValue.text = "+" + heal; // ȸ�� ���� ǥ��
        combatValue.color = Color.green; // ȸ���� �ʷϻ�
    }

    IEnumerator MoveUp()//���� �̵� �ڷ�ƾ
    {
        float elapsedTime = 0f;

        while (elapsedTime < destroyTime)
        {
            transform.position += (Vector3.up * moveSpeed * Time.deltaTime); // ���� �̵�

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // �ð��� ������ ����
    }
}
