using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class sceneManager {
	// Every level change must happen through this class to keep the current level
	// value consistent.
	public static void gotoLevel(int level){
		SceneManager.LoadScene(dataHolder.scenes[level], LoadSceneMode.Single);
		dataHolder.currentLevel = level;
	}

	public static void gotoLevel(string levelName){
		SceneManager.LoadScene(levelName, LoadSceneMode.Single);
	}

	public static void nextLevel(){
		int next;
		if (dataHolder.currentLevel < dataHolder.scenes.Length - 1) {
			next = dataHolder.currentLevel + 1;
		} else {
			next = 0;
		}
		dataHolder.currentLevel = next;
		SceneManager.LoadScene(dataHolder.scenes[next], LoadSceneMode.Single);
	}		
}