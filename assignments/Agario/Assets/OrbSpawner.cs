 
using System.Threading.Tasks;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] private GameObject orb;
    public float X;
    public float Y;

    public void SpawnOrb()
    {
        Instantiate(orb, new Vector3(X, Y,0), Quaternion.identity);
    }
    
    
     
    
}
