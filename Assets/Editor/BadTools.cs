﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class BadTools : EditorWindow
{
    [MenuItem("BadTools/open")]
    static void GetMe()
    {
        GetWindow<BadTools>();
    }

    static int money = 1000;
    static PlayerAttackComponent player;

    void OnGUI()
    {
        if (!Application.isPlaying)
        {
            GUILayout.Label("Run the game.", EditorStyles.helpBox);
            if (GUILayout.Button("Open Main Menu"))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(Path.Combine(Application.dataPath, "custom/scene/MainMenu.unity"));
            }
            return;
        }

        player = FindObjectOfType<PlayerAttackComponent>();

        if (player == null)
        {
            GUILayout.Label("Player Is Null.", EditorStyles.helpBox);
            return;
        }


        GUILayout.Label("Add/Remove Money.", EditorStyles.helpBox);

        GUILayout.BeginHorizontal();
        money = EditorGUILayout.IntField(money);
        if (GUILayout.Button("Add")) player.addLunchMoney(money);
        if (GUILayout.Button("Remove")) player.addLunchMoney(-money);
        GUILayout.EndHorizontal();
    }
}
