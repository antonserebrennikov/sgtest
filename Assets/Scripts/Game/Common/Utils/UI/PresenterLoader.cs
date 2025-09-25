using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Common.Utils.Assets.Prefab;
using MVP.Presenter;
using UnityEngine;

namespace Game.Common.Utils.UI
{
    public class PresenterLoader: IPresenterLoader
    {
        //TODO: make configurable
        private const string uiPrefabsRootPath = "Prefabs/UI";

        private readonly IPrefabLoader prefabLoader;
        private readonly Dictionary<Type, IPresenter> cache = new();

        public PresenterLoader(IPrefabLoader prefabLoader)
        {
            this.prefabLoader = prefabLoader;
        }

        public async Task<T> LoadPresenterAsync<T>() where T : IPresenter
        {
            if (cache == null)
                throw new InvalidOperationException("[IPresenterLoader] Cache is not initialized.");

            if (cache.ContainsKey(typeof(T)))
                return await Task.FromResult((T)cache[typeof(T)]);

            try
            {
                var prefab = await prefabLoader.LoadAsync($"{uiPrefabsRootPath}/{typeof(T).Name}");
                var view = prefab.GetComponent<T>();

                if (prefab.GetComponent<DontDestroyOnLoad>() == null)
                    prefab.AddComponent<DontDestroyOnLoad>();
            
                cache.Add(typeof(T), view);
                
                return view;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}