using UnityEngine;

namespace Game.Common.Utils.DialogData
{
    public static class DialogDataFactory
    {
        public static DialogPayload FromJson(string json)
        {
            return JsonUtility.FromJson<DialogPayload>(json);
        }
    }
}
