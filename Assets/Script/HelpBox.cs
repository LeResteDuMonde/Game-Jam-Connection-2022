using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBox : MonoBehaviour
{
    public static HelpBox instance;

    void Awake() {
        instance = this;
    }

    [SerializeField] private GameObject helpButton;
    [SerializeField] private GameObject helpPanel;
    
    private GameObject escapeTip;
    private GameObject boardTip;
    private GameObject inventoryTip;
    private GameObject helpTip;

    // Start is called before the first frame update
    public void ToggleHelp() {
        helpPanel.SetActive(!helpPanel.activeSelf);
        helpButton.SetActive(!helpPanel.activeSelf);
    }
}
