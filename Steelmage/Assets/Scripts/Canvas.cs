using UnityEngine;

namespace Steelmage {
  public class Canvas : MonoBehaviour {
    private static Canvas s_instance;

    public static Canvas Instance {
      get { return Canvas.s_instance ?? (Canvas.s_instance = Object.FindObjectOfType<Canvas>()); }
    }

    public T InstantiateObject<T>(Object newObject, Vector3 position) where T : class {
      var value = (MonoBehaviour)Object.Instantiate(newObject, position, Quaternion.identity);
      value.transform.SetParent(this.transform, false /* worldPositionStays */);
      value.transform.SetAsFirstSibling();
      return value as T;
    }
  }
}