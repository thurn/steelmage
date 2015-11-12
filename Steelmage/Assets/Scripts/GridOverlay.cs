using UnityEngine;
using System.Collections;
using Vectrosity;

namespace Steelmage {
  public class GridOverlay : MonoBehaviour {
    private GGGrid _grid;

    private void Start() {
      _grid = GetComponent<GGGrid>();

      for (var x = 0; x < _grid.GridWidth; ++x) {
        for (var y = 0; y < _grid.GridHeight; ++y) {
          var cell = _grid.Cells[x, y];
          var pointArray = new[] {cell.MinPoint3D, cell.CenterPoint3D};
          var points = new VectorLine("Points", pointArray, null, 2.0f) {color = Color.green};
          points.Draw3DAuto();
        }
      }
    }
    private void Update() {}
  }
}
