using UnityEngine;

namespace Steelmage {
  public class Canvas : MonoBehaviour {
    private static Canvas _instance;

    public static Canvas Instance {
      get { return _instance ?? (_instance = FindObjectOfType<Canvas>()); }
    }

    public T InstantiateObject<T>(Object newObject, Vector3 position) where T : class {
      var value = (MonoBehaviour)Instantiate(newObject, position, Quaternion.identity);
      value.transform.SetParent(transform, false /* worldPositionStays */);
      value.transform.SetAsFirstSibling();
      return value as T;
    }
  }
}