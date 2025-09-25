using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Common.Utils.Assets.Prefab
{
    public class AddressablePrefabLoader : IPrefabLoader
    {
        public async Task<GameObject> LoadAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Prefab address cannot be null or empty");

            try
            {
                // Await the task
                var prefab = await Addressables.LoadAssetAsync<GameObject>(address).Task;

                if (prefab == null)
                    throw new FileNotFoundException($"Prefab not found at address: {address}");

                // Instantiate the prefab in the scene
                return GameObject.Instantiate(prefab);
            }
            catch (FileNotFoundException fex)
            {
                Debug.LogError($"[AddressablePrefabLoader] File not found: {fex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AddressablePrefabLoader] Error occurred while loading prefab: {ex.Message}");
                throw;
            }
        }
    }
}