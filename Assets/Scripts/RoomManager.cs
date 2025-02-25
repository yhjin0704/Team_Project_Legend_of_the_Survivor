using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class RoomManager : MonoBehaviour
{
    
    public GameObject[] mapPrefabs;   // 맵 프리팹 리스트
    public GameObject playerPrefab;   // 플레이어 프리팹
    public GameObject enemyPrefab;    // 적 프리팹
    public GameObject portalPrefab;   // 포탈 프리팹

    private GameObject currentRoom;   // 지금 위치한 방
    private List<GameObject> enemies = new List<GameObject>();  //적들 배열들
    private GameObject portal;        //포탈 오브젝트

    
    void Start()
    {
        GenerateNewRoom();
        SpawnPlayer();
    }



    void GenerateNewRoom()            //방 생성
    {
        if (currentRoom != null)
        {
            Destroy(currentRoom);
        }

        // 랜덤한 방 프리팹 선택
        int roomIndex = Random.Range(0, roomPrefabs.Length);
        currentRoom = Instantiate(roomPrefabs[roomIndex], Vector3.zero, Quaternion.identity);

}
*/