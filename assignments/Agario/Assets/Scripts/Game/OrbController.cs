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
        public int spawnOrbId;
        public int deSpawnOrbId;

        [SerializeField] private MessageHandler msgHandler;
        private Dictionary<int, GameObject> orbsOnfield = new();
        private void OnEnable()
        {
            msgHandler.OnSpawnOrb += SpawnOrb;
            msgHandler.OnDeSpawnOrb += DeSpawnOrb;
        }

        private void OnDisable()
        {
            msgHandler.OnSpawnOrb -= SpawnOrb;
            msgHandler.OnDeSpawnOrb -= DeSpawnOrb;

        }

        public void SpawnOrb()
        {
            var orbSpawn = Instantiate(orb, new Vector3(X, Y,0), Quaternion.identity);
           var orbSpawnId = orbSpawn.GetComponent<SpawnedOrbData>().orbId = spawnOrbId;
            orbsOnfield.Add(orbSpawnId,orbSpawn);
            // Debug.Log($"Adding orb {orbId} at {new Vector2(X,Y)}");
        }

        public void DeSpawnOrb()
        {
            var orbToDestroy = orbsOnfield[deSpawnOrbId];
            orbsOnfield.Remove(deSpawnOrbId);
            Destroy(orbToDestroy);

        }

    }
}
