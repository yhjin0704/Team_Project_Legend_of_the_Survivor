using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActorUI : MonoBehaviour
{
    //hp �Ǹ���, ���� �ؽ�Ʈ ������, �ؽ�Ʈ ���� ����Ʈ
    private Slider hpSlider;
    [SerializeField]private TextMeshProUGUI currentHPText;
    [SerializeField] private GameObject combatValueTextPrefab;
    [SerializeField] private Transform combatValueSpawnPoint;


    private void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>(); // �ڽ� ������Ʈ���� �����̴��� ã�Ƽ� �Ҵ�)
    }

    public void ChangeHPBar(float currentHP, float maxHPP) // ü�¹� ����
    {
        hpSlider.value = currentHP / maxHPP;

    }

    public void ChangeCurrentHP(float currentHP)
    {
        currentHPText.text = ((int)currentHP).ToString();
    }

    public void ShowCombatValue(int damage, bool isDamage) // ���� �ؽ�Ʈ ���
    {
        GameObject combatValueText = Instantiate(combatValueTextPrefab, combatValueSpawnPoint.position, Quaternion.identity); // ���� �ؽ�Ʈ ����
        combatValueText.transform.SetParent(combatValueSpawnPoint.transform, false); // ���� �ؽ�Ʈ �θ� ����

        CombatValueText cvt = combatValueText.GetComponent<CombatValueText>(); // ���� �ؽ�Ʈ ������Ʈ ��������

        if (cvt != null) // ���� �ؽ�Ʈ�� ���� ���
        {
            if (isDamage) // �������� ���
            {
                cvt.SetDamageText(damage);
            }
            else // ȸ���� ���
            {
                cvt.SetHealText(damage);
            }
        }
    }
}
