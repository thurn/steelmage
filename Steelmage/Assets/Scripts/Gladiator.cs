using UnityEngine;

enum GladiatorState {
  Walking,
  Shooting,
  Idle
}

enum TransitionType {
  Normal,
  Trigger
}

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animation))]
public class Gladiator : MonoBehaviour {
  public Transform GunPosition;
  public float MoveSpeed = 6.0f;
  public float RotationSpeed = 15.0f;
  // Begin slowing down when closer to the target than this threshold:
  public float TargetDistanceThreshold = 2.0f;
  public float DecelerationSpeed = 20.0f;

  private Animation _animation;
  private CharacterController _characterController;
  private bool _running;
  private Vector3? _moveTargetPosition;
  private float _currentSpeed;

  void Start() {
    _animation = GetComponent<Animation>();
    _characterController = GetComponent<CharacterController>();
    _animation.Play("guns_idle");
  }

  public void FireRifle() {
    F3DController.Instance.Fire(F3DEffectType.Vulcan, GunPosition);
  }

  void Update() {
    var movement = Vector3.zero;

    if (Input.GetMouseButton(0)) {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit mouseHit;
      if (Physics.Raycast(ray, out mouseHit)) {
        var hitObject = mouseHit.transform.gameObject;
        if (hitObject.layer == LayerMask.NameToLayer("Ground")) {
          _moveTargetPosition = mouseHit.point;
          _currentSpeed = MoveSpeed;
        }
      }
    }

    if (_moveTargetPosition != null) {
      var targetPosition = (Vector3)_moveTargetPosition;
			var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation,
          targetRotation,
          RotationSpeed * Time.deltaTime);

			movement = _currentSpeed * Vector3.forward;
			movement = transform.TransformDirection(movement);
			
			if (Vector3.Distance(targetPosition, transform.position) < TargetDistanceThreshold) {
				_currentSpeed -= DecelerationSpeed * Time.deltaTime;
				if (_currentSpeed <= 0) {
				  _moveTargetPosition = null;
				}
			}
    }

    movement *= Time.deltaTime;
    _characterController.Move(movement);

    if (Input.GetKeyDown(KeyCode.Space)) {
      _animation.CrossFade(_running ? "guns_idle" : "guns_walk_loop");
      _running = !_running;
    }

    if (Input.GetKeyDown(KeyCode.Mouse0)) {
      _animation.CrossFade("shot_rifle");
    }
  }
}
