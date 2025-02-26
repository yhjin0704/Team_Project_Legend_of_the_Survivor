using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTileObject : MonoBehaviour
{
    public int health = 1;
    public Tilemap tilemap;         // Ÿ�ϸ� ����
    public TileBase breakableTile;  // �μ� �� �ִ� Ÿ�� (�ִϸ��̼� Ÿ��)

    private void Start()
    {
        if (tilemap == null)
        {
            tilemap = GetComponent<Tilemap>();
        }
    }

    // Ÿ�Ͽ� �������� �ִ� �Լ�
    public void DamageTile(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);   // �� ��ǥ -> Ÿ�� ��ǥ ��ȯ


        // ���� ��ġ�� Ÿ���� ������ �μ� �� �ִ� Ÿ������ Ȯ��
        if (tilemap.GetTile(cellPosition) == breakableTile)
        {
            tilemap.SetTile(cellPosition, null);                       // Ÿ�� ���� (���� ������ �� ����)
        }
    }



    // �浹 ���� (�÷��̾� ���� � ����)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))        // "PlayerAttack" �±װ� �ִ� ������Ʈ�� �浹 ��
        {
            DamageTile(collision.transform.position);    // ���� ��ġ�� �������� Ÿ�� ����
        }
    }
}
