using UnityEngine;

public class SpawnInFrontOfCamera : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float distanceInFront = 1.0f;

    void Start()
    {
        Camera mainCam = Camera.main;
        if (mainCam != null && objectToSpawn != null)
        {
            Vector3 spawnPosition = mainCam.transform.position + mainCam.transform.forward * distanceInFront;
            Quaternion spawnRotation = Quaternion.LookRotation(mainCam.transform.forward);
            Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        }
    }
}
