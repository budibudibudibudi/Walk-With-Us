using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Test : MonoBehaviour
{
}
#if UNITY_EDITOR
[CustomEditor(typeof(Test))]
public class TestEditor:Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Test test = (Test)target;

        if (GUILayout.Button("TestButton"))
        {
            test.gameObject.AddComponent<Rigidbody>();
        }
    }
}
#endif
