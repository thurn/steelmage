using UnityEngine;

namespace Steelmage {
  public enum AnimationState {
    Standing,
    StartingWalking,
    Walking
  }

  public class AnimationController : MonoBehaviour {
    private AnimationState _animationState;
    private Animator _animator;
    private int _inputAngleResetCounter;
    private NavMeshAgent _navMeshAgent;
    private Transform _target;
    private bool _target2;
    public Transform Target2;

    private void Start() {
      _animator = GetComponent<Animator>();
      _animationState = AnimationState.Standing;
      _navMeshAgent = GetComponent<NavMeshAgent>();
      _navMeshAgent.updatePosition = false;
    }

    // Update is called once per frame
    private void Update() {
      switch (_animationState) {
        case AnimationState.Walking:
          if (Vector3.Distance(transform.position, _target.position) < 2.0f) {
            _animator.SetFloat("InputMagnitude", 0.0f);
            _animator.SetFloat("InputAngle", 0.0f);
            _animationState = AnimationState.Standing;
          }
          else {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = _navMeshAgent.nextPosition;
            sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            _animator.SetFloat("InputAngle", AngleToTarget(transform, _navMeshAgent.nextPosition));
          }
          break;
        case AnimationState.StartingWalking:
          _animator.SetFloat("WalkStartAngle", AngleToTarget(transform, _navMeshAgent.nextPosition));
          _animator.SetFloat("InputMagnitude", 0.5f);
          _inputAngleResetCounter++;
          if (_inputAngleResetCounter == 10) {
            _animationState = AnimationState.Walking;
            _inputAngleResetCounter = 0;
          }
          break;
      }

      if (Input.GetKeyDown(KeyCode.M)) {
        _navMeshAgent.nextPosition = transform.position;
        _navMeshAgent.SetDestination(Target2.position);
        _target = Target2;
        _animationState = AnimationState.StartingWalking;
      }
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