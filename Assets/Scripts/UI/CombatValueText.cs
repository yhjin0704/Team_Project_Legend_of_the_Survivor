using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatValueText : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f; // 위로 이동하는 속도
    [SerializeField] private float destroyTime = 0.5f; // 텍스트 삭제 시간
    private TextMeshProUGUI combatValue; //CombatValue 텍스트

    private void Awake()
    {
        combatValue = GetComponent<TextMeshProUGUI>(); // 텍스트 컴포넌트 가져오기
    }

    private void Start()
    {
        StartCoroutine(MoveUp()); // 위로 이동 코루틴 시작
    }

    public void SetDamageText(int damage)
    {
        combatValue.text = "-" + damage; // 대미지 숫자 표시
        combatValue.color = Color.white; // 대미지는 흰색
    }

    public void SetHealText(int heal)
    {
        combatValue.text = "+" + heal; // 회복 숫자 표시
        combatValue.color = Color.green; // 회복은 초록색
    }

    IEnumerator MoveUp()//위로 이동 코루틴
    {
        float elapsedTime = 0f;

        while (elapsedTime < destroyTime)
        {
            transform.position += (Vector3.up * moveSpeed * Time.deltaTime); // 위로 이동

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // 시간이 지나면 제거
    }
}
