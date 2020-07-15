using UnityEngine;

namespace TestKeplerians
{
  [RequireComponent(typeof(Rigidbody))]
  public class PickableObject : InteractableObject
  {
    public bool Attaching { get => attaching; }

    public Transform AttachFather { get => attachFather; set => attachFather = value; }

    [Header("Pickup options")]
    [SerializeField]
    private Vector3 finalRotation;

    [SerializeField]
    private float duration = 1.0f;

    [SerializeField]
    private float throwForce = 2.0f;

    [Header("Layers")]
    [SerializeField]
    private string layerAfterPickUp;

    [SerializeField]
    private string layerAfterThrow;

    private Transform attachFather;

    private Rigidbody rg;

    private Vector3 forceVelocity;

    private Transform rotateTransform;

    private bool isAttached = false;

    private bool attaching = false;

    private float timer;

    override protected void ReactionToInteract(params object[] extraInfo)
    {
      if (isAttached == false)
      {
        attaching = true;
        rg.isKinematic = true;
      }
      else
        DettachedFromPlayer();
    }

    public void DettachedFromPlayer()
    {
      transform.parent = null;
      gameObject.layer = LayerMask.NameToLayer(layerAfterThrow);

      forceVelocity = rotateTransform.forward * throwForce;
      forceVelocity.y = throwForce * 2;//@HAck
      rg.isKinematic = false;
      rg.AddForce(forceVelocity, ForceMode.Impulse);

      isAttached = false;

      FinishInteraction();
    }

    private void AttachedToPlayer()
    {
      transform.parent = attachFather;
      gameObject.layer = LayerMask.NameToLayer(layerAfterPickUp);

      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.Euler(finalRotation);

      isAttached = true;
      attaching = false;

      FinishInteraction();
    }

    private void Awake()
    {
      rg = GetComponent<Rigidbody>();
      rotateTransform = Camera.main.transform;
    }


    private void Update()
    {
      if (attaching == false)
        return;

      timer += Time.deltaTime;
      transform.position = Vector3.Lerp(transform.position, attachFather.position, timer / duration);
      transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(finalRotation), timer / duration);

      if (Vector3.SqrMagnitude(transform.position - attachFather.position) < 0.001f)
      {
        AttachedToPlayer();
        timer = 0;
      }
    }
  }
}
