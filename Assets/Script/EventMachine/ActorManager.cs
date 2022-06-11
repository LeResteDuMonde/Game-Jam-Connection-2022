using UnityEngine;

public class ActorManager : MonoBehaviour
{

    public static ActorManager instance;

    void Awake() {
        instance = this;
    }

    public GameObject[] actors;

    public void TriggerTransition(string transition) {
        foreach(GameObject g in actors) {
            var a = g.GetComponent<Character>();
            a.TriggerTransition(transition);
        }
    }

    void UpdateActors() {
        foreach(GameObject g in actors) {
            var a = g.GetComponent<Character>();
            //a.UpdateBehaviours();
        }
    }
}
