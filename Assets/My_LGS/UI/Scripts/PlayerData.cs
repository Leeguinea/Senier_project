using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition;  // 플레이어의 위치
    public Vector3 playerrotation;  // 플레이어의 회전
    public int playerHP;          // 플레이어의 체력
    public int bulletCnt;           // 플레이어의 탄약 수

    public void SaveData(Transform playerTransform, int hp, int bullets)
    {
        // 데이터 세이브
        playerPosition = playerTransform.position; // 플레이어의 위치 저장
        //playerrotation = playerTransform.rotation.eulerAngles; // 플레이어의 회전 저장
        playerHP = hp; // 플레이어의 체력 저장
        bulletCnt = bullets; // 플레이어의 탄약 수 저장
    }

    public void SaveHPandBullet(int hp, int bullets)
    {
        // 데이터 세이브
        playerHP = hp; // 플레이어의 체력 저장
        bulletCnt = bullets; // 플레이어의 탄약 수 저장
    }

    public void LoadData(Transform playerTransform, ref int hp, ref int bullets)
    {
        //데이터 로드
        playerTransform.position = playerPosition; // 저장된 위치로 플레이어 이동
        //playerTransform.rotation = Quaternion.Euler(playerrotation); // 저장된 회전으로 플레이어 회전
        hp = playerHP; // 저장된 체력
        bullets = bulletCnt; // 저장된 탄약 수
    }

    public void LoadHPandBullet(ref int hp, ref int bullets)
    {
        //데이터 로드
        hp = playerHP; // 저장된 체력
        bullets = bulletCnt; // 저장된 탄약 수
    }
}
