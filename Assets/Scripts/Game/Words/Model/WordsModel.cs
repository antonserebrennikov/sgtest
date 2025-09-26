using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.Common.Utils.DialogData;
using Game.Common.Utils.DialogDataLoader;
using Game.Common.Utils.TextureLoader;
using UnityEngine;

namespace Game.Words.Model
{
    public class WordsModel: IWordsModel
    {
        //TODO: Move to config
        private const string dialogUrl = "https://private-624120-softgamesassignment.apiary-mock.com/v3/magicwords";
        //TODO: Move to config
        private const int dialogLoadingTimeoutInMilliseconds = 60 * 1000; // 1 minute
        private const int avatarLoadingTimeoutInMilliseconds = 20 * 1000; // 20 sec
        
        private IDialogDataLoader dialogDataLoader;
        private ITextureLoader textureLoader;
        private DialogPayload dialogPayload;
        private Dictionary<string, Texture2D> avatarCache = new();
        
        public WordsModel(IDialogDataLoader dialogDataLoader, ITextureLoader textureLoader)
        {
            this.dialogDataLoader = dialogDataLoader ?? throw new ArgumentNullException(nameof(dialogDataLoader));
            this.textureLoader = textureLoader ?? throw new ArgumentNullException(nameof(textureLoader));
        }

        public async Task<DialogPayload> GetDialogData()
        {
            if (dialogPayload != null)
                return dialogPayload;
            
            if (dialogDataLoader == null)
                throw new Exception("Mission data loader is not set");
            
            using var cancellationTokenSource = new CancellationTokenSource(dialogLoadingTimeoutInMilliseconds);
            
            dialogPayload = await dialogDataLoader.LoadAsync(dialogUrl, cancellationTokenSource.Token);
            
            return dialogPayload;
        }
        
        public async Task<Texture2D> GetAvatarTexture(AvatarEntry avatar)
        {
            if (avatarCache.ContainsKey(avatar.name) && avatarCache[avatar.name] != null)
                return avatarCache[avatar.name];
            
            if (string.IsNullOrWhiteSpace(avatar.url))
                throw new ArgumentException("URL is null or empty.", nameof(avatar.url));
            
            if (textureLoader == null)
                throw new Exception("Texture loader is not set");
            
            using var cancellationTokenSource = new CancellationTokenSource(avatarLoadingTimeoutInMilliseconds);
            var texture = await textureLoader.LoadAsync(avatar.url, cancellationTokenSource.Token);
            
            if (texture != null && !avatarCache.ContainsKey(avatar.name))
                avatarCache[avatar.name] = texture;
            
            return texture;
        }
    }
}