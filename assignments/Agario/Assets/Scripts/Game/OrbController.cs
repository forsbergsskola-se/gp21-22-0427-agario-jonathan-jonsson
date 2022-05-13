using System.Collections.Generic;
using System.Linq;
using AgarioShared.Network;
using UnityEngine;

namespace Game
{
    public class OrbController : MonoBehaviour
    {
        [SerializeField] private GameObject orb;
        public float X;
        public float Y;

        [SerializeField] private MessageHandler msgHandler;
        private Dictionary<Vector2, GameObject> orbDictionary = new Dictionary<Vector2, GameObject>();
        public float XToCheck { get; set; }
        public float YToCheck { get; set; }


        private void OnEnable()
        {
            msgHandler.OnSpawnOrb += SpawnOrb;
            msgHandler.OnDeSpawnOrb += DeSpawnOrb;
        }

        private void OnDisable()
        {
            msgHandler.OnSpawnOrb -= SpawnOrb;
        }

        public void SpawnOrb()
        {
            
            var orbSpawn = Instantiate(orb, new Vector3(X, Y,0), Quaternion.identity);
            Debug.Log($"Adding: {new Vector2(X,Y)}");
            if (orbDictionary.ContainsKey(new Vector2(X, Y)))
            {
                
            }
            orbDictionary.Add(new Vector2(X,Y),orbSpawn);
        }

        public void DeSpawnOrb()
        {
            if (orbDictionary.ContainsKey(new Vector2(XToCheck, YToCheck)))
            {
                Destroy(orbDictionary[new Vector2(XToCheck,YToCheck)]);
                
            }
            else
            {
                Debug.LogError($"ERROR: Orb is not in dictionary at {XToCheck},{YToCheck}.");                
            }
            orbDictionary.Remove(new Vector2(X, Y));

        }

    }
}
