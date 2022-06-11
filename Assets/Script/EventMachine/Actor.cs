using UnityEngine;

public class Actor : MonoBehaviour
{
    public GameObject[] behaviours;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Run each behaviour which condition is satisfied
    public void UpdateBehaviours(EventMachine ev) {
        foreach (GameObject g in behaviours) {
            ActorBehaviour b = g.GetComponent<ActorBehaviour>();
            if (b.Condition(ev)) b.Update(ev);
        }
    }

    public void Interact(EventMachine ev) {
        // Only one interaction behavior can be run at a time
        foreach (GameObject g in behaviours) {
            ActorBehaviour b = g.GetComponent<ActorBehaviour>();
            if (b.Condition(ev)) {
                b.Interact(ev);
                break;
            }
        }
    }
}
