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
        public int orbId;

        [SerializeField] private MessageHandler msgHandler;

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
            orbSpawn.GetComponent<SpawnedOrbData>().orbId = orbId;
            // Debug.Log($"Adding orb {orbId} at {new Vector2(X,Y)}");
        }

        public void DeSpawnOrb()
        {

             
        }

    }
}
