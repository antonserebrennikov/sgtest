using System.Threading.Tasks;

namespace Game.Common.Utils.Assets.Scene
{
    public interface ISceneLoader
    {
        public Task LoadSceneAsync(string sceneName);
    }
}