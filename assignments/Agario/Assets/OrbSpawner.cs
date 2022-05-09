 
using System;
using System.Threading.Tasks;
using UnityEngine;
 


public class OrbSpawner : MonoBehaviour
{
    [SerializeField] private GameObject orb;
    public float X;
    public float Y;

[SerializeField]    private MessageHandler msgHandler;
    
    private void OnEnable()
    {
        msgHandler.OnSpawnOrb += SpawnOrb;
    }

    private void OnDisable()
    {
        msgHandler.OnSpawnOrb -= SpawnOrb;
    }

    public void SpawnOrb()
    {
        Debug.Log("Help");
        Instantiate(orb, new Vector3(X, Y,0), Quaternion.identity);
        Debug.Log("Plz");
    }
    
     
    
}
