using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStartPileToggler : MonoBehaviour
{
    [SerializeField] GameObject buildingTip;
    [SerializeField] GameObject destroyinTip;

    public void SetBuilder()
    {
        destroyinTip.gameObject.SetActive(false);
        buildingTip.gameObject.SetActive(true);        
    }

    public void SetDestroyer()
    {
        buildingTip.gameObject.SetActive(false);
        destroyinTip.gameObject.SetActive(true);
    }
}
