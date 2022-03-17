using UnityEngine;

public class ConstructingToggler : MonoBehaviour
{
    [SerializeField] GameObject wallBuilder;
    [SerializeField] GameObject doorMounter;
    
    public void Toggle()
    {
        wallBuilder.SetActive(!wallBuilder.activeSelf);
        doorMounter.SetActive(!doorMounter.activeSelf);
    }
}
