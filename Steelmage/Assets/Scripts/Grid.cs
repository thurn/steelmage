using UnityEngine;
using Vectrosity;

public class Grid : MonoBehaviour {
  // Use this for initialization
  private void Start() {
    VectorLine.SetLine(Color.green, new Vector2(0, 0), new Vector2(Screen.width - 1, Screen.height - 1));
  }

  // Update is called once per frame
  private void Update() {}
}