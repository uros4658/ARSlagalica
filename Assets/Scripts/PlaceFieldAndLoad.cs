using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;

public class PlaceFieldAndLoad : MonoBehaviour
{
    [SerializeField] private GameObject placementRetticle; // Should be the same as this GameObject
    [SerializeField] private GameObject persistentAnchorObject; // Optional: a container to hold the fixed position
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == placementRetticle.transform)
                {
                    // Save the world position and rotation
                    Vector3 fixedPosition = placementRetticle.transform.position;
                    Quaternion fixedRotation = placementRetticle.transform.rotation;

                    // Store it in a static variable or persistent object
                    PlacementAnchor.Position = fixedPosition;
                    PlacementAnchor.Rotation = fixedRotation;

                    // Load the MainMenu scene
                    SceneManager.LoadScene(mainMenuSceneName);
                }
            }
        }
    }
}

