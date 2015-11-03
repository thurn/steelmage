using UnityEngine;

public class Gladiator2 : MonoBehaviour {
  private Animator _animator;
  private Animation _animation;

  public void Start() {
    _animator = GetComponent<Animator>();
    _animation = GetComponent<Animation>();
  }

  public void Update() {
    if (!_animation.isPlaying) {
      _animation.enabled = false;
      _animator.enabled = true;
    }

    if (Input.GetKeyDown(KeyCode.V)) {
      _animator.SetTrigger("Walk");
    } else if (Input.GetKeyDown(KeyCode.B)) {
      _animator.SetTrigger("Cast");
    } else if (Input.GetKeyDown(KeyCode.N)) {
      _animator.SetTrigger("Draw");
    } else if (Input.GetKeyDown(KeyCode.L)) {
      _animator.SetTrigger("Idle");
    } else if (Input.GetKeyDown(KeyCode.M) && !_animation.isPlaying) {
      _animator.enabled = false;
      _animation.enabled = true;
      _animation.Play("shot_rifle");
    }
  }

  public void DrawWeapon() {
    Debug.Log("Draw Weapon");
  }

  public void FireRifle() {
    Debug.Log("Fire Rifle");
  }
}
