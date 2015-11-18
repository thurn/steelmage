using UnityEngine;

public class CellSelector : MonoBehaviour {
  private GGCell _hoverCell;

  private void Update() {
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    var cell = GGGrid.GetCellFromRay(ray, 1000f);
    if (cell != _hoverCell) {
      if (cell != null) {
        Debug.Log("Hovering Cell: " + cell.GridX + "," + cell.GridY);
      }
      _hoverCell = cell;
    }
  }
}
