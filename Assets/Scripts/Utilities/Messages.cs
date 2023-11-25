using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    class Messages
    {
        private static Dictionary<string, string> s_storage;

        public static string getValue(string key)
        {
            try
            {
                return s_storage[key];
            }
            catch
            {
                return key;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            s_storage = new Dictionary<string, string>();

            var textAsset = Resources.Load<TextAsset>(@"Messages");

            string text = textAsset.text;
            string[] lines = text.Split('\n');

            foreach (var line in lines)
            {
                if (line.StartsWith("#") || line.Contains("=") == false)
                    continue;

                int index = line.IndexOf("=");
                string key = line.Substring(0, index);
                string value = line.Substring(index + 1, line.Length - index - 1);
                value = value.Replace("\\n", "\n");

                if (s_storage.ContainsKey(key))
                {
                    Debug.LogWarning($"Key '{key}' already exists and new value has ignored.");
                }
                else
                {
                    s_storage.Add(key, value);
                }
            }
        }
    }
}
