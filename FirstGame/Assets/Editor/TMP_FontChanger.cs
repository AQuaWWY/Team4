using UnityEngine;
using UnityEditor;
using TMPro;

// 创建一个编辑器窗口
public class TmpFontChanger : EditorWindow
{
    private TMP_FontAsset newFont; // 用于在窗口中选择新字体

    // 创建一个菜单项，点击后会打开我们的窗口
    [MenuItem("Tools/TMP Font Changer")]
    public static void ShowWindow()
    {
        // 显示现有窗口实例。如果不存在，则创建一个。
        GetWindow<TmpFontChanger>("TMP Font Changer");
    }

    // 绘制窗口内容
    void OnGUI()
    {
        GUILayout.Label("批量替换TMP字体", EditorStyles.boldLabel);

        // 创建一个对象字段，让用户可以拖拽或选择字体资源
        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("新字体 (TMP_Font Asset)", newFont, typeof(TMP_FontAsset), false);

        // 如果用户选择了字体，则显示按钮
        if (newFont != null)
        {
            if (GUILayout.Button("替换当前场景中所有TMP字体"))
            {
                ChangeFontInScene();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("请先选择一个新的TMP字体资源。", MessageType.Info);
        }
    }

    private void ChangeFontInScene()
    {
        if (newFont == null)
        {
            Debug.LogError("错误：未指定新字体！");
            return;
        }

        // 查找场景中所有的TextMeshProUGUI组件（用于UI）
        TextMeshProUGUI[] uiTexts = FindObjectsOfType<TextMeshProUGUI>(true); // true表示包含非激活对象
        int changedCount = 0;

        foreach (var text in uiTexts)
        {
            // 记录旧字体，只在字体不同时才修改
            TMP_FontAsset oldFont = text.font;
            if (oldFont != newFont)
            {
                text.font = newFont;
                EditorUtility.SetDirty(text); // 标记对象已修改，以便保存
                changedCount++;
            }
        }

        // 查找场景中所有的TextMeshPro组件（用于3D世界）
        TextMeshPro[] worldTexts = FindObjectsOfType<TextMeshPro>(true);
         foreach (var text in worldTexts)
        {
            TMP_FontAsset oldFont = text.font;
            if (oldFont != newFont)
            {
                text.font = newFont;
                EditorUtility.SetDirty(text);
                changedCount++;
            }
        }

        Debug.Log($"完成！共在当前场景中修改了 {changedCount} 个TMP对象的字体为: {newFont.name}");
    }
}