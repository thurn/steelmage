using UnityEngine;

public class Navigator : MonoBehaviour {
  private NavMeshAgent _agent;
  private int _numWalks;
  private Vector2 _smoothDeltaPosition;
  private Vector2 _velocity = Vector2.zero;
  private bool _walking;
  public float HorizontalMoveMagnitude = 0.0f;
  public Vector3 MoveDirection = Vector3.zero;
  public float MoveMagnitude = 0.0f;
  public Transform Target;
  public float VerticalMoveMagnitude = 0.0f;

  private void Start() {
    _agent = GetComponent<NavMeshAgent>();
    _agent.updatePosition = false;
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.P)) {
      _agent.SetDestination(Target.position);
      Debug.Log("WalkStart");
    }
 
    Debug.Log("nextPosition " + _agent.nextPosition.x + "," + _agent.nextPosition.y + "," + _agent.nextPosition.z);

    if (!_walking) return;

    Debug.Log("======================================");
    Debug.Log("position " + transform.position.x + "," + transform.position.y + "," + transform.position.z);
    Debug.Log("nextPosition " + _agent.nextPosition.x + "," + _agent.nextPosition.y + "," + _agent.nextPosition.z);
    var worldDeltaPosition = _agent.nextPosition - transform.position;
    Debug.Log("worldDeltaPosition " + worldDeltaPosition.x + "," + worldDeltaPosition.y + "," + worldDeltaPosition.z);

    // Map "worldDeltaPosition" to local space
    var deltaX = Vector3.Dot(transform.right, worldDeltaPosition);
    var deltaY = Vector3.Dot(transform.forward, worldDeltaPosition);
    var deltaPosition = new Vector2(deltaX, deltaY);
    Debug.Log("deltaPosition " + deltaPosition.x + "," + deltaPosition.y);

    // Low-pass filter the delta move
    var smooth = Mathf.Min(1.0f, Time.deltaTime/0.15f);
    _smoothDeltaPosition = Vector3.Lerp(_smoothDeltaPosition, deltaPosition, smooth);
    Debug.Log("smoothDeltaPosition " + _smoothDeltaPosition.x + "," + _smoothDeltaPosition.y);

    // Update velocity if time advances
    if (Time.deltaTime > 1e-5f) {
      _velocity = _smoothDeltaPosition/Time.deltaTime;

      Debug.Log("_velocity " + _velocity);
      _numWalks++;
      if (_numWalks > 5) _walking = false;
    }
  }
}