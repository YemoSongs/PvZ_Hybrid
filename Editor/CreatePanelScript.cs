﻿using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// 生成UI Panel脚本（自动绑定好Button，Toggle，Slider的事件监听）
/// </summary>
public class CreatePanelScript : MonoBehaviour
{
    [MenuItem("GameObject/Create Panel Script", false, 10)]
    static void CreateHelpPanelScript(MenuCommand menuCommand)
    {
        // 获取当前选中的GameObject
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null || !selectedObject.name.EndsWith("Panel"))
        {
            EditorUtility.DisplayDialog("Error！！！", "请选中一个UI界面来生成脚本！Please select a UI Panel to generate script for.", "OK");
            return;
        }

        string panelName = selectedObject.name;
        string scriptName = panelName + ".cs";
        string scriptsFolder = Path.Combine(Application.dataPath, "Scripts/MainGame/UI");

        // 确保目录存在
        if (!Directory.Exists(scriptsFolder))
        {
            Directory.CreateDirectory(scriptsFolder);
        }

        string scriptPath = Path.Combine(scriptsFolder, scriptName);

        if (File.Exists(scriptPath))
        {
            EditorUtility.DisplayDialog("Error!!!!!", "脚本已经在该路径存在！！！Script already exists at path: " + scriptPath, "OK");
            return;
        }

        // 获取所有Button组件
        UnityEngine.UI.Button[] buttons = selectedObject.GetComponentsInChildren<UnityEngine.UI.Button>();
        List<string> buttonNames = new List<string>();

        foreach (UnityEngine.UI.Button button in buttons)
        {
            buttonNames.Add(button.name);
        }

        // 生成按钮事件处理代码
        StringBuilder buttonEventCode = new StringBuilder();
        StringBuilder clickButtonCode = new StringBuilder();

        if(buttonNames.Count > 0)
        {
            foreach (string buttonName in buttonNames)
            {
                string methodName = buttonName + "_OnClick";
                buttonEventCode.AppendLine($"    void {methodName}()");
                buttonEventCode.AppendLine("    {");
                buttonEventCode.AppendLine("        ");
                buttonEventCode.AppendLine("    }");
                buttonEventCode.AppendLine();

                clickButtonCode.AppendLine($"            case \"{buttonName}\":");
                clickButtonCode.AppendLine($"                {methodName}();");
                clickButtonCode.AppendLine("                break;");
                clickButtonCode.AppendLine();
            }
        }

        // 获取所有Toggle组件
        UnityEngine.UI.Toggle[] toggles = selectedObject.GetComponentsInChildren<UnityEngine.UI.Toggle>();
        List<string> toggleNames = new List<string>();

        foreach (UnityEngine.UI.Toggle toggle in toggles)
        {
            toggleNames.Add(toggle.name);
        }

        // 生成Toggle事件处理代码
        StringBuilder toggleEventCode = new StringBuilder();
        StringBuilder changeToggleCode = new StringBuilder();

        if (toggleNames.Count > 0)
        {
            foreach (string toggleName in toggleNames)
            {
                string methodName = toggleName + "_ChangeValue";
                toggleEventCode.AppendLine($"    void {methodName}(bool value)");
                toggleEventCode.AppendLine("    {");
                toggleEventCode.AppendLine("        ");
                toggleEventCode.AppendLine("    }");
                toggleEventCode.AppendLine();

                changeToggleCode.AppendLine($"            case \"{toggleName}\":");
                changeToggleCode.AppendLine($"                {methodName}(value);");
                changeToggleCode.AppendLine("                break;");
                changeToggleCode.AppendLine();
            }
        }

        // 获取所有Slider组件
        UnityEngine.UI.Slider[] sliders = selectedObject.GetComponentsInChildren<UnityEngine.UI.Slider>();
        List<string> sliderNames = new List<string>();

        foreach (UnityEngine.UI.Slider slider in sliders)
        {
            sliderNames.Add(slider.name);
        }


        // 生成Slider事件处理代码
        StringBuilder sliderEventCode = new StringBuilder();
        StringBuilder changeSliderCode = new StringBuilder();

        if (sliderNames.Count > 0)
        {
            foreach (string sliderName in sliderNames)
            {
                string methodName = sliderName + "_ChangeValue";
                sliderEventCode.AppendLine($"    void {methodName}(float value)");
                sliderEventCode.AppendLine("    {");
                sliderEventCode.AppendLine("        ");
                sliderEventCode.AppendLine("    }");
                sliderEventCode.AppendLine();

                changeSliderCode.AppendLine($"            case \"{sliderName}\":");
                changeSliderCode.AppendLine($"                {methodName}(value);");
                changeSliderCode.AppendLine("                break;");
                changeSliderCode.AppendLine();
            }
        }


        // 模板代码
        string template = @"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class {0} : BasePanel
{{
    #region 自定义字段，属性

    #endregion    

    public override void HideMe()
    {{
        
    }}

    public override void ShowMe()
    {{
        
    }}

    #region 自定义函数

    #endregion
    
    #region 控件事件监听
{1}
    protected override void ClickBtn(string btnName)
    {{
        switch (btnName)
        {{
{2}            default:
                break;
        }}
    }}
{3}
    protected override void ToggleValueChange(string toggleName, bool value)
    {{
        switch (toggleName)
        {{
{4}            default:
                break;
        }}
    }}
{5}
    protected override void SliderValueChange(string sliderName, float value)
    {{
        switch (sliderName)
        {{
{6}            default:
                break;
        }}
    }}
    #endregion
}}";

        // 格式化模板代码
        string formattedScript = string.Format(template, panelName, 
                                                         buttonEventCode.ToString(), 
                                                         clickButtonCode.ToString(),
                                                         toggleEventCode.ToString(),
                                                         changeToggleCode.ToString(),
                                                         sliderEventCode.ToString(),
                                                         changeSliderCode.ToString());

        // 写入到文件
        File.WriteAllText(scriptPath, formattedScript);

        // 刷新AssetDatabase
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Success！！！", "脚本生成成功！Script created successfully at path: " + scriptPath, "OK");
    }
}
