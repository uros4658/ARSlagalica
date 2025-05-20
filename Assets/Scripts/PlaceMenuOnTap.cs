using UnityEngine;

public class PlaceMenuOnTap : MonoBehaviour
{
    public GameObject menuPrefab;
    public PlacementReticleController reticleController;

    private GameObject spawnedMenu;

    void Update()
    {
        if (spawnedMenu != null) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 pos = reticleController.transform.position;
            Quaternion rot = reticleController.transform.rotation;

            spawnedMenu = Instantiate(menuPrefab, pos, rot);
            spawnedMenu.transform.LookAt(Camera.main.transform);
            spawnedMenu.transform.Rotate(0, 180f, 0); // face camera
        }
    }
}
