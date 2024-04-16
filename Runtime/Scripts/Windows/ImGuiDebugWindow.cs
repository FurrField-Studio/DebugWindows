using System;
using ImGuiNET;
using UImGui;
using UnityEngine;

namespace CheatWindows.Scripts
{
    public class ImGuiDebugWindow : MonoBehaviour
    {
        public string Name;
        public string Category;
        public Vector2 MinSize;

        public bool HasCategory => !string.IsNullOrEmpty(Category);

        private void OnEnable()
        {
            UImGuiUtility.Layout += OnLayout;
            UImGuiUtility.OnInitialize += OnInitialize;
            UImGuiUtility.OnDeinitialize += OnDeinitialize;
        }
        
        private void OnDisable()
        {
            UImGuiUtility.Layout -= OnLayout;
            UImGuiUtility.OnInitialize -= OnInitialize;
            UImGuiUtility.OnDeinitialize -= OnDeinitialize;
        }

        private void OnLayout(UImGui.UImGui obj)
        {
            ImGui.Begin(Name);

            DrawWindow();

            EnsureMinimalSize();
            
            ImGui.End();
        }

        protected virtual void OnInitialize(UImGui.UImGui obj) { }
        protected virtual void OnDeinitialize(UImGui.UImGui obj) { }
        protected virtual void DrawWindow(){}

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        protected void EnsureMinimalSize()
        {
            Vector2 size = ImGui.GetWindowSize();
            
            bool updateSize = false;
            
            if(size.x < MinSize.x)
            {
                size.x = MinSize.x;

                updateSize = true;
            }

            if(size.y < MinSize.y)
            {
                size.y = MinSize.y;

                updateSize = true;
            }
	
            if(updateSize)
            {
                ImGui.SetWindowSize(size);
            }
        }
    }
}