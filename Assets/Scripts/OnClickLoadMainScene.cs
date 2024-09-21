using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickLoadMainScene : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(2);
            Debug.Log("Attempting to load Main Scene");
        }
    }
}
