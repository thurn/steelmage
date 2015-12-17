using UnityEngine;

namespace Steelmage {
  public enum AnimationState {
    Standing,
    StartingWalking,
    WalkingWithTurning,
    WalkingWithoutTurning,
    StoppingLeftFootUp,
    StoppingRightFootUp
  }

  public class AnimationController : MonoBehaviour {
    private AnimationState _animationState;
    private Animator _animator;
    public Transform Target;

    private void Start() {
      _animator = GetComponent<Animator>();
      _animationState = AnimationState.Standing;
    }

    // Update is called once per frame
    private void Update() {
      if (Input.GetKeyDown(KeyCode.N)) {
        var relativePosition = Target.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        var walkStartAngle = NormalizeAngle(rotation.eulerAngles.y);
        Debug.Log("walkStartAngle " + walkStartAngle);
        _animator.SetFloat("WalkStartAngle", walkStartAngle);
        _animator.SetFloat("InputMagnitude", 0.5f);
      }
    }

    private static float NormalizeAngle(float angle) {
      if (Mathf.Abs(angle - 360) < Mathf.Abs(angle)) return angle - 360;
      return angle;
    }
  }
}