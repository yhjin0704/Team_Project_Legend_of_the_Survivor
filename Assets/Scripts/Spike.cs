using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Collider2D spikeCollider;

    private void Start()
    {
        spikeCollider = GetComponent<Collider2D>();
        spikeCollider.enabled = false; // 기본 OFF
    }

    // 애니메이션 이벤트: 콜라이더 활성화
    public void ActivateCollider()
    {
        spikeCollider.enabled = true;
    }

    // 애니메이션 이벤트: 콜라이더 비활성화
    public void DeactivateCollider()
    {
        spikeCollider.enabled = false;
    }

    // 충돌 감지 (Player만)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player 태그 확인
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                //player.Hit(); // 플레이어의 Hit 함수 호출
            }
        }
    }
}
