using System.Collections.Generic;
using UnityEngine;

namespace Steelmage {
  public enum TurnType {
    None,
    WalkForward,
    Walk90Left,
    Walk90Right,
    Walk180
  }

  public class MovementManager : MonoBehaviour {
    private Animator _animator;
    private List<GGCell> _currentPath;
    private GGCell _currentTarget;
    public GGObject GridObject;
    private TurnType _currentTurn;
    public float Walk180Nudge = 0.22f;
    public float Walk90Nudge = 0.11f;

    private void Start() {
      _animator = GridObject.GetComponent<Animator>();
      _currentTurn = TurnType.None;
    }

    // Update is called once per frame
    private void Update() {
      if (Input.GetMouseButtonDown(0)) {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var cell = GGGrid.GetCellFromRay(ray, 1000f);
        _currentPath = GGAStar.GetPath(GridObject.Cell, cell, false);
        if (_currentPath.Count > 0) {
          // Remove start position from path
          _currentPath.RemoveAt(0);
        }
      }

      if (_currentTarget != null) {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLoop")) {
          _currentTurn = TurnType.None;
        }

        if (_currentTurn == TurnType.Walk180) {
          var targetRotation = Quaternion.LookRotation(_currentTarget.CenterPoint3D - GridObject.transform.position);
          GridObject.transform.rotation = Quaternion.Lerp(GridObject.transform.rotation, targetRotation, Time.deltaTime * Walk180Nudge);
        }

        if (_currentTurn == TurnType.Walk90Right || _currentTurn == TurnType.Walk90Left) {
          var targetRotation = Quaternion.LookRotation(_currentTarget.CenterPoint3D - GridObject.transform.position);
          GridObject.transform.rotation = Quaternion.Lerp(GridObject.transform.rotation, targetRotation, Time.deltaTime * Walk90Nudge);
        }

        if ((GridObject.transform.position - _currentTarget.CenterPoint3D).magnitude < 0.1f) {
          Debug.Log("Reached target: " + _currentTarget.CenterPoint3D);
          _currentTarget = null;
        }
      } else if (_currentPath != null) {
        _currentTarget = _currentPath[0];
        var targetRotation = Quaternion.LookRotation(_currentTarget.CenterPoint3D - GridObject.transform.position);
        var angle = (360 + targetRotation.eulerAngles.y - GridObject.transform.rotation.eulerAngles.y)%360;
        Debug.Log("angle " + angle);

        if (angle < 45 || angle >= 315) {
          Debug.Log("Walk Start Trigger");
          _animator.SetTrigger("WalkStart");
          _currentTurn = TurnType.WalkForward;
        }
        else if (angle < 135) {
          Debug.Log("Walk Right Trigger");
          _animator.SetTrigger("Walk90RightStart");
          _currentTurn = TurnType.Walk90Right;
        }
        else if (angle < 225) {
          Debug.Log("Walk 180 Trigger");
          _animator.SetTrigger("Walk180Start");
          _currentTurn = TurnType.Walk180;
        }
        else if (angle < 315) {
          Debug.Log("Walk Left Trigger");
          _animator.SetTrigger("Walk90LeftStart");
          _currentTurn = TurnType.Walk90Left;
        }

        _currentPath.RemoveAt(0);
        if (_currentPath.Count == 0) {
          _currentPath = null;
        }
      }
    }
  }
}