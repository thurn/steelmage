using UnityEngine;

public sealed class F3DPrefabs : MonoBehaviour {
  [Header("Vulcan")]
  public Transform VulcanProjectile;
  public Transform VulcanMuzzle;
  public Transform VulcanImpact;

  [Header("Solo gun")]
  public Transform SoloGunProjectile;
  public Transform SoloGunMuzzle;
  public Transform SoloGunImpact;

  [Header("Sniper")]
  public Transform SniperBeam;
  public Transform SniperMuzzle;
  public Transform SniperImpact;

  [Header("Shotgun")]
  public Transform ShotGunProjectile;
  public Transform ShotGunMuzzle;
  public Transform ShotGunImpact;

  [Header("Seeker")]
  public Transform SeekerProjectile;
  public Transform SeekerMuzzle;
  public Transform SeekerImpact;

  [Header("Rail gun")]
  public Transform RailgunBeam;
  public Transform RailgunMuzzle;
  public Transform RailgunImpact;

  [Header("Plasma gun")]
  public Transform PlasmagunProjectile;
  public Transform PlasmagunMuzzle;
  public Transform PlasmagunImpact;

  [Header("Plasma beam")]
  public Transform PlasmaBeam;

  [Header("Plasma beam heavy")]
  public Transform PlasmaBeamHeavy;

  [Header("Lightning gun")]
  public Transform LightningGunBeam;

  [Header("Flame")]
  public Transform FlameRed;

  [Header("Laser impulse")]
  public Transform LaserImpulseProjectile;
  public Transform LaserImpulseMuzzle;
  public Transform LaserImpulseImpact;

  public static F3DPrefabs Instance { get; private set; }

  public void Awake() {
    Instance = this;
  }
}
