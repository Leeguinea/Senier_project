using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public PlayerData playerData; // 플레이어의 위치, 회전, 체력, 탄약 수
    public string lastSavedScene; // 마지막으로 저장된 scene 이름
}
