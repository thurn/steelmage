using UnityEngine;

public class NavigationManager : MonoBehaviour {
  public Transform Target;
  private NavMeshAgent _agent;
  private Animator _animator;
  private Vector2 _smoothDeltaPosition = Vector2.zero;
  private Vector2 _velocity = Vector2.zero;

  private void Start() {
    _agent = GetComponent<NavMeshAgent>();
    _animator = GetComponent<Animator>();
    _agent.updatePosition = false;
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.P)) {
      _animator.SetTrigger("WalkStart");
      _agent.SetDestination(Target.position);
      _animator.SetFloat("vely", 1.0f);
      Debug.Log("WalkStart");
    }

    var worldDeltaPosition = _agent.nextPosition - transform.position;

    // Map "worldDeltaPosition" to local space
    var deltaX = Vector3.Dot(transform.right, worldDeltaPosition);
    var deltaY = Vector3.Dot(transform.forward, worldDeltaPosition);
    var deltaPosition = new Vector2(deltaX, deltaY);

    // Low-pass filter the delta move
    var smooth = Mathf.Min(1.0f, Time.deltaTime/0.15f);
    _smoothDeltaPosition = Vector3.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

    // Update velocity if time advances
    if (Time.deltaTime > 1e-5f) {
      _velocity = _smoothDeltaPosition/Time.deltaTime;
    }
    var shouldMove = _velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.radius;
 
    _animator.SetBool("move", shouldMove);
    _animator.SetFloat("velx", _velocity.x);
    _animator.SetFloat("vely", _velocity.y);
    Debug.Log("magnitude? " + _velocity.magnitude + " distance " + _agent.remainingDistance + " velocity " + _velocity);
  }
}