using System.Threading.Tasks;
using MVP.Presenter;

namespace Game.Common.Utils.UI
{
    public interface IPresenterLoader
    {
        public Task<T> LoadPresenterAsync<T>() where T : IPresenter;
    }
}