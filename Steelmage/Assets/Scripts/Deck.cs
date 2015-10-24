using UnityEngine;

namespace Steelmage {
  public class Deck : MonoBehaviour {
    public Sprite IceSprite;
    public Sprite WoodSprite;
    public Sprite LavaSprite;
    public Sprite SciFiSprite;

    public void DrawCard() {
      Steelmage.DrawCard.Instance.Draw(transform.position, IceSprite);
    }
  }
}