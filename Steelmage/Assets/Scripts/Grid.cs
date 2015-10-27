using UnityEngine;
using Vectrosity;

public class Grid : MonoBehaviour {
  // Use this for initialization
  private void Start() {
    VectorLine.SetLine3D(Color.green,
      transform.position,
      transform.up * 5,
      transform.right * 5);
    Debug.Log("Posi " + transform.position);
  }

  // Update is called once per frame
  private void Update() {}
}