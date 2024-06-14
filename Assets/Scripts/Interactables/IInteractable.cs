public interface IInteractable
{
    void InitInteractable();
    void OnInteract();
    void OnEnterRange();
    void OnExitRange();
    void SetCost(int newCost);
}