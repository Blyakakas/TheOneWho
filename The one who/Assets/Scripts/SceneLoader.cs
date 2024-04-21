using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private bool _restartThisScene = false;
    [SerializeField] private bool _triggerRestart = true;
    public void SceneLoad()
    {
        SceneManager.LoadSceneAsync(index);
    }

    private void OnValidate()
    {
        if(_restartThisScene)
        {
            index = SceneManager.GetActiveScene().buildIndex;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement playerMovement) && _triggerRestart)
        {
            SceneManager.LoadSceneAsync(index);
        }
    }
}
