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
          if (!cell.IsPathable || cell.IsOccupied) continue;
          var pointArray = new[] {
            new Vector3(cell.MinPoint3D.x, cell.MinPoint3D.y, cell.MinPoint3D.z),
            new Vector3(cell.MaxPoint3D.x, cell.MinPoint3D.y, cell.MinPoint3D.z), 
            new Vector3(cell.MaxPoint3D.x, cell.MinPoint3D.y, cell.MaxPoint3D.z), 
            new Vector3(cell.MinPoint3D.x, cell.MinPoint3D.y, cell.MaxPoint3D.z),
            new Vector3(cell.MinPoint3D.x, cell.MinPoint3D.y, cell.MinPoint3D.z)
          };

          var color = new Color(1.0f, 1.0f, 1.0f, 0.15f);

          var points = new VectorLine("Points", pointArray, null, 2.0f, LineType.Continuous) {
              color = color
          };
          points.Draw3DAuto();
        }
      }
    }
    private void Update() {}
  }
}
