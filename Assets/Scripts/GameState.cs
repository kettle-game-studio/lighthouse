using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameState : MonoBehaviour
{
    Dictionary<string, string> StringMap;

    public HashSet<string> Unlocked = new();

    public HashSet<string> dedushkaUnlockable = new()
    {
        "lenin",
        "das kapital",
    };

    public List<string> listToUnlock = new();

    void Start()
    {
        StringMap = new() {
            { "_comment", "I can't touch it" },

            { "lenin_lock", "I have to ask grandpa if I can take it. I don't think he loves Lenin this much" },
            { "lenin_request", "Can I exterminate Lenin" },
            { "lenin_response", "Noo! It's my beloved theorist of Marxism-Leninism! Don' t you dare desecrate his portrait!" },
            { "lenin_comment", "Shoot, he does love Lenin this much" },

            { "can_lock", "" },
            { "detective novel_lock", "" },
            { "icon_lock", "" },

            { "das kapital_lock", "Hmm, maybe I should ask grandpa first..." },
            { "das kapital_request", "Can i, pretty please, take your copy of Das Kapital for a second?" },
            { "das kapital_response", "Fine! But you should read it. Marx had an outstanding view on materialism!" },

            { "pillow", "" },
            { "timber_lock", "" },
            { "plank_lock", "" },
            { "stool_lock", "" },
            { "stump_lock", "" }
        };
    }

    public string nextThingToUnlock()
    {
        while (listToUnlock.Count != 0)
        {
            var tooltip = listToUnlock[0].ToLower();
            listToUnlock.RemoveAt(0);

            if (dedushkaUnlockable.Contains(tooltip) && !Unlocked.Contains(tooltip))
            {
                Unlocked.Add(tooltip);
                return tooltip;
            }
        }

        return null;
    }

    public string GetString(string key)
    {
        var k = key.ToLower();
        if (!StringMap.ContainsKey(k))
            return $"UNKNOWN STRING \"{key}\"";
        return StringMap[k];
    }

    public bool TouchObject(string tooltip)
    {
        var t = tooltip.ToLower();
        if (Unlocked.Contains(t))
            return true;

        listToUnlock.Add(t);
        return false;
    }
}
