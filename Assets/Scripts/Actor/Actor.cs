using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EState
{
    Idle,
    Move,
    Attack,
    Dead
}

public class Actor : MonoBehaviour
{
    public SpriteRenderer characterRenderer;

    protected AnimationHandler animationHandler;

    public float hp = 100;
    protected float maxHp;
    public float GetMaxHp()
    {
        return maxHp;
    }
    public void SetMaxHp(float _maxHp)
    {
        maxHp = _maxHp;
    }

    [Range(1f, 20f)] public float speed = 3;
    public float atk = 1;
    public float atkDelay = 3;

    protected EState state = EState.Idle;
    public EState GetState()
    {
        return state;
    }
    public void SetState(EState _state)
    {
        state = _state;
    }

    public bool IsAlive = true;

    public GameObject defaultBulletPrefab;

    protected Animator animator;

    protected SpriteRenderer _renderer;
    public Transform GetRendererTransform()
    {
        return _renderer.transform;
    }

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        _renderer = GetComponentInChildren<SpriteRenderer>();

        maxHp = hp;
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void LateUpdate()
    {
        Vector3Int cellPos = GameManager.Instance.ChangeToCellPosition(_renderer.transform.position);
        _renderer.sortingOrder = -(int)cellPos.y;
    }
}