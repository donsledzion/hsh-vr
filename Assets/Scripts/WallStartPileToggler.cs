using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStartPileToggler : MonoBehaviour
{
    [SerializeField] GameObject buildingTip;
    [SerializeField] GameObject destroyinTip;
    [SerializeField] GameObject buildingRectTip;

    public void SetBuilder()
    {
        destroyinTip.gameObject.SetActive(false);
        buildingRectTip.gameObject.SetActive(false);
        buildingTip.gameObject.SetActive(true);        
    }

    public void SetDestroyer()
    {
        buildingTip.gameObject.SetActive(false);
        buildingRectTip.gameObject.SetActive(false);
        destroyinTip.gameObject.SetActive(true);
    }

    public void SetRectBuilder()
    {
        buildingTip.gameObject.SetActive(false);
        buildingRectTip.gameObject.SetActive(true);
        destroyinTip.gameObject.SetActive(false);
    }
}
