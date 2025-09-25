using System.Collections.Generic;
using UnityEngine;

namespace Game.Common.Utils.DialogData
{
    public static class DialogDataFactory
    {
        public static DialogPayload CreateSample()
        {
            return new DialogPayload
            {
                dialogue = new List<DialogueEntry>
                {
                    new DialogueEntry
                    {
                        name = "Sheldon",
                        text = "I admit {satisfied} the design of Cookie Crush is quite elegant in its simplicity."
                    },
                    new DialogueEntry
                    {
                        name = "Leonard",
                        text = "That’s practically a compliment, Sheldon. {intrigued} Are you feeling okay?"
                    },
                    new DialogueEntry
                    {
                        name = "Penny",
                        text = "Don’t worry, Leonard. He’s probably just trying to justify playing it himself."
                    },
                    new DialogueEntry
                    {
                        name = "Sheldon",
                        text =
                            "Incorrect. {neutral} I’m studying its mechanics. The progression system is oddly satisfying."
                    },
                    new DialogueEntry
                        { name = "Penny", text = "It’s called fun, Sheldon. You should try it more often." },
                    new DialogueEntry
                        { name = "Leonard", text = "She’s got a point. Sometimes, a simple game can be relaxing." },
                    new DialogueEntry { name = "Neighbour", text = "I fully agree {affirmative}" },
                    new DialogueEntry
                    {
                        name = "Sheldon",
                        text = "Relaxing? I suppose there’s merit in low-stakes gameplay to reduce cortisol levels."
                    },
                    new DialogueEntry
                    {
                        name = "Penny",
                        text = "Translation: Sheldon likes crushing cookies but won’t admit it. {laughing}"
                    },
                    new DialogueEntry
                        { name = "Sheldon", text = "Fine. I find the color-matching oddly soothing. Happy?" },
                    new DialogueEntry
                        { name = "Leonard", text = "Very. Now we can finally play as a team in Wordscapes." },
                    new DialogueEntry
                    {
                        name = "Penny", text = "Wait, Sheldon’s doing team games now? What’s next, co-op decorating?"
                    },
                    new DialogueEntry
                    {
                        name = "Sheldon",
                        text = "Unlikely. But if the design involves symmetry and efficiency, I may consider it."
                    },
                    new DialogueEntry { name = "Penny", text = "See? Casual gaming brings people together!" },
                    new DialogueEntry { name = "Leonard", text = "Even Sheldon. That’s a win for everyone. {win}" },
                    new DialogueEntry
                    {
                        name = "Sheldon",
                        text = "Agreed. {neutral} Though I still maintain chess simulators are superior."
                    },
                    new DialogueEntry
                    {
                        name = "Penny",
                        text = "Sure, Sheldon. {intrigued} You can play chess *after* we beat this next level."
                    }
                },
                avatars = new List<AvatarEntry>
                {
                    new AvatarEntry
                        { name = "Sheldon", url = "https://api.dicebear.com:81/timeout", position = "right" },
                    new AvatarEntry
                    {
                        name = "Sheldon",
                        url =
                            "https://api.dicebear.com/9.x/personas/png?body=squared&clothingColor=6dbb58&eyes=open&hair=buzzcut&hairColor=6c4545&mouth=smirk&nose=smallRound&skinColor=e5a07e",
                        position = "left"
                    },
                    new AvatarEntry
                    {
                        name = "Penny",
                        url =
                            "https://api.dicebear.com/9.x/personas/png?body=squared&clothingColor=f55d81&eyes=happy&hair=extraLong&hairColor=f29c65&mouth=smile&nose=smallRound&skinColor=e5a07e",
                        position = "right"
                    },
                    new AvatarEntry
                    {
                        name = "Leonard",
                        url =
                            "https://api.dicebear.com/9.x/personas/png?body=checkered&clothingColor=f3b63a&eyes=glasses&hair=shortCombover&hairColor=362c47&mouth=surprise&nose=mediumRound&skinColor=d78774",
                        position = "right"
                    },
                    new AvatarEntry
                        { name = "Nobody", url = "https://api.dicebear.com/5.x/personas/", position = "right" }
                }
            };
        }

        public static DialogPayload FromJson(string json)
        {
            return JsonUtility.FromJson<DialogPayload>(json);
        }

        public static string ToJson(DialogPayload payload, bool prettyPrint = true)
        {
            return JsonUtility.ToJson(payload, prettyPrint);
        }
    }
}
