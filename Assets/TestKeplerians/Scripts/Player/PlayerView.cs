using UnityEngine;

namespace TestKeplerians
{
  public class PlayerView : MonoBehaviour
  {
    public IInteractable InteractableDetected { get => interactableDetected; }

    [SerializeField]
    private LayerMask layerToDetect;

    [SerializeField]
    private float rayLenght = 5f;

    private Ray cameraRay;

    private RaycastHit hitInfo = new RaycastHit();

    private Transform rayTransform;

    private IInteractable interactableDetected = null;

    private void OnEnable()
    {
      rayTransform = Camera.main.transform;
    }

    private void Start()
    {
      cameraRay = new Ray(transform.position, transform.forward);
    }

    void FixedUpdate()
    {
      cameraRay.origin = rayTransform.position;
      cameraRay.direction = rayTransform.forward;

      if (Physics.Raycast(cameraRay, out hitInfo, rayLenght, layerToDetect))
        interactableDetected = hitInfo.transform.GetComponent<IInteractable>();
      else
        interactableDetected = null;
    }

    private void OnDrawGizmos()
    {
      if (rayTransform != null)
        Debug.DrawRay(rayTransform.position, rayTransform.forward, Color.yellow);
    }
  }
}
