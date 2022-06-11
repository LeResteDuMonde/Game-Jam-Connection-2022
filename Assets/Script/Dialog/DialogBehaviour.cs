using UnityEngine;

public class DialogBehaviour : MonoBehaviour, ActorBehaviour
{
    public string dialogFile;

    public bool Condition(EventMachine _) {
        // TODO
        return true;
    }

    public void UpdateActor(EventMachine _) {}

    public void Interact(EventMachine ev) {
        Debug.Log("Interact");
        var dialog = LocalizationManager.instance.RetrieveDialog(dialogFile);
        DialogBox.instance.ShowNewDialog(dialog);
    }
}
