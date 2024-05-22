using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string id;
    public Vector3 position;
    public Vector3 rotation;
    public int hp;

    public static List<EnemyData> SaveData(GameObject[] enemies)
    {
        List<EnemyData> enemiesData = new List<EnemyData>();
        foreach (GameObject enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            EnemyData data = new EnemyData
            {
                id = enemyComponent.uniqueID,
                position = enemy.transform.position,
                rotation = enemy.transform.rotation.eulerAngles,
                hp = enemyComponent.enemyCurrentHP
            };
            enemiesData.Add(data);
        }
        return enemiesData;
    }

    public static void LoadData(List<EnemyData> enemiesData, GameObject[] enemies)
    {
        foreach (EnemyData data in enemiesData)
        {
            foreach (GameObject enemy in enemies)
            {
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent.uniqueID == data.id)
                {
                    enemy.transform.position = data.position;
                    enemy.transform.rotation = Quaternion.Euler(data.rotation);
                    enemyComponent.enemyCurrentHP = data.hp;
                    break;
                }
            }
        }
    }
}
