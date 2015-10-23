using DG.Tweening;
using UnityEngine;

namespace Steelmage {
  public class DrawCard : MonoBehaviour {
    private static DrawCard _instance;
    public Card CardPrefab;
    public Transform ShowCardPosition;

    public static DrawCard Instance {
      get { return _instance ?? (_instance = FindObjectOfType<DrawCard>()); }
    }

    public void Draw(Vector3 deckPosition) {
      var card = Instantiate(CardPrefab);
      card.transform.SetParent(transform);
      card.transform.localScale = Vector3.one;
      card.transform.position = deckPosition;
      card.transform.SetAsLastSibling();
      DOTween.Sequence()
        .Append(card.transform.DOMove(new Vector2(0, 100), 0.5f).SetRelative())
        .Append(DOTween.Sequence()
          .Append(card.transform.DOMove(ShowCardPosition.position, 1.0f))
          .Insert(0, card.transform.DOScale(new Vector3(3, 3, 1), 1.0f))
          .Insert(0, card.transform.DORotate(new Vector3(0, 90, 0), 0.5f))
          .Insert(0.5f, card.transform.DORotate(Vector3.zero, 0.5f)))
        .Append(DOTween.Sequence()
          .Append(card.transform.DOMove(transform.position, 1.0f))
          .Insert(0, card.transform.DOScale(Vector3.one, 1.0f)));
    }
  }
}