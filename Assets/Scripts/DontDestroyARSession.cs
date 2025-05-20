using UnityEngine;

public class DontDestroyARSession : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
 