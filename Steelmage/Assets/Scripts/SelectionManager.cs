using UnityEngine;
using Vectrosity;

public class SelectionManager : MonoBehaviour {
  public Transform DefaultSelected;

  private void Start() {
    var linePoints = new Vector3[60];
    var line = new VectorLine("selected", linePoints, null, 2.0f, LineType.Continuous) {
      color = Color.green
    };
    line.MakeCircle(DefaultSelected.position, Vector3.up, 1.0f);
    line.Draw3DAuto();
  }

  // Update is called once per frame
  private void Update() {}
}