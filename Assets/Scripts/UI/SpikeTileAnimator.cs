using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikeTileAnimator : MonoBehaviour
{
    public Tilemap tilemap; // ������ũ�� �ִ� Ÿ�ϸ�
    public TileBase[] spikeTiles; // �ִϸ��̼��� Ÿ�� �迭 (��: �ھƳ��� Ÿ�� �̹�����)
    public Vector3Int tilePosition; // Ư�� Ÿ���� ��ǥ
    public float frameRate = 0.5f; // �ִϸ��̼� �ӵ�
    public bool isActive = false; // ������ũ�� Ȱ�� �������� ����

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

            // ������ ������(�ھƳ� ����)�� �� Ȱ��ȭ
            isActive = (index == spikeTiles.Length - 1);

            index = (index + 1) % spikeTiles.Length;
            yield return new WaitForSeconds(frameRate);
        }
    }
}
