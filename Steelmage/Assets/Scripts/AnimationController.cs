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
    private Vector3 _target;

    public void Start() {
      _animator = GetComponent<Animator>();
      _animationState = AnimationState.Standing;
      _navMeshAgent = GetComponent<NavMeshAgent>();
      _navMeshAgent.updatePosition = false;
      _navMeshAgent.updateRotation = false;
    }

    public void Update() {
      switch (_animationState) {
        case AnimationState.Walking:
          if (Vector3.Distance(transform.position, _target) < 2.5f) {
            _animationState = AnimationState.StoppingWalking;
          }
          else {
            _animator.SetFloat("InputAngle", AngleToTarget(transform, _navMeshAgent.nextPosition));
            _animator.SetFloat("InputMagnitude", 0.5f);            
          }
          break;
        case AnimationState.StartingWalking:
          if (!_navMeshAgent.pathPending) {
            NavMeshHit hit;
            _navMeshAgent.SamplePathPosition(NavMesh.AllAreas, 1.0f, out hit);
            _animator.SetFloat("WalkStartAngle", AngleToTarget(transform, hit.position));
            _animator.SetFloat("InputMagnitude", 0.5f);   
            _animationState = AnimationState.Walking;
          }
          break;
        case AnimationState.StoppingWalking:
          _animator.SetFloat("InputAngle", 0.0f);
          _animator.SetFloat("InputMagnitude", 0.5f);
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