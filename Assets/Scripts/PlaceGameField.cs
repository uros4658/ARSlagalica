using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlaceGameField : MonoBehaviour
{
    public GameObject gameFieldPrefab; // Assign your prefab in Inspector
    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (spawnedObject != null)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                spawnedObject = Instantiate(gameFieldPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}
