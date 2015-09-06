using UnityEngine;

public class Gladiator : MonoBehaviour {
  public Transform GunPosition;

  private Animation _animation;
  private bool _walking;

  void Start() {
    _animation = GetComponent<Animation>();
    _animation.Play("guns_idle");
  }

  public void FireRifle() {
    F3DController.Instance.Fire(F3DEffectType.Vulcan, GunPosition);
  }

  public void WalkLoopPoint() {
    Debug.Log("Walk Loop Point " + _walking);
    if (_walking) {
      _animation.Play("guns_walk_loop", PlayMode.StopAll);
    }
    else {
      _animation.Play("guns_idle");      
    }
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Mouse0)) {
      _animation.Play("shot_rifle", PlayMode.StopAll);
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
      _walking = !_walking;
      if (_walking) {
        _animation.Play("guns_walk", PlayMode.StopAll);
      }
    }
  }
}
