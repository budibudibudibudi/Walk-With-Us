using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[InitializeOnLoad]
public static class ToolbarButton 
{

    static ToolbarButton(){
        EditorApplication.delayCall -= OnInit;
        EditorApplication.delayCall += OnInit;

    }

    private static List<MethodInfo> _rightMethods = new List<MethodInfo>();
    private static List<MethodInfo> _leftMethods = new ();
    private static int selectedSceneIndex = 0;

    private static void OnInit(){
        var toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");

        var objects = Resources.FindObjectsOfTypeAll(toolbarType);
        if(objects.Length == 0) return;
        var toolbar = objects[0] as ScriptableObject;
        var root = toolbar.GetType().GetField("m_Root", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(toolbar) as VisualElement;
        var toolbarKanan =  root.Q<VisualElement>("ToolbarZoneRightAlign");
        var toolbarKiri = root.Q<VisualElement>("ToolbarZoneLeftAlign");

        PopulateMethod();
        
        var container = new IMGUIContainer(() =>
        {
            GUILayout.BeginHorizontal();

            foreach (var method in _rightMethods)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(null, null);
                }
            }

            GUILayout.EndHorizontal();

        });
        toolbarKanan.Add(container);

        var leftContainer = new IMGUIContainer(() =>
        {
            GUILayout.BeginHorizontal();

            string[] sceneNames = new string[SceneManager.sceneCountInBuildSettings];
            for (int i = 0; i < sceneNames.Length; i++)
            {
                sceneNames[i] = SceneUtility.GetScenePathByBuildIndex(i);
                sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(sceneNames[i]);
            }
            selectedSceneIndex = EditorGUILayout.Popup(SceneManager.GetActiveScene().buildIndex, sceneNames);
            string selectedSceneName = sceneNames[selectedSceneIndex];
            foreach (var method in _leftMethods)
            {
                if (GUI.changed)
                {
                    method.Invoke(null, new object[] { selectedSceneName });
                }
            }

            GUILayout.EndHorizontal();
        });
        toolbarKiri.Add(leftContainer);
    }

    private static void PopulateMethod(){
        var rightMethods =  TypeCache.GetMethodsWithAttribute<ToolbarRightAttribute>();
        foreach (var method in rightMethods) {
            if (method.IsStatic) {
                _rightMethods.Add(method);
            }
        }
        var leftMethods = TypeCache.GetMethodsWithAttribute<ToolbarLeftAttribute>();
        foreach (var method in leftMethods)
        {
            if (method.IsStatic)
            {
                _leftMethods.Add(method);
            }
        }
    }
}
