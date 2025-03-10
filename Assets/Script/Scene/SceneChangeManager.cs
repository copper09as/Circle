using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : SingleTon<SceneChangeManager>
{
    private float LoadingProgress = 0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(LoadSceen("Shop"));
        }
    }
    public IEnumerator LoadSceen(string sceneName)
    {
        ShowLoading();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while(asyncOperation.progress<0.9f)
        {
            Debug.Log("当前加载场景进度是" + Instance.LoadingProgress);
            yield return null;
        }
        asyncOperation.allowSceneActivation = true;
        yield return null;
    }
    private void ShowLoading()
    {

    }
    public void SetLoadingProcess(AsyncOperation asyncOperation)
    {
        Instance.LoadingProgress = asyncOperation.progress;

    }
}
