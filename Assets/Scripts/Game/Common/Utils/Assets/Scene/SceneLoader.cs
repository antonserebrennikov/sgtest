using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Common.Utils.Assets.Scene
{
    public class SceneLoader: ISceneLoader
    {
        public async Task LoadSceneAsync(string sceneName)
        {
            try
            {
                // Load the scene asynchronously
                var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

                if (asyncOperation == null)
                {
                    Debug.LogError($"Failed to load scene '{sceneName}'. AsyncOperation returned null.");
                    return;
                }

                // Wait until the scene is fully loaded
                while (!asyncOperation.isDone)
                {
                    await Task.Yield(); // Ensure the method doesn't block the main thread
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}