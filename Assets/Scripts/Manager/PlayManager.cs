using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        int randomIndex = Random.Range(0, gameManager.Maps.Length - 1);
        Instantiate(gameManager.Maps[randomIndex], Vector3.zero, Quaternion.identity);
    }

}
