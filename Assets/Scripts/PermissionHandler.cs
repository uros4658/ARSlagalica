using UnityEngine;
using System.Collections;

public class WebcamPermissionRequest : MonoBehaviour
{
    IEnumerator Start()
    {
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            var authorizationRequest = Application.RequestUserAuthorization(UserAuthorization.WebCam);
            yield return authorizationRequest;

            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                Debug.Log("Webcam access granted.");
                // You can now safely use the webcam
            }
            else
            {
                Debug.LogWarning("Webcam access denied.");
                // Handle the case where permission was denied
            }
        }
        else
        {
            Debug.Log("Webcam access already granted.");
        }
    }
}
