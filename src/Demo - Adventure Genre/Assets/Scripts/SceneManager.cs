using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : GenericSingletonClass<SceneManager> {

	//Simple manager de escenas. Para que funcione se necesita crear una nueva escena que se llame exactamente "loading"

    void Awake()
    {
		base.Awake ();
    }

	//Carga la escena deseada pasandole un string con el nombre de dicha escena
    public void LoadScene(string scene)
    {
		//Antes de cargar la escena, carga una pantalla de loading
        UnityEngine.SceneManagement.SceneManager.LoadScene("loading");
        StartCoroutine(LoadLevelCorroutine(scene));
    }

	//Esta corrutina "falsifica" un tiempo de carga de 1 segundo en caso de que la carga sea instantanea.
	//Ejemplo: La escena a cargar es super liviana por lo que carga en un instante y la pantalla de loading dura una fraccion de segundo
	//Eso visualmente queda mal, por lo que esta corrutina se encarga de mantener el loading al menos durante un segundo para dar
	//la sensacion de carga
    IEnumerator LoadLevelCorroutine(string scene)
    {
        System.GC.Collect();
        yield return new WaitForSecondsRealtime(1);
        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
