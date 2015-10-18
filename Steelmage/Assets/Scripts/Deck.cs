using UnityEngine;

namespace Steelmage {
  public class Deck : MonoBehaviour {
    public void DrawCard() {
      Steelmage.DrawCard.Instance.Draw(transform.position);
    }
  }
}