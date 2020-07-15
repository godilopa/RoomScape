
using UnityEngine;
using StandardAssets.Characters.FirstPerson;

namespace TestKeplerians
{
  public class PlayerController : MonoBehaviour
  {
    public PlayerCanvas PlayerCanvas { get => playerCanvas; }

    public PlayerView PlayerView { get => playerView; }

    [SerializeField]
    private PlayerCanvas playerCanvas;

    [SerializeField]
    private PlayerView playerView;

    [SerializeField]
    private Transform playerHand;

    [SerializeField]
    private FirstPersonInput playerInput;

    private bool interactPressed;

    private IInteractable objectInteractable;

    private PickableObject objectPicked;

    public void SetPlayerPosition(Vector3 position)
    {
      transform.position = position;
    }

    public void ActivePlayer(bool active)
    {
      gameObject.SetActive(active);
    }

    public void SetPlayerInput(bool enable)
    {
      playerInput.enabled = enable;
    }

    private void OnInteract()
    {
      interactPressed = true;
    }

    private void ThrowPickable()
    {
      Debug.Log("Interact without having ann object, so release it");

      objectPicked.OnInteract();
      //@HACK: Si pulsamos interactuar rapido, debe esperar a que se lance para ponerlo a null
      if (objectPicked.Attaching == false)
        objectPicked = null;
    }

    private void PickThrowPickable()
    {
      Debug.Log("See an object interact having one, release it if is pickable");

      objectInteractable.OnInteract(objectPicked);

      if (objectInteractable as PickableObject)
        objectPicked.OnInteract();
    }

    private void Interact()
    {
      Debug.Log("See an object interact");
      if (objectInteractable as PickableObject)
      {
        Debug.Log("Ive pickep up a pickable");
        objectPicked = objectInteractable as PickableObject;

        if (objectPicked.AttachFather == null && playerHand != null)
          objectPicked.AttachFather = playerHand;
        else
          Debug.Log("Player hand is null");
      }

      objectInteractable.OnInteract();

      playerCanvas.ShowReactionTextDuringFixedTime(((InteractableObject)objectInteractable).TextReaction);
    }

    private void CanvasUpdate()
    {
      if (objectInteractable != null && playerCanvas != null)
      {
        playerCanvas.ShowObjectName(((InteractableObject)objectInteractable).TextName);
        playerCanvas.ShowReactionText(((InteractableObject)objectInteractable).TextThought);
      }
      else if (playerCanvas != null)
      {
        playerCanvas.ShowObjectName(string.Empty);
        playerCanvas.ShowReactionText(string.Empty);
      }
    }

    private void Awake()
    {
      if (playerCanvas == null)
        playerCanvas = GetComponent<PlayerCanvas>();

      if (playerView == null)
        playerView = GetComponent<PlayerView>();

      if (playerInput == null)
        playerInput = GetComponent<FirstPersonInput>();
    }

    private void Update()
    {
      objectInteractable = playerView.InteractableDetected;

      CanvasUpdate();

      //Interactuo sin tener objeto con uno cogido, lo suelto
      if (interactPressed == true && objectInteractable == null && objectPicked != null)
        ThrowPickable();

      //Veo un objeto interactuo teniendo uno cogido, suelto si es tambien pickable
      if (interactPressed == true && objectInteractable != null && objectPicked != null)
        PickThrowPickable();

      //Veo un objeto interactuo
      if (interactPressed == true && objectInteractable != null)
        Interact();

      interactPressed = false;
    }
  }
}
