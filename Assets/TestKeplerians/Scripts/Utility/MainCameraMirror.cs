
using UnityEngine;

namespace TestKeplerians
{
  public class MainCameraMirror : MonoBehaviour
  {
    private Transform cameraTransform;

    private void OnEnable()
    {
      cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
      transform.position = cameraTransform.position;
      transform.rotation = cameraTransform.rotation;
    }
  }
}
