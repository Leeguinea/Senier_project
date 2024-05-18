using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public Vector3 rotation;
    public int hp;
    public int ammo;

    public void SaveData(Transform playerTransform, int playerHP, int playerAmmo)
    {
        position = playerTransform.position;
        rotation = playerTransform.rotation.eulerAngles;
        hp = playerHP;
        ammo = playerAmmo;
    }

    public void LoadData(Transform playerTransform, ref int playerHP, ref int playerAmmo)
    {
        playerTransform.position = position;
        playerTransform.rotation = Quaternion.Euler(rotation);
        playerHP = hp;
        playerAmmo = ammo;
    }
}
