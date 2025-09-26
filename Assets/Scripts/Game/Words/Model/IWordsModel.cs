using System.Threading.Tasks;
using Game.Common.Utils.DialogData;
using MVP.Model;
using UnityEngine;

namespace Game.Words.Model
{
    public interface IWordsModel: IModel
    {
        public Task<DialogPayload> GetDialogData();
        public Task<Texture2D> GetAvatarTexture(AvatarEntry avatar);
    }
}