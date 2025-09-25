using System;
using System.Collections.Generic;

namespace Game.Common.Utils.DialogData
{
    [Serializable]
    public class DialogPayload
    {
        public List<DialogueEntry> dialogue;
        public List<AvatarEntry> avatars;
    }
}
