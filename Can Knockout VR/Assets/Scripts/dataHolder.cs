using UnityEngine;
using System.Collections;

public static class dataHolder {
	// This is a static class which does not get destroyed on scene changes and 
	// keeps track of data between scene transitions
	public static string[] scenes = {"level1","level2","level3","level4"};
	public static int currentLevel = 0;
}