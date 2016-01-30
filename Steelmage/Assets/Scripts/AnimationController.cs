using UnityEngine;

namespace Steelmage {
  public enum AnimationState {
    Standing,
    StartingWalking,
    StoppingWalking,
    Walking
  }

  public class AnimationController : MonoBehaviour {
    private AnimationState _animationState;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private int _startWalkingCounter;
    private Vector3 _target;
    private bool _first = true;

    public void Start() {
      _animator = GetComponent<Animator>();
      _animationState = AnimationState.Standing;
      _navMeshAgent = GetComponent<NavMeshAgent>();
      _navMeshAgent.updatePosition = false;
    }

    public void Update() {
      switch (_animationState) {
        case AnimationState.Walking:
          _animator.SetFloat("InputAngle", AngleToTarget(transform, _navMeshAgent.nextPosition));
            _animator.SetFloat("InputMagnitude", 0.5f);
          if (Vector3.Distance(transform.position, _target) < 1.5f) {
            _animationState = AnimationState.StoppingWalking;
          }
          break;
        case AnimationState.StartingWalking:
          if (_startWalkingCounter == 1) {
            _animator.SetFloat("InputMagnitude", 0.25f);
            _animator.SetFloat("WalkStartAngle", AngleToTarget(transform, _navMeshAgent.nextPosition));
            // This is frequently way off
            Debug.Log("Walk Start Angle " + _animator.GetFloat("WalkStartAngle"));
          }
          _first = false;
          _startWalkingCounter++;
          if (_startWalkingCounter == 5) {
            // Stay in the StartWalking state for X frames to prevent jerky motion
            _animationState = AnimationState.Walking;
            _startWalkingCounter = 0;
          }
          break;
        case AnimationState.StoppingWalking:
          _animator.SetFloat("InputAngle", 0.0f);
            _animator.SetFloat("InputMagnitude", 0.25f);
          if (Vector3.Distance(transform.position, _target) < 1.0f) {
            _animationState = AnimationState.Standing;
          }
          break;
        case AnimationState.Standing:
          _animator.SetFloat("InputMagnitude", 0.0f);
          break;
      }
    }

    public void WalkToTarget(Vector3 target) {
      _navMeshAgent.nextPosition = transform.position;
      _navMeshAgent.SetDestination(target);
      _target = target;
      _animationState = AnimationState.StartingWalking;
    }

    private static float AngleToTarget(Transform source, Vector3 targetPosition) {
      var relativePosition = targetPosition - source.position;
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