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
        GameManager.Instance.PortalProperty = this;
        GameManager.Instance.AddOnAllEnemiesDefeated(SetActivePortalActive);
        portalCollider.enabled = false;
        portalRenderer.enabled = false;
    }
    public void SetActivePortalActive()
    {
        portalCollider.enabled = true;
        portalRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.RemoveOnAllEnemiesDefeated(SetActivePortalActive);
            GameManager.Instance.ChangeScene(SceneState.Play);
        }
    }


}
