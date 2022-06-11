// Describes a behaviour that runs under a certain condition
public interface ActorBehaviour {
    bool Condition(ActorManager ev);

    // Run some code
    void UpdateActor(ActorManager ev);

    // Run if interacted with by the player
    void Interact(ActorManager ev);
}
