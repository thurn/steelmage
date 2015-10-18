using System.Collections.Generic;
using UnityEngine;

namespace Steelmage {
  public class Hand : MonoBehaviour {
    private static Hand _instance;
    public List<Card> Cards;

    public static Hand Instance {
      get { return _instance ?? (_instance = FindObjectOfType<Hand>()); }
    }

    public void AddCard(Card card) {
      //foreach (var currentCard in Cards) {
      //  LeanTween.moveX(currentCard.gameObject, currentCard.transform.position.x + 80, 0.3f);
      //}

      Cards.Add(card);
    }
  }
}