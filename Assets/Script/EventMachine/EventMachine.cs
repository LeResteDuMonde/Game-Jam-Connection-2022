using UnityEngine;

public class EventMachine : MonoBehaviour
{
    public GameObject[] actors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateActors() {
        foreach(GameObject g in actors) {
            Actor a = g.GetComponent<Actor>();
            a.UpdateBehaviours(this);
        }
    }
}
