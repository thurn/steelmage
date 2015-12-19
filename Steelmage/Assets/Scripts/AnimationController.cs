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
        var lookRotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        var walkStartAngle = NormalizeAngle(lookRotation.eulerAngles.y);
        var currentRotation = NormalizeAngle(transform.rotation.eulerAngles.y);
        var targetRotation = NormalizeAngle(walkStartAngle - currentRotation);
        Debug.Log("walkStartAngle " + walkStartAngle + " current rotation " + currentRotation + " ws-cur " + targetRotation);
        _animator.SetFloat("WalkStartAngle", targetRotation);
        _animator.SetFloat("InputMagnitude", 0.5f);
      }

      //var relativePosition = Target.position - transform.position;
      //var lookRotation = Quaternion.LookRotation(relativePosition, Vector3.up);
      //var targetAngle = Quaternion.Angle(transform.rotation, lookRotation);
      //Debug.Log("transform rotation " + transform.rotation.eulerAngles.y + " look rotation " + 
      //  lookRotation.eulerAngles.y + " target " + targetAngle);
      //var walkStartAngle = NormalizeAngle(lookRotation.eulerAngles.y);
      //_animator.SetFloat("WalkStartAngle", walkStartAngle);
    }

    private static float NormalizeAngle(float angle) {
      if (Mathf.Abs(angle) < 2.5) return 0;
      if (Mathf.Abs(angle - 360) < Mathf.Abs(angle)) return angle - 360;
      if (Mathf.Abs(angle + 360) < Mathf.Abs(angle)) return angle + 360;
      return angle;
    }
  }
}