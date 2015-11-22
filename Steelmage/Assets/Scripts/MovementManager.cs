using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

namespace Steelmage {
  public class MovementManager : MonoBehaviour {
    private GGCell _hoverCell;
    //private VectorLine _line;
    private List<VectorLine> _lines;
    private SelectionManager _selectionManager;
    public GGGrid Grid;
    // Use this for initialization
    void Start() {
      _lines = new List<VectorLine>();
      _selectionManager = GetComponent<SelectionManager>();
    }

    void Update() {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      var cell = GGGrid.GetCellFromRay(ray, 1000f);
      if (!ReferenceEquals(cell, _hoverCell)) {
        if (cell != null && _selectionManager.SelectedGridObject != null) {
          VectorLine.Destroy(_lines);
          var path = GGAStar.GetPath(_selectionManager.SelectedGridObject.Cell,
            cell, false);
          for (var i = 0; i < path.Count - 1; ++i) {
            var points = new[] {path[i].CenterPoint3D, path[i+1].CenterPoint3D};
            var line = new VectorLine("path", points, null, 2.0f, LineType.Continuous) {
              color = Color.yellow
            };
            line.Draw3DAuto();
            _lines.Add(line);
          }
        }
        _hoverCell = cell;
      }
    }
  }
}