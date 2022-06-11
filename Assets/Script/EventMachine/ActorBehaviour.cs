// Describes a behaviour that runs under a certain condition
public interface ActorBehaviour {
    bool Condition(EventMachine ev);

    // Run some code
    void Update(EventMachine ev);

    // Run if interacted with by the player
    void Interact(EventMachine ev);
}
