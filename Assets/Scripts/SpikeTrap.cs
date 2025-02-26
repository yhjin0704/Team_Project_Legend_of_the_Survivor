using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Animator animator;        // 스파이크 애니메이션 제어하기 위한 애니메이터
    public bool isActive = false;    // 스파이크가 활성화되었는지 여부(true: 솟아남, false: 내려감)
    public int damage = 10;          // 플레이어에게 줄 데미지

    private void Start()
    {
        animator = GetComponent<Animator>();                // 애니메이터 가져오기
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))         // 스파이크가 활성 상태일 때만 플레이어에게 데미지
        {
            Player player = other.GetComponent<Player>();   // 플레이어 체력 컴포넌트 가져오기
            if (player != null)
            {
                //player.TakeDamage(damage);                            // 플레이어에게 데미지 주기
                
            }
        }
    }

    // 애니메이션 이벤트를 통해 스파이크가 올라올 때 호출됨
    public void ActivateSpike()
    {
        isActive = true;
    }

    // 애니메이션 이벤트를 통해 스파이크가 내려갈 때 호출됨
    public void DeactivateSpike()
    {
        isActive = false;
    }
}
