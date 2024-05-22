using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public string id;
    public bool isActive;

    public static List<ObjectData> SaveData(GameObject[] objects)
    {
        List<ObjectData> objectsData = new List<ObjectData>();
        foreach (GameObject obj in objects)
        {
            ObjectData data = new ObjectData
            {
                id = obj.name,
                isActive = obj.activeSelf
            };
            objectsData.Add(data);
        }
        return objectsData;
    }

    public static void LoadData(List<ObjectData> objectsData, GameObject[] objects)
    {
        foreach (ObjectData data in objectsData)
        {
            foreach (GameObject obj in objects)
            {
                if (obj.name == data.id)
                {
                    obj.SetActive(data.isActive);
                    break;
                }
            }
        }
    }
}
