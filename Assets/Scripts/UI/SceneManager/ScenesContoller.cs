using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesContoller : MonoBehaviour
{
   
    
    
    
    public void LoadScene(string sceneName)
    {

        StartCoroutine(waitsecs());

      IEnumerator waitsecs()
        {
            yield return new WaitForSeconds(70 * Time.deltaTime);
            SceneManager.LoadScene(sceneName);
        }

        

    }
    public void QuitGame() => Application.Quit();

  

}
