using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : SingleTon<SceneChangeManager>,IDestroySelf
{
    private float LoadingProgress = 0;
    [SerializeField] private TitleUi titleUi;
    private void Awake()
    {
        if (titleUi != null)
            titleUi.importantManager.Add(this);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(LoadSceen("Shop",1));
        }
    }
    public IEnumerator LoadSceen(string sceneName,int mode)
    {
        ShowLoading();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, (LoadSceneMode)mode);
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

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
