using System.Collections.Generic;
using UnityEngine;

enum GladiatorState {
  Walking,
  Shooting,
  Idle
}

public class Gladiator : MonoBehaviour {
  public Transform GunPosition;

  private Animation _animation;
  private GladiatorState _currentState;

  private readonly Dictionary<GladiatorState, string> _stateAnimations = new Dictionary<GladiatorState, string> {
    {GladiatorState.Idle, "guns_idle"},
    {GladiatorState.Walking, "guns_walk_loop"},
    {GladiatorState.Shooting, "shot_rifle"}
  };

  private readonly Dictionary<GladiatorState, string> _enterStateAnimations = new Dictionary<GladiatorState, string> {
    {GladiatorState.Walking, "guns_walk_start" }
  };

  private readonly Dictionary<GladiatorState, string> _exitStateAnimations = new Dictionary<GladiatorState, string> {
    {GladiatorState.Walking, "guns_walk_stop" }
  };

  private readonly HashSet<string> _interruptibleAnimations = new HashSet<string> {
    "guns_idle"
  };

  void Start() {
    _animation = GetComponent<Animation>();
    _currentState = GladiatorState.Idle;
  }

  public void FireRifle() {
    F3DController.Instance.Fire(F3DEffectType.Vulcan, GunPosition);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Mouse0)) {
      _animation.Play("shot_rifle", PlayMode.StopAll);
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
      TransitionToState(GladiatorState.Walking);
    }

    UpdateAnimation();
  }

  void TransitionToState(GladiatorState newState) {
    if (_interruptibleAnimations.Contains(_stateAnimations[_currentState])) {
      Debug.Log("stopping " + _stateAnimations[_currentState]);
      _animation.Stop();
    }
    if (_exitStateAnimations.ContainsKey(_currentState)) {
      Debug.Log("exit Playing" + _exitStateAnimations[_currentState]);
      PlayOrQueue(_exitStateAnimations[_currentState]);
    }
    if (_enterStateAnimations.ContainsKey(newState)) {
      Debug.Log("enter Playing " + _enterStateAnimations[newState]);
      PlayOrQueue(_enterStateAnimations[newState]);
    }
    _currentState = newState;
  }

  void UpdateAnimation() {
    if (!_animation.isPlaying) {
      Debug.Log("Nothing playing, starting " + _stateAnimations[_currentState]);
      _animation.Play(_stateAnimations[_currentState]);
    }
  }

  void PlayOrQueue(string animationName) {
    if (_animation.isPlaying) {
      _animation.PlayQueued(animationName);
    } else {
      _animation.Play(animationName);
    }
  }
}
