using UnityEngine;

public class Gladiator : MonoBehaviour {
  public Transform GunPosition;

  private Animator _animator;
  private bool _walking;

// Use this for initialization
  void Start() {
    _animator = GetComponent<Animator>();
  }

  public void FireRifle() {
    F3DController.Instance.Fire(F3DEffectType.Vulcan, GunPosition);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Mouse0)) {
      _animator.SetTrigger("shootRifle");
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
      _walking = !_walking;
      _animator.SetBool("walking", _walking);
    }
  }
}
