using UnityEngine;

public enum Language {
    French,
    English
}

public class LocalizationManager : MonoBehaviour
{
    public Language language;

    public static LocalizationManager instance;

    public void Awake() {
        instance = this;
    }

    public Dialog RetrieveDialog(string dialogName) {
        string path = "Dialog/" + language.ToString() + "/" + dialogName;
        var dialog = Resources.Load<TextAsset>(path);
        return JsonUtility.FromJson<Dialog>(dialog.text);
    }
}
