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
  //private GladiatorState _currentState;

  //private readonly Dictionary<GladiatorState, string> _stateAnimations = new Dictionary<GladiatorState, string> {
  //  {GladiatorState.Idle, "guns_idle"},
  //  {GladiatorState.Walking, "guns_walk_loop"},
  //};

  //private readonly Dictionary<GladiatorState, string> _enterStateAnimations = new Dictionary<GladiatorState, string> {
  //  {GladiatorState.Walking, "guns_walk_start" },
  //  {GladiatorState.Shooting, "shot_rifle"}
  //};

  //private readonly Dictionary<GladiatorState, string> _exitStateAnimations = new Dictionary<GladiatorState, string> {
  //  {GladiatorState.Walking, "guns_walk_stop" }
  //};

  //private readonly HashSet<string> _interruptibleAnimations = new HashSet<string> {
  //  "guns_idle"
  //};

  void Start() {
    _animation = GetComponent<Animation>();
    _animation.Play("guns_idle");
    //_currentState = GladiatorState.Idle;
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

  //void Update() {
  //  if (Input.GetKeyDown(KeyCode.Mouse0)) {
  //    TransitionToState(GladiatorState.Shooting, TransitionType.Trigger);
  //  }

  //  if (Input.GetKeyDown(KeyCode.Space)) {
  //    var newState = _currentState == GladiatorState.Walking ?
  //        GladiatorState.Idle : GladiatorState.Walking;
  //    TransitionToState(newState, TransitionType.Normal);
  //  }

  //  UpdateAnimation();
  //}

  //void TransitionToState(GladiatorState newState, TransitionType type) {
  //  if (_interruptibleAnimations.Contains(_stateAnimations[_currentState])) {
  //    Debug.Log("stopping " + _stateAnimations[_currentState]);
  //    _animation.Stop();
  //  }
  //  if (_exitStateAnimations.ContainsKey(_currentState)) {
  //    Debug.Log("exit Playing" + _exitStateAnimations[_currentState]);
  //    PlayOrQueue(_exitStateAnimations[_currentState]);
  //  }
  //  if (_enterStateAnimations.ContainsKey(newState)) {
  //    Debug.Log("enter Playing " + _enterStateAnimations[newState]);
  //    PlayOrQueue(_enterStateAnimations[newState]);
  //  }
  //  if (type == TransitionType.Normal) {
  //    _currentState = newState;
  //  }
  //}

  //void UpdateAnimation() {
  //  if (!_animation.isPlaying) {
  //    Debug.Log("Nothing playing, starting " + _stateAnimations[_currentState]);
  //    _animation.Play(_stateAnimations[_currentState]);
  //  }
  //}

  //void PlayOrQueue(string animationName) {
  //  if (_animation.isPlaying) {
  //    _animation.PlayQueued(animationName);
  //  } else {
  //    _animation.Play(animationName);
  //  }
  //}
}
