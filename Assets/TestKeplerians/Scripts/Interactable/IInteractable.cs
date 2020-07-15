
namespace TestKeplerians
{
  public interface IInteractable
  {
    string TextName { get; }

    string TextThought { get; }

    string TextReaction { get; }

    void OnInteract(params object[] extraInfo);
  }
}
