using UnityEngine;
using System.Collections;

public class Gladiator2 : MonoBehaviour {
  private Animator _animator;

  void Start() {
    _animator = GetComponent<Animator>();
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.V)) {
      _animator.SetTrigger("Walk");
    }
  }
}
