using UnityEngine;
using UnityEngine.UI;

namespace Steelmage {
  public class Deck : MonoBehaviour {
    public Sprite CardFront;

    private Animation _animation;
    private Image _image;

    public void Start() {
      _animation = GetComponent<Animation>();
      _image = GetComponent<Image>();
    }

    public void Flip() {
      _image.sprite = CardFront;
    }

    public void DrawCard() {
      Debug.Log("Draw Card " + Screen.width + "x" + Screen.height);
      _animation.Play("drawCard");
    }
  }
}