using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;

[CustomEditor(typeof(Readme))]
[InitializeOnLoad]
public class ReadmeEditor : Editor {
	
	static string kShowedReadmeSessionStateName = "ReadmeEditor.showedReadme";
	
	static float kSpace = 16f;
	
	static ReadmeEditor()
	{
		EditorApplication.delayCall += SelectReadmeAutomatically;
	}
	
	static void SelectReadmeAutomatically()
	{
		if (!SessionState.GetBool(kShowedReadmeSessionStateName, false ))
		{
			var readme = SelectReadme();
			SessionState.SetBool(kShowedReadmeSessionStateName, true);
			
			if (readme && !readme.loadedLayout)
			{
				LoadLayout();
				readme.loadedLayout = true;
			}
		} 
	}
	
	static void LoadLayout()
	{
		var assembly = typeof(EditorApplication).Assembly; 
		var windowLayoutType = assembly.GetType("UnityEditor.WindowLayout", true);
		var method = windowLayoutType.GetMethod("LoadWindowLayout", BindingFlags.Public | BindingFlags.Static);
		method.Invoke(null, new object[]{Path.Combine(Application.dataPath, "TutorialInfo/Layout.wlt"), false});
	}
	
	[MenuItem("Tutorial/Show Tutorial Instructions")]
	static Readme SelectReadme() 
	{
		var ids = AssetDatabase.FindAssets("Readme t:Readme");
		if (ids.Length == 1)
		{
			var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));
			
			Selection.objects = new UnityEngine.Object[]{readmeObject};
			
			return (Readme)readmeObject;
		}
		else
		{
			Debug.Log("Couldn't find a readme");
			return null;
		}
	}
	
	protected override void OnHeaderGUI()
	{
		var readme = (Readme)target;
		Init();

        //var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth - 30f, 3000f);
        var iconHeight = Mathf.Min(EditorGUIUtility.currentViewWidth/6.6f, 291);

        GUILayout.BeginVertical(m_BodyStyle);
		{
            GUILayout.Space(10);
            GUILayout.Label(readme.icon, IconStyle, GUILayout.Width(EditorGUIUtility.currentViewWidth - 30f), GUILayout.Height(iconHeight));
            GUILayout.Space(10);
            GUILayout.Label(readme.title, TitleStyle);
            GUILayout.Space(10);
        }
		GUILayout.EndHorizontal();
	}
	
	public override void OnInspectorGUI()
	{
		var readme = (Readme)target;
		Init();
		
		foreach (var section in readme.sections)
		{
			if (!string.IsNullOrEmpty(section.heading))
			{
                EditorGUILayout.LabelField(section.heading, HeadingStyle);
                //GUILayout.Space(20);
                foreach (var point in section.points) GUILayout.Label(point, HeadingStyle);
            }
            GUILayout.Space(20);
            //GUILayout.Space(kSpace);
        }
	}
	
	
	bool m_Initialized;

    GUIStyle BigHeaderStyle { get { return m_BigHeaderStyle; } }
    [SerializeField] GUIStyle m_BigHeaderStyle;

    GUIStyle LinkStyle { get { return m_LinkStyle; } }
	[SerializeField] GUIStyle m_LinkStyle;
	
	GUIStyle IconStyle { get { return m_IconStyle; } }
	[SerializeField] GUIStyle m_IconStyle;

    GUIStyle TitleStyle { get { return m_TitleStyle; } }
    [SerializeField] GUIStyle m_TitleStyle;

    GUIStyle HeadingStyle { get { return m_HeadingStyle; } }
	[SerializeField] GUIStyle m_HeadingStyle;

    GUIStyle PointStyle { get { return m_PointStyle; } }
    [SerializeField] GUIStyle m_PointStyle;

    GUIStyle BodyStyle { get { return m_BodyStyle; } }
	[SerializeField] GUIStyle m_BodyStyle;
	
	void Init()
	{
		if (m_Initialized)
			return;

        m_BodyStyle = new GUIStyle(EditorStyles.helpBox);
        m_BodyStyle.wordWrap = true;
        m_BodyStyle.fontSize = 14;

        m_BodyStyle = new GUIStyle(EditorStyles.label);
		m_BodyStyle.wordWrap = true;
		m_BodyStyle.fontSize = 14;
		
		m_TitleStyle = new GUIStyle(m_BodyStyle);
        m_TitleStyle.alignment = TextAnchor.UpperCenter;
        m_TitleStyle.fontSize = 30;
        m_TitleStyle.normal.textColor = Color.white;

        m_IconStyle = new GUIStyle(m_BodyStyle);
        m_IconStyle.alignment = TextAnchor.UpperCenter;

        m_HeadingStyle = new GUIStyle(m_BodyStyle);
		m_HeadingStyle.fontSize = 15;

        m_LinkStyle = new GUIStyle(m_BodyStyle);
		m_LinkStyle.wordWrap = false;
		// Match selection color which works nicely for both light and dark skins
		m_LinkStyle.normal.textColor = new Color (0x00/255f, 0x78/255f, 0xDA/255f, 1f);
		m_LinkStyle.stretchWidth = false;
		
		m_Initialized = true;
	}
	
	bool LinkLabel (GUIContent label, params GUILayoutOption[] options)
	{
		var position = GUILayoutUtility.GetRect(label, LinkStyle, options);

		Handles.BeginGUI ();
		Handles.color = LinkStyle.normal.textColor;
		Handles.DrawLine (new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
		Handles.color = Color.white;
		Handles.EndGUI ();

		EditorGUIUtility.AddCursorRect (position, MouseCursor.Link);

		return GUI.Button (position, label, LinkStyle);
	}
}

