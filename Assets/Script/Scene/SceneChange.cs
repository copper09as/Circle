using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private static AsyncOperationHandle<SceneInstance> sceneLoadHandle;
    public static void ChangeTo(string sceneName)
    {
        if (sceneLoadHandle.IsValid())
        {
            Addressables.UnloadSceneAsync(sceneLoadHandle).Completed += (handle) =>
            {
                if(handle.Status == AsyncOperationStatus.Succeeded)
                    sceneLoadHandle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            };
        }
        else
        {
            sceneLoadHandle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
    
}
