using UnityEngine;
using UnityEngine.UI;

namespace Steelmage {
  public class Card : MonoBehaviour {
    public Sprite CardFront;

    private Animation _animation;
    private Image _image;

    public void Awake() {
      _animation = GetComponent<Animation>();
      _image = GetComponent<Image>();
    }

    public void Flip() {
      _image.sprite = CardFront;
    }

    public void Draw() {
      _animation.Play("drawCard");
    }                     
  }
}