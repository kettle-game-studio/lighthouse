using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    Dictionary<string, string> StringMap;

    void Start()
    {
        StringMap = new() {
            { "default_comment", "I cant touch it" },

            { "lenin_lock", "I have to ask grandpa if I can take it. I don't thinkhe loves Lenin this much" },
            { "lenin_request", "Can I exterminate Lenin" },
            { "lenin_response", "Noo! It's my beloved theorist of Marxism-Leninism! Dont you dare desecrate his portrait!" },
            { "lenin_comment", "Shoot, he does love Lenin this much" },
        };
    }

    public string GetString(string key)
    {
        if (!StringMap.ContainsKey(key))
            return $"UNKNOWN STRING \"{key}\"";
        return StringMap[key];
    }
}
