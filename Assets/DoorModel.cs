using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorModel : MonoBehaviour
{
    [SerializeField] GameObject model_1;
    [SerializeField] GameObject model_2;
    [SerializeField] GameObject model_3;
    
    public void SelectModel(int model_no)
    {
        DisableAll();
        switch(model_no)
        {
            case 1:
                model_1.SetActive(true);
                break;
            case 2:
                model_2.SetActive(true);
                break;
            case 3:
                model_3.SetActive(true);
                break;
        }
    }

    void DisableAll()
    {
        model_1.SetActive(false);
        model_2.SetActive(false);
        model_3.SetActive(false);
    }
}
