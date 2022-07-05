using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadLevelDelay = 1f;
    [SerializeField] float loadLevelDelay = 4f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    AudioSource audioSource;

    bool isTriggered = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("friendly collider");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence() 
    {
        if (!isTriggered) 
        {
            isTriggered = true;
            audioSource.Stop();
            GetComponent<Movement>().enabled = false;
            audioSource.PlayOneShot(crashSound);
            Invoke("ReloadLevel", reloadLevelDelay);
        }
    }

    void StartSuccessSequence() 
    {
        if (!isTriggered)
        {
            isTriggered = true;
            audioSource.Stop();
            GetComponent<Movement>().enabled = false;
            audioSource.PlayOneShot(successSound);
            Invoke("LoadNextLevel", loadLevelDelay);
        }
    }

    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
