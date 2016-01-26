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
    public Transform Target2;
    private int _inputAngleResetCounter;
    private bool _target2;

    private void Start() {
      _animator = GetComponent<Animator>();
      _animationState = AnimationState.Standing;
    }

    // Update is called once per frame
    private void Update() {
      if (Input.GetKeyDown(KeyCode.N)) {
        _animator.SetFloat("WalkStartAngle", AngleToTarget(transform, Target));
        _animator.SetFloat("InputMagnitude", 0.5f);
      }

      if (Input.GetKeyDown(KeyCode.M)) {
        _target2 = true;
      }

      if (_target2 && Vector3.Distance(transform.position, Target2.position) > 5.0f) {
        _animator.SetFloat("InputAngle", AngleToTarget(transform, Target2));
      }
    }

    private static float AngleToTarget(Transform source, Transform target) {
      var relativePosition = target.position - source.position;
      var lookRotation = Quaternion.LookRotation(relativePosition, Vector3.up);
      var walkStartAngle = NormalizeAngle(lookRotation.eulerAngles.y);
      var currentRotation = NormalizeAngle(source.rotation.eulerAngles.y);
      return NormalizeAngle(walkStartAngle - currentRotation);      
    }

    private static float NormalizeAngle(float angle) {
      if (Mathf.Abs(angle) < 2.5) return 0;
      if (Mathf.Abs(angle - 360) < Mathf.Abs(angle)) return angle - 360;
      if (Mathf.Abs(angle + 360) < Mathf.Abs(angle)) return angle + 360;
      return angle;
    }
  }
}