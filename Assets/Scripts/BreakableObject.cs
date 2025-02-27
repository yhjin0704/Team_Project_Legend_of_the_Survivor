using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public int health = 1;
    private int currentHealth;


    void Start()
    {
        currentHealth = health; // 시작 시 체력 설정
    }
}

