using UnityEngine;
using System.Collections.Generic;

public class MovementManager : MonoBehaviour {
  public GGObject GridObject;
  private List<GGCell> _currentPath;
  private GGCell _currentTarget;

  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      var cell = GGGrid.GetCellFromRay(ray, 1000f);
      _currentPath = GGAStar.GetPath(GridObject.Cell, cell, false);
      _currentTarget = null;
      if (_currentPath.Count > 0) {
        // Remove start position from path
        _currentPath.RemoveAt(0);
      }
    }

    if (_currentTarget != null) {
      var target = _currentTarget.CenterPoint3D;
      var targetRotation = Quaternion.LookRotation(target - GridObject.transform.position);
      GridObject.transform.rotation = Quaternion.Lerp(GridObject.transform.rotation, targetRotation, Time.deltaTime * 3.0f);
    } else if (_currentPath != null) {
      _currentTarget = _currentPath[0];
      Debug.Log("New target: " + _currentTarget.CenterPoint3D);
      Debug.Log("Current Position " + GridObject.transform.position);
      _currentPath.RemoveAt(0);
      if (_currentPath.Count == 0) {
        _currentPath = null;
      }
    }
  }
}
