using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : SingleTon<SceneChangeManager>,IDestroySelf
{
    private float LoadingProgress = 0;
    [SerializeField]private LoadingPanel loadingPanel;
    private bool IsLoadingScene = false;
    [SerializeField] private TitleUi titleUi;
    private void Awake()
    {
        if (titleUi != null)
            titleUi.importantManager.Add(this);
        DontDestroyOnLoad(gameObject);
    }
    /*private void Start()
    {
        ShowLoading();
        Instance.loadingPanel.SetLoadingProcess(1, 1f);
    }*/

    public IEnumerator LoadScene(string sceneName,int mode,bool isLoading = false)
    {
        if(isLoading)
            ShowLoading();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, (LoadSceneMode)mode);
        //�������֮���Ƿ�������ת
        asyncOperation.allowSceneActivation = false;
        while (asyncOperation.progress < 0.9f)
        {
            if (isLoading)
            {
                SetLoadingProcess(asyncOperation);
                Debug.Log("��ǰ���س���������" + Instance.LoadingProgress);
            }
            yield return null;
        }
        if (isLoading)
        {
            HideLoading();
        }

        asyncOperation.allowSceneActivation = true;
        yield return null;
    }
    public IEnumerator LeaveScene(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
        yield return null;
    }
    public void ShowLoading()
    {
        if (Instance.loadingPanel == null)
        {
            try
            {
                Instance.loadingPanel = GameObject.FindWithTag("LoadingPanel").GetComponent<LoadingPanel>();
            }
            catch
            {
                //Debug.LogError("û���ڵ�ǰ�����ҵ��������");
                return;
            }
        }

        if (!loadingPanel.Visible || !loadingPanel.gameObject.activeSelf)
        {
            // ���ؽ��治���ӣ���򿪼���ҳ��
            Instance.loadingPanel.gameObject.SetActive(true);
            Instance.loadingPanel.Show();
            Instance.IsLoadingScene = true;
        }
    }
    public void HideLoading()
    {
        if (Instance.loadingPanel == null)
        {
            try
            {
                Instance.loadingPanel = GameObject.FindWithTag("LoadingPanel").GetComponent<LoadingPanel>();
            }
            catch
            {
                //Debug.LogError("û���ڵ�ǰ�����ҵ��������");
                return;
            }
        }

        Instance.IsLoadingScene = false;
        //Instance.loadingPanel.Hide();
    }
    public void SetLoadingProcess(AsyncOperation asyncOperation)
    {
        Instance.LoadingProgress = asyncOperation.progress;
        Instance.loadingPanel.SetLoadingProcess(1, Instance.LoadingProgress);

    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
