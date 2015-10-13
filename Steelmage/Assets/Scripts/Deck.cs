﻿using UnityEngine;

namespace Steelmage {
  public class Deck : MonoBehaviour {
    private Animation _animation;

    public void Start() {
      _animation = GetComponent<Animation>();
    }

    public void DrawCard() {
      Debug.Log("Draw Card " + Screen.width + "x" + Screen.height);
      _animation.Play("drawCard");
    }
  }
}