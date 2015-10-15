using UnityEngine;

namespace Steelmage {
  public class Canvas : MonoBehaviour {
    private static Canvas _instance;

    public static Canvas Instance {
      get {
        if (_instance == null) {
          _instance = FindObjectOfType<Canvas>();
        }
        return _instance;
      }
    }

    public T InstantiateObject<T>(Object newObject, Vector3 position) where T : class {
      var value = (MonoBehaviour)Instantiate(newObject, position, Quaternion.identity);
      value.transform.parent = transform;
      return value as T;
    }
  }
}