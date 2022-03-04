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
    //=====================================================================================================
    // Serialization classes
    //=====================================================================================================
    [System.Serializable]
    class SaveWallsData
    {
        public List<WallPanel.WallCreationData> wallsData = new List<WallPanel.WallCreationData>();
    }
    //=====================================================================================================
    public void SerializeWalls(string slotName)
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/" + slotName);
        string dataPath = Application.persistentDataPath + "/" + slotName + "/walls.json";
        List<WallPanel.WallCreationData> wallCreationDatas = new List<WallPanel.WallCreationData>();
        Transform[] walls = wallsContainer.GetComponentsInChildren<Transform>();
        SaveWallsData data = new SaveWallsData();        

        foreach(Transform wall in walls)
        {
            if(wall.gameObject.GetComponent<WallPanel>())
                data.wallsData.Add(wall.gameObject.GetComponent<WallPanel>().creationData);
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
    }

    void LoadWallsData(string slotName)
    {
        string dataPath = Application.persistentDataPath + "/" + slotName + "/walls.json";

        if (File.Exists(dataPath))
        {
            string plainData = File.ReadAllText(dataPath);

            SaveWallsData data = JsonUtility.FromJson<SaveWallsData>(plainData);

            foreach(WallPanel.WallCreationData wall in data.wallsData)
            {
                wallBuilder.BuildSection(wall.spawnPosition, wall.rotationAngle, wall.scale);
            }
        }
        else
        {
            Debug.LogWarning("Data file not found");
        }
    }

    public void LoadSavedData(string slotName="default")
    {
        LoadWallsData(slotName);
    }

    public void SaveData(string slotName="default")
    {
        SerializeWalls(slotName);
    }

    public void ClearBoard()
    {
        //TODO: implement removing everything that exists on the board

        foreach (Transform child in wallsContainer.transform)
            GameObject.Destroy(child.gameObject);
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
