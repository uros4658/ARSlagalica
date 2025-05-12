using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSlagalica()
    {
        SceneManager.LoadScene("Slagalica");
    }

    public void LoadMojBroj()
    {
        SceneManager.LoadScene("MojBroj");
    }

    public void LoadSkocko()
    {
        SceneManager.LoadScene("Skocko");
    }

    public void LoadAsocijacije()
    {
        SceneManager.LoadScene("Asocijacije");
    }
}
