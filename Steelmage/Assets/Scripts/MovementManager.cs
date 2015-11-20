using UnityEngine;
using Vectrosity;

namespace Steelmage {
  public class MovementManager : MonoBehaviour {
    public GGGrid Grid;
    private GGCell _hoverCell;
    private VectorLine _line;

    // Use this for initialization
    private void Start() {}

    private void Update() {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      var cell = GGGrid.GetCellFromRay(ray, 1000f);
      if (!ReferenceEquals(cell, _hoverCell)) {
        if (cell != null) {
          var linePoints = new Vector3[60];
          VectorLine.Destroy(ref _line);
          _line = new VectorLine("target", linePoints, null, 2.0f, LineType.Continuous) {
            color = Color.yellow
          };
          _line.MakeCircle(cell.CenterPoint3D, Vector3.up, 0.9f);
          _line.Draw3DAuto();
        }
        _hoverCell = cell;
      }
    }
  }
}