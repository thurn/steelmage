using UnityEngine;

public class Gladiator : MonoBehaviour {
  public Transform GunPosition;

  private bool _isFiring;

  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if (!_isFiring && Input.GetKeyDown(KeyCode.Mouse0)) {
      F3DController.Instance.Fire(F3DEffectType.Vulcan, GunPosition);
      _isFiring = true;
    }

    if (_isFiring && Input.GetKeyUp(KeyCode.Mouse0)) {
      F3DController.Instance.Stop();
      _isFiring = false;
    }
  }
}
