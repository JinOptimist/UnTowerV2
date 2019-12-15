using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Utility
{
    public class InterfaceUtility
    {
        public static MonoBehaviour GetMonoBehaviourImplementTheInterface<T>(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return null;
            }

            var monoBehaviours = gameObject.GetComponents<MonoBehaviour>();
            foreach (var monoBehaviour in monoBehaviours)
            {
                var currentScriptHasT = monoBehaviour.GetType().GetInterface(typeof(T).Name);
                if (currentScriptHasT != null)
                {
                    return monoBehaviour;
                }
            }

            return null;
        }

        public static bool IsImplementTheInterface<T>(GameObject gameObject)
        {
            return GetMonoBehaviourImplementTheInterface<T>(gameObject) != null;
        }

        public static IEnumerable<GameObject> FindObjectsImplementOfInterface<T>(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                if (IsImplementTheInterface<T>(gameObject))
                {
                    yield return gameObject;
                }
            }
        }
    }
}
