// <copyright file="Test.cs" company="Pixel Precision LLC">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>William Leu</author>
// <date>04/12/2020</date>
// <summary>
// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PxPre.UIDock;

public class Test : MonoBehaviour
{
    public Root root;

    public RectTransform rtMain;

    Window mainWin;
    
    void Start()
    {
        this.mainWin = root.WrapIntoWindow(this.rtMain, "Test Titlebar");
    }

    Window CreateDummyWindow()
    {
        GameObject go = new GameObject("TestWin");
        go.transform.SetParent(root.transform);

        UnityEngine.UI.Image img = go.AddComponent<UnityEngine.UI.Image>();
        img.color =
            new Color(
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f));

        Window.PrepareChild(img.rectTransform);
        img.rectTransform.anchoredPosition = new Vector2(0.0f, 0.0f);
        img.rectTransform.sizeDelta = new Vector2(200.0f, 200.0f);

        return this.root.WrapIntoWindow(img.rectTransform, "Dummy Window");
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Add Window") == true)
        {
            CreateDummyWindow();
        }

        GUILayout.Space(20.0f);

        if (GUILayout.Button("Add Top") == true)
        {
            Window w = CreateDummyWindow();
            this.root.DockWindow(w, null, Root.DropType.Top);
        }

        if (GUILayout.Button("Add Bottom") == true)
        {
            Window w = CreateDummyWindow();
            this.root.DockWindow(w, null, Root.DropType.Bottom);
        }

        if (GUILayout.Button("Add Left") == true)
        {
            Window w = CreateDummyWindow();
            this.root.DockWindow(w, null, Root.DropType.Left);
        }

        if (GUILayout.Button("Add Right") == true)
        {
            Window w = CreateDummyWindow();
            this.root.DockWindow(w, null, Root.DropType.Right);
        }

        GUILayout.Space(20.0f);

        if(GUILayout.Button("Cascade") == true)
        { 
            List<Window> wins = new List<Window>(this.root.DockedWindows());

            foreach(Window w in wins)
                this.root.UndockWindow(w);

            Vector2 cascadeBase = new Vector2(50.0f, -50.0f);
            Vector2 cascadeOffset = new Vector2(15.0f, -15.0f);
            int cascadeRestartAmt = 10;
            int idx = 0;
            // All windows should now be floating.
            foreach(Window fw in this.root.FloatingWindows())
            {
                fw.rectTransform.anchoredPosition = 
                    cascadeBase + cascadeOffset * (idx % cascadeRestartAmt);

                // Shadows don't update on their own if we touch
                // the anchoredPosition directly.
                fw.UpdateShadow();
                ++idx;
            }
        }

        GUILayout.Space(20.0f);

        if(GUILayout.Button("Clear") == true)
        { 
            this.root.Clear();
        }
    }
}
