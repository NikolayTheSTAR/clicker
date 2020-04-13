using System;
using UnityEngine;

[CreateAssetMenu(fileName = "readmee", menuName = "Create readme")]
public class Readme : ScriptableObject {
	public Texture2D icon;
	public string title;
	public Section[] sections;
	public bool loadedLayout;
	
	[Serializable]
	public class Section {
		public string heading;
        public string[] points;
	}
}
