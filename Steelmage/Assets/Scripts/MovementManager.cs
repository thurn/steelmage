using System.Collections.Generic;
using UnityEngine;

namespace Steelmage {

  public class MovementManager : MonoBehaviour {
    private AnimationController _animationController;

    public void Start() {
      _animationController = GetComponent<AnimationController>();
    }
 
    public void Update() {
      if (Input.GetMouseButtonDown(0)) {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var cell = GGGrid.GetCellFromRay(ray, 1000f);
        _animationController.WalkToTarget(cell.CenterPoint3D);
      }
    }
  }
}