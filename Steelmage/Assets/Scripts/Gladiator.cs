using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

enum GladiatorState {
  Walking,
  Shooting,
  Idle
}

enum TransitionType {
  Normal,
  Trigger
}

public class Gladiator : MonoBehaviour {
  public Transform GunPosition;

  private Animation _animation;
  private bool _running;

  void Start() {
    _animation = GetComponent<Animation>();
    _animation.Play("guns_idle");
  }

  public void FireRifle() {
    F3DController.Instance.Fire(F3DEffectType.Vulcan, GunPosition);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Space)) {
      _animation.CrossFade(_running ? "guns_idle" : "walk_loop");
      _running = !_running;
    }

    if (Input.GetKeyDown(KeyCode.Mouse0)) {
      _animation.CrossFade("shot_rifle");
    }
  }
}
