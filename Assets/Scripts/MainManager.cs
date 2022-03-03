using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [SerializeField] WallBuilder wallBuilder;
    

    public GameObject wallsContainer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public List<WallPanel.WallCreationData> wallsData = new List<WallPanel.WallCreationData>();
    }

    public void SaveWallsData()
    {
        List<WallPanel.WallCreationData> wallCreationDatas = new List<WallPanel.WallCreationData>();
        Transform[] walls = wallsContainer.GetComponentsInChildren<Transform>();
        SaveData data = new SaveData();        

        foreach(Transform wall in walls)
        {
            if(wall.gameObject.GetComponent<WallPanel>())
                data.wallsData.Add(wall.gameObject.GetComponent<WallPanel>().creationData);
        }

        string json = JsonUtility.ToJson(data);

        Debug.Log("Json: " + json);

        File.WriteAllText(Application.persistentDataPath + "/building_status.json", json);
        

        
    }

    public void LoadWallSData()
    {
        if (File.Exists(Application.persistentDataPath + "/building_status.json"))
        {
            string plainData = File.ReadAllText(Application.persistentDataPath + "/building_status.json");
            Debug.Log("Plain Data: " + plainData);

            SaveData data = JsonUtility.FromJson<SaveData>(plainData);
            Debug.Log("Read Data: " + data);
            foreach(WallPanel.WallCreationData wall in data.wallsData)
            {
                Debug.Log(wall.rotationAngle + ", " + wall.scale + ", " + wall.spawnPosition);
                wallBuilder.BuildSection(wall.spawnPosition, wall.rotationAngle, wall.scale);
            }

        }
        else
        {
            Debug.LogWarning("Load file not found");
        }
    }
}
