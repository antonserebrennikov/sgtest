using System.Threading.Tasks;
using UnityEngine;

namespace Game.Common.Utils.Assets.Prefab
{
    public interface IPrefabLoader
    {
        Task<GameObject> LoadAsync(string path);
    }
}