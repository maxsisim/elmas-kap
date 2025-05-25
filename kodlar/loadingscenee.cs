using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider loadingSlider;
    public float fillSpeed = 0.5f; // Slider ne kadar hýzlý dolsun

    private float targetProgress = 0f;
    private string sceneToLoad;

    void Start()
    {
        sceneToLoad = SceneToLoadHolder.sceneName;
        StartCoroutine(FillSliderAndLoadScene());
    }

    IEnumerator FillSliderAndLoadScene()
    {
        while (loadingSlider.value < 1f)
        {
            loadingSlider.value += fillSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.3f); // Ýsteðe baðlý bekleme
        SceneManager.LoadScene(sceneToLoad);
    }
}
