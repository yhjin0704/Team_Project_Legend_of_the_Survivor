using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Portal : MonoBehaviour
{
    private bool isPortalActive = false;
    private Collider2D portalCollider;
    private TilemapRenderer portalRenderer;
    private void Awake()
    {
        portalCollider = GetComponent<Collider2D>();
        portalRenderer = GetComponent<TilemapRenderer>();
        GameManager.Instance.AddOnAllEnemiesDefeated(() => SetPortalActive(true));
        SetPortalActive(false);
    }
    public void SetPortalActive(bool isActive)
    {
        portalCollider.enabled = isActive;
        portalRenderer.enabled = isActive;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ChangeScene(SceneState.Play);
        }
    }


}
