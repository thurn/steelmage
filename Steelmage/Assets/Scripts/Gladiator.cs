using UnityEngine;

public class Gladiator : MonoBehaviour {
  public Transform GunPosition;

  private bool _isFiring;
  private Animator _animator;
  private readonly int _shootRifleHash = Animator.StringToHash("shootRifle");

  // Use this for initialization
  void Start() {
    _animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update() {
    if (!_isFiring && Input.GetKeyDown(KeyCode.Mouse0)) {
      //F3DController.Instance.Fire(F3DEffectType.Vulcan, GunPosition);
      _isFiring = true;
      _animator.SetTrigger(_shootRifleHash);
    }

    if (_isFiring && Input.GetKeyUp(KeyCode.Mouse0)) {
      //F3DController.Instance.Stop();
      _isFiring = false;
    }
  }
}
