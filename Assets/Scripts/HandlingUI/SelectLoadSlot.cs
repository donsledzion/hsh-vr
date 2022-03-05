using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SelectLoadSlot : MonoBehaviour
{
    [SerializeField] GameObject loadButtonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] FindSavedSessions()
    {
        string[] slots = Directory.GetDirectories(Application.persistentDataPath, "*",SearchOption.TopDirectoryOnly);
        foreach(string slot in slots)
        {   
            Debug.Log("Slot: " + Path.GetFileName(slot));            
        }
        return slots;
    }

    public void DrawButtons()
    {
        //string[] slots = FindSavedSessions();
        string[] slots = Directory.GetDirectories(Application.persistentDataPath, "*",SearchOption.TopDirectoryOnly);
        int counter = 0;
        foreach(string slot in slots)
        {
            string slotName = Path.GetFileName(slot);
            /*int buttonHeight = loadButtonPrefab.gameObject.transform.;*/
            GameObject loadButton = Instantiate(loadButtonPrefab, transform.position+new Vector3(0,-counter*60,0), transform.rotation);
            counter++;
            loadButton.transform.SetParent(transform);
            LoadButton loadButtonController = loadButton.GetComponent<LoadButton>();
            loadButtonController.UpdateButton(slotName,new Vector2(10,10), "jakas data");
            //loadButton.GetComponent<Button>().onClick

        }
    }
}
