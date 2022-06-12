using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBox : MonoBehaviour
{

    public static HelpBox instance;

    void Awake() {
        instance = this;
    }

    private GameObject helpPanel;
    private GameObject escapeTip;
    private GameObject boardTip;
    private GameObject inventoryTip;
    private GameObject helpTip;

    // Start is called before the first frame update
    void Start()
    {
        helpPanel = transform.Find("HelpPanel").gameObject;
    }

    public void ToggleHelp() {
        helpPanel.SetActive(!helpPanel.activeSelf);
    }
}
