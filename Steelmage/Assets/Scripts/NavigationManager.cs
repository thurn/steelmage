using UnityEngine;

public class NavigationManager : MonoBehaviour {
  public Transform Target;
  private NavMeshAgent _agent;

  private void Start() {
    _agent = GetComponent<NavMeshAgent>();
  }

  private void Update() {
    _agent.SetDestination(Target.position);
  }
}