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
        _animator.SetFloat("InputMagnitude", 0.5f);

      }
      var relativePosition = Target.position - transform.position;
      var targetRotation = Quaternion.LookRotation(relativePosition, Vector3.up);
      var deltaRotation = Quaternion.FromToRotation(transform.forward, relativePosition);
      var walkStartAngle = NormalizeAngle(deltaRotation.eulerAngles.y);
      Debug.Log("walkStartAngle " + walkStartAngle);
      _animator.SetFloat("WalkStartAngle", walkStartAngle);
    }

    private static float NormalizeAngle(float angle) {
      if (Mathf.Abs(angle) < 5) return 0;
      if (Mathf.Abs(angle - 360) < Mathf.Abs(angle)) return angle - 360;
      return angle;
    }
  }
}