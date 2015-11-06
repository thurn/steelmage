using UnityEngine;

public class Gladiator2 : MonoBehaviour {
  private Animator _animator;
  public SkinnedMeshRenderer Rifle;
  public Transform RifleMuzzle;

  public void Start() {
    _animator = GetComponent<Animator>();
  }

  public void Update() {
    if (Input.GetKeyDown(KeyCode.V)) {
      _animator.SetTrigger("Walk");
    } else if (Input.GetKeyDown(KeyCode.B)) {
      _animator.SetTrigger("Cast");
    } else if (Input.GetKeyDown(KeyCode.N)) {
      _animator.SetTrigger("Draw");
    } else if (Input.GetKeyDown(KeyCode.L)) {
      _animator.SetTrigger("Idle");
    } else if (Input.GetKeyDown(KeyCode.M)) {
      _animator.SetTrigger("Shoot");
    } else if (Input.GetKeyDown(KeyCode.H)) {
      _animator.SetTrigger("Holster");
    }
  }

  public void DrawWeapon() {
    Debug.Log("Draw Weapon");
    Rifle.enabled = true;
  }

  public void HolsterWeapon() {
    Rifle.enabled = false;
  }

  public void FireRifle() {
    F3DController.Instance.Fire(F3DEffectType.Vulcan, RifleMuzzle);
  }
}
