using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActorUI : MonoBehaviour
{
    //hp 실린더, 전투 텍스트 프리팹, 텍스트 스폰 포인트
    private Slider hpSlider;
    [SerializeField]private TextMeshProUGUI currentHPText;
    [SerializeField] private GameObject combatValueTextPrefab;
    [SerializeField] private Transform combatValueSpawnPoint;


    private void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>(); // 자식 오브젝트에서 슬라이더를 찾아서 할당)
    }

    public void ChangeHPBar(float currentHP, float maxHPP) // 체력바 갱신
    {
        hpSlider.value = currentHP / maxHPP;

    }

    public void ChangeCurrentHP(float currentHP)
    {
        currentHPText.text = ((int)currentHP).ToString();
    }

    public void ShowCombatValue(int damage, bool isDamage) // 전투 텍스트 출력
    {
        GameObject combatValueText = Instantiate(combatValueTextPrefab, combatValueSpawnPoint.position, Quaternion.identity); // 전투 텍스트 생성
        combatValueText.transform.SetParent(combatValueSpawnPoint.transform, false); // 전투 텍스트 부모 설정

        CombatValueText cvt = combatValueText.GetComponent<CombatValueText>(); // 전투 텍스트 컴포넌트 가져오기

        if (cvt != null) // 전투 텍스트가 있을 경우
        {
            if (isDamage) // 데미지일 경우
            {
                cvt.SetDamageText(damage);
            }
            else // 회복일 경우
            {
                cvt.SetHealText(damage);
            }
        }
    }
}
