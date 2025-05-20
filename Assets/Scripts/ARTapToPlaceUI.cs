using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARTapToPlaceUI : MonoBehaviour
{
    public GameObject uiPrefab; // Prefab to instantiate (e.g., PlacementReticle)
    private ARRaycastManager raycastManager;
    private GameObject placedUI;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (placedUI != null) return; // Only allow one placement

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Input.GetTouch(0).position;

            // Raycast against all detected planes (horizontal + vertical if enabled)
            if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // Save position and rotation globally if needed later
                PlacementAnchor.Position = hitPose.position;
                PlacementAnchor.Rotation = hitPose.rotation;

                // Instantiate UI object at the hit location
                placedUI = Instantiate(uiPrefab, hitPose.position, hitPose.rotation);

                // Rotate it to face the camera
                placedUI.transform.LookAt(Camera.main.transform);
                placedUI.transform.Rotate(0, 180f, 0); // flip to face user

                // Optional: Attach to the detected plane to keep it stable
                var anchor = hits[0].trackable as ARPlane;
                if (anchor != null)
                {
                    placedUI.transform.SetParent(anchor.transform);
                }

                // IMPORTANT: Don't load another scene here unless required.
                // If you really want to load the MainMenu scene, do it after a delay or via button press.
                // SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
