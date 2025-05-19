using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement; 
using System.Collections.Generic;

public class ARTapToPlaceUI : MonoBehaviour
{
    public GameObject uiPrefab;
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

                // Save the placement pose for use in MainMenu scene
                PlacementAnchor.Position = hitPose.position;
                PlacementAnchor.Rotation = hitPose.rotation;

                // Optionally instantiate something here (like a visual marker)
                placedUI = Instantiate(uiPrefab, hitPose.position, hitPose.rotation);
                placedUI.transform.LookAt(Camera.main.transform);
                placedUI.transform.Rotate(0, 180f, 0);

                // Load MainMenu scene after placement
                SceneManager.LoadScene("MainMenu"); //
            }
        }
    }
}
