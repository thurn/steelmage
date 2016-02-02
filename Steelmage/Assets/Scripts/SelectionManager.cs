using UnityEngine;
using Vectrosity;

public class SelectionManager : MonoBehaviour {
  public GGObject SelectedGridObject;
  private VectorLine _line;

  private void Update() {
    // TODO: Investigate hierarchy flicker
    VectorLine.Destroy(ref _line);
    var linePoints = new Vector3[8];
    _line = new VectorLine("selected", linePoints, null, 4.0f, LineType.Continuous) {
      color = Color.green
    };
    _line.MakeCircle(SelectedGridObject.CachedTransform.position, Vector3.up, 1.0f);
    _line.Draw3DAuto();
  }

}