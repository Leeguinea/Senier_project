using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition;  // �÷��̾��� ��ġ
    public Vector3 playerrotation;  // �÷��̾��� ȸ��
    public int playerHP;          // �÷��̾��� ü��
    public int bulletCnt;           // �÷��̾��� ź�� ��

    public void SaveData(Transform playerTransform, int hp, int bullets)
    {
        // ������ ���̺�
        playerPosition = playerTransform.position; // �÷��̾��� ��ġ ����
        //playerrotation = playerTransform.rotation.eulerAngles; // �÷��̾��� ȸ�� ����
        playerHP = hp; // �÷��̾��� ü�� ����
        bulletCnt = bullets; // �÷��̾��� ź�� �� ����
    }

    public void SaveHPandBullet(int hp, int bullets)
    {
        // ������ ���̺�
        playerHP = hp; // �÷��̾��� ü�� ����
        bulletCnt = bullets; // �÷��̾��� ź�� �� ����
    }

    public void LoadData(Transform playerTransform, ref int hp, ref int bullets)
    {
        //������ �ε�
        playerTransform.position = playerPosition; // ����� ��ġ�� �÷��̾� �̵�
        //playerTransform.rotation = Quaternion.Euler(playerrotation); // ����� ȸ������ �÷��̾� ȸ��
        hp = playerHP; // ����� ü��
        bullets = bulletCnt; // ����� ź�� ��
    }

    public void LoadHPandBullet(ref int hp, ref int bullets)
    {
        //������ �ε�
        hp = playerHP; // ����� ü��
        bullets = bulletCnt; // ����� ź�� ��
    }
}
