using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Actor : MonoBehaviour
{
    [Range(1, 100)]public float hp = 100;
    [Range(1f, 20f)]public float speed = 3;
    public float atk = 1;
    public float atkDelay = 3;
    public int gold = 0;
    public bool IsAlive = true;
    public bool isMove = false;

    public GameObject defaultBulletPrefab;

    protected Animator animator;

    protected Transform _renderer;
    public Transform GetRenderer()
    {
        return _renderer;
    }

    // 공격 목표
    protected Transform target;
    public Transform GetTarget()
    {
        return target;
    }

    public float shotPosDistance;

    // 투사체 발사 위치
    protected Transform shotPos;
    public Transform GetShotPos()
    {
        return shotPos;
    }
    
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        _renderer = transform.Find("Renderer");
        shotPos = transform.Find("ShotPos");
    }

    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        GameObject monsterObject = GameObject.Find("Monster");
        SetShotPos(monsterObject.transform);
    }

    protected virtual void Attak()
    {
    }

    protected virtual void Hit()
    {
    }

    protected virtual void Dead()
    {
    }

    protected virtual void SetShotPos(Transform _targetPos)
    {
        Vector3 direction = (_targetPos.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shotPos.rotation = Quaternion.Euler(0, 0, angle);

        shotPos.position = transform.position + direction * shotPosDistance;
        shotPos.position = transform.position + direction * shotPosDistance;
    }

    void OnDrawGizmos()
    {
        if (shotPos != null)
        {
            // 그려질 색상 설정
            Gizmos.color = Color.red;
            // 2D 씬에서 targetTransform의 위치에 원 그리기
            Gizmos.DrawWireSphere(shotPos.position, 0.02f);
        }
    }
}