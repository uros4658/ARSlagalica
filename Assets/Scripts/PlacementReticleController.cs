using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class PlacementReticleController : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject reticle;

    private Pose placementPose;
    private bool placementPoseIsValid = false;

    void Start()
    {
        if (raycastManager == null)
        {
            raycastManager = FindObjectOfType<ARRaycastManager>();
            if (raycastManager == null)
            {
                Debug.LogError("ARRaycastManager not found in scene!");
            }
        }

        if (reticle == null)
        {
            Debug.LogWarning("Reticle GameObject not assigned!");
        }
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdateReticle();
    }

    void UpdatePlacementPose()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (raycastManager != null)
        {
            raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid)
            {
                placementPose = hits[0].pose;
            }
        }
    }

    void UpdateReticle()
    {
        if (reticle == null) return;

        if (placementPoseIsValid)
        {
            reticle.SetActive(true);
            reticle.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            reticle.SetActive(false);
        }
    }

    public Pose GetPlacementPose()
    {
        return placementPose;
    }

    public bool IsValidPlacement()
    {
        return placementPoseIsValid;
    }
}
