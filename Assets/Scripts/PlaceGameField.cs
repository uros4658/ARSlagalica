using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlaceGameField : MonoBehaviour
{
    public GameObject gameFieldPrefab;
    public ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    void Update()
    {
        if (spawnedObject != null) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            // Use Vertical Plane detection (wall)
            if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                // Rotate to face forward
                hitPose.rotation = Quaternion.LookRotation(-hitPose.forward);

                spawnedObject = Instantiate(gameFieldPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}
