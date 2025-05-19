using UnityEngine;

public class AnchorMainMenu : MonoBehaviour
{
    void Start()
    {
        transform.position = PlacementAnchor.Position;
        transform.rotation = PlacementAnchor.Rotation;
    }
}
