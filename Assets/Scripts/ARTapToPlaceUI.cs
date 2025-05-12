using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARTapToPlaceUI : MonoBehaviour
{
    public GameObject uiPrefab;  // Your UI panel prefab
    private ARRaycastManager raycastManager;
    private GameObject placedUI;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (placedUI != null) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Input.GetTouch(0).position;

            if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                placedUI = Instantiate(uiPrefab, hitPose.position, hitPose.rotation);
                placedUI.transform.LookAt(Camera.main.transform); // Make it face the user
                placedUI.transform.Rotate(0, 180f, 0); // Optional flip
            }
        }
    }
}
