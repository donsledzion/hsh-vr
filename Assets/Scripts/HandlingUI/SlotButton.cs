using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotButton : MonoBehaviour
{
    public string SlotNameText { get; private set; }
    [SerializeField] public TextMeshProUGUI slotNameTMP;
    [Space]
    string gridSizeText;
    [SerializeField] TextMeshProUGUI gridSizeTMP;
    [Space]
    string slotDateText;
    [SerializeField] TextMeshProUGUI slotDateTMP;

    public void UpdateButton(string name, Vector2 size, string date = "n/a")
    {
        slotNameTMP.text = name;
        gridSizeTMP.text = "Rozmiar: " + size.x + "m x " + size.y + "m";
        slotDateTMP.text = "Zmodyfikowano: " + date;
    }
}
