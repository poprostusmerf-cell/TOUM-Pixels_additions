using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Pixeladditions.Patches;

public static class PixeladditionsCredits
{
    private static TMP_FontAsset? _cachedFont;
    private static Material? _cachedMaterial;

    public static void Initialize()
    {
        SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>)((scene, _) =>
        {
            var original = Object.FindObjectOfType<VersionShower>();
            if (original != null)
            {
                var originalText = original.GetComponentInChildren<TextMeshPro>();
                if (originalText != null)
                {
                    _cachedFont = originalText.font;
                    _cachedMaterial = originalText.fontMaterial;
                }
            }

            if (_cachedFont == null || _cachedMaterial == null || HudManager.Instance == null)
            {
                return;
            }

            var creditsObj = new GameObject("PixeladditionsCredits");
            creditsObj.transform.SetParent(HudManager.Instance.transform, false);

            var text = creditsObj.AddComponent<TextMeshPro>();
            text.font = _cachedFont;
            text.fontMaterial = _cachedMaterial;
            text.UpdateFontAsset();

            text.text = "jebac disa anubisa i tetrisa co mu jaja zwisa";
            text.fontSize = 2f;
            text.alignment = TextAlignmentOptions.BottomLeft;
            text.color = Color.white;

            creditsObj.transform.localPosition = new Vector3(4.9f, 0f, -10f);
        }));
    }
}