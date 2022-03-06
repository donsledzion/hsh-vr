using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonSlot : MonoBehaviour
{
    [SerializeField] protected GameObject slotButtonPrefab;
    [SerializeField] protected GameObject goBackButtonPrefab;
    [SerializeField] protected MainManager mainManager;
    [SerializeField] protected CreationUIController uiController;
    [SerializeField] protected int maxSlotsCount = 5;
    [SerializeField] protected float buttonsOffset = 5;
    [Space]
    [SerializeField] protected UnityEvent onBackButton;



    protected void ClearButtons()
    {
        Transform[] children = gameObject.transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject != transform.gameObject)
                Destroy(child.gameObject);
        }
    }

    protected void AddBackButton(Vector2 buttonPos)
    {
        GameObject goBackButton = Instantiate(
                goBackButtonPrefab,
                transform.position + new Vector3(buttonPos.x, buttonPos.y, 0),
                transform.rotation);
        goBackButton.transform.SetParent(transform);
        goBackButton.GetComponent<Button>().onClick.AddListener(() => onBackButton.Invoke());
    }
}
