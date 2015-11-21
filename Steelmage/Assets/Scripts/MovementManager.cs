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
          Debug.Log("Path length " + path.Count);
          foreach (var pathCell in path) {
            var line = new VectorLine("path", new Vector3[60], null, 2.0f, LineType.Continuous) {
              color = pathCell.IsPathable ? Color.yellow : Color.red
            };
            line.MakeCircle(pathCell.CenterPoint3D, Vector3.up, 1.0f);
            line.Draw3DAuto();
            _lines.Add(line);
          }
        }
        _hoverCell = cell;
      }
    }
  }
}