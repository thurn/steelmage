using UnityEngine;

namespace Steelmage {
  public class DrawCard : MonoBehaviour {
    private static DrawCard _instance;
    public static DrawCard Instance {
      get { return _instance ?? (_instance = FindObjectOfType<DrawCard>()); }
    }

    public Card CardPrefab;

    public void Draw(Vector3 deckPosition) {
      var card = Instantiate(CardPrefab);
      card.transform.SetParent(transform);
      card.transform.localScale = Vector3.one;
      card.transform.position = deckPosition;
      card.transform.SetAsLastSibling();
      LeanTween.move(card.gameObject, new Vector2(400, 400), 5.0f);
    }
  }
}