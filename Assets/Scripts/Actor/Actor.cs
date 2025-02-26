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

    [Range(1, 100)] public float hp = 100;
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

    protected Transform _renderer;
    public Transform GetRenderer()
    {
        return _renderer;
    }

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        _renderer = transform.Find("Renderer");
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
}