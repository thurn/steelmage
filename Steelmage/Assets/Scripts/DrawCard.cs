using DG.Tweening;
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
      DOTween.Sequence()
        .Append(card.transform.DOMove(new Vector2(0, 100), 0.5f).SetRelative())
        .Append(card.transform.DOMove(transform.position, 3.0f));
    }
  }
}