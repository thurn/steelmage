using UnityEngine;

namespace Steelmage {
  public class Deck : MonoBehaviour {
    public Card CardPrefab;
    public void Start() {}

    public void DrawCard() {
      var card = Canvas.Instance.InstantiateObject<Card>(CardPrefab, transform.position);
      card.Draw();
      Hand.Instance.AddCard(card);
    }
  }
}