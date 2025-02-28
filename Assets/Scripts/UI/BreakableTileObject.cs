using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTileObject : MonoBehaviour
{
    public int health = 1;
    public Tilemap tilemap;         // 타일맵 참조
    public TileBase breakableTile;  // 부술 수 있는 타일 (애니메이션 타일)

    private void Start()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }
    }

    // 타일에 데미지를 주는 함수
    public void DamageTile(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);   // 맵 좌표 -> 타일 좌표 변환


        // 현재 위치의 타일이 지정된 부술 수 있는 타일인지 확인
        if (tilemap.GetTile(cellPosition) == breakableTile)
        {
            tilemap.SetTile(cellPosition, null);                       // 타일 제거 (이제 지나갈 수 있음)
        }
    }



    // 충돌 감지 (플레이어 공격 등에 반응)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))        // "PlayerAttack" 태그가 있는 오브젝트와 충돌 시
        {
            DamageTile(collision.transform.position);    // 공격 위치를 기준으로 타일 삭제
        }
    }
}
