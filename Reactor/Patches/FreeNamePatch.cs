using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Reactor.Patches
{
    internal static class FreeNamePatch
    {
        public static void Initialize()
        {
            SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>) ((scene, _) =>
            {
                if (scene.name != "MMOnline") return;
                
                var nameText = GameObject.Find("NameText");
                if (!nameText) return;

                var textBoxTMP = nameText.AddComponent<TextBoxTMP>();
                textBoxTMP.Background = nameText.GetComponentInChildren<SpriteRenderer>();
                textBoxTMP.OnChange = new UnityEngine.UI.Button.ButtonClickedEvent();
                textBoxTMP.OnEnter = new UnityEngine.UI.Button.ButtonClickedEvent();
                textBoxTMP.OnFocusLost = new UnityEngine.UI.Button.ButtonClickedEvent();
                textBoxTMP.characterLimit = 10;
                
                var textMeshPro = nameText.GetComponentInChildren<TextMeshPro>();
                textBoxTMP.outputText = textMeshPro;
                textBoxTMP.SetText(SaveManager.PlayerName);

                textBoxTMP.OnChange.AddListener((Action) (() =>
                {
                    SaveManager.PlayerName = textBoxTMP.text;
                }));

                var pipeGameObject = GameObject.Find("Pipe");
                if (!pipeGameObject) return;
                
                var pipe = UnityEngine.Object.Instantiate(pipeGameObject, textMeshPro.transform);
                pipe.GetComponent<TextMeshPro>().fontSize = 4f;
                textBoxTMP.Pipe = pipe.GetComponent<MeshRenderer>();
            }));
        }
    }
}
