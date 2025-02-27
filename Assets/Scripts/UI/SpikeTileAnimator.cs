using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikeTileAnimator : MonoBehaviour
{
    public Tilemap tilemap; // 스파이크가 있는 타일맵
    public TileBase[] spikeTiles; // 애니메이션할 타일 배열 (예: 솟아나는 타일 이미지들)
    public Vector3Int tilePosition; // 특정 타일의 좌표
    public float frameRate = 0.5f; // 애니메이션 속도
    public bool isActive = false; // 스파이크가 활성 상태인지 여부

    private int index = 0;

    void Start()
    {
        StartCoroutine(AnimateTile());
    }

    IEnumerator AnimateTile()
    {
        while (true)
        {
            tilemap.SetTile(tilePosition, spikeTiles[index]);

            // 마지막 프레임(솟아난 상태)일 때 활성화
            isActive = (index == spikeTiles.Length - 1);

            index = (index + 1) % spikeTiles.Length;
            yield return new WaitForSeconds(frameRate);
        }
    }
}
