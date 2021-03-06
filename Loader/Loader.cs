using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverAI
{
    public class Loader
    {
        static UnityEngine.GameObject gameObject;
        public static void Load()
        {
            gameObject = new UnityEngine.GameObject();
            gameObject.AddComponent<Consolation.Console>();
            gameObject.AddComponent<MainLogic>();
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }

        public static void Unload()
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
