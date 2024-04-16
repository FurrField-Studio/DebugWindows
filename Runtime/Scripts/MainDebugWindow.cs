using System.Collections.Generic;
using System.Linq;
using ImGuiNET;
using UnityEngine;

namespace CheatWindows.Scripts
{
    public class MainDebugWindow : ImGuiDebugWindow
    {
        private List<ImGuiDebugWindow> _debugWindows;

        private List<DebugWindowCategory> _debugWindowCategories;

        public void Initialize(GameObject[] debugWindowsPrefabs)
        {
            _debugWindows = new List<ImGuiDebugWindow>(debugWindowsPrefabs.Length);

            CreateDebugWindows(debugWindowsPrefabs);

            GenerateCategories();
            
            _debugWindows = _debugWindows.Where(debugWindow => !debugWindow.HasCategory).ToList();
        }

        private void CreateDebugWindows(GameObject[] debugWindowsPrefabs)
        {
            ImGuiDebugWindow newDebugWindow;
            
            for (int i = 0; i < debugWindowsPrefabs.Length; i++)
            {
                newDebugWindow = Instantiate(debugWindowsPrefabs[i], transform).GetComponent<ImGuiDebugWindow>();
                newDebugWindow.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                newDebugWindow.gameObject.SetActive(false);
                
                _debugWindows.Add(newDebugWindow);
            }
        }

        private void GenerateCategories()
        {
            _debugWindowCategories = new();
            
            foreach (var debugWindow in _debugWindows)
            {
                if(!debugWindow.HasCategory) continue;
                
                DebugWindowCategory debugWindowCategory = _debugWindowCategories.Find(dwc => dwc.Category == debugWindow.Category);
                if (debugWindowCategory == null)
                {
                    debugWindowCategory = new DebugWindowCategory(debugWindow.Category);
                    _debugWindowCategories.Add(debugWindowCategory);
                }
                
                debugWindowCategory.AddDebugWindow(debugWindow);
            }
        }

        protected override void DrawWindow()
        {
            RenderWindowList(_debugWindows);

            foreach (var debugWindowCategory in _debugWindowCategories)
            {
                debugWindowCategory.RenderCategory();
            }
        }
        
        private class DebugWindowCategory
        {
            public string Category { get; private set; }

            private List<ImGuiDebugWindow> _debugWindows;

            public DebugWindowCategory(string category)
            {
                _debugWindows = new List<ImGuiDebugWindow>();
                Category = category;
            }

            public void AddDebugWindow(ImGuiDebugWindow debugWindow)
            {
                _debugWindows.Add(debugWindow);
            }
            
            public void RenderCategory()
            {
                if (ImGui.CollapsingHeader(Category))
                {
                    RenderWindowList(_debugWindows);
                }
            }
        }

        private static void RenderWindowList(List<ImGuiDebugWindow> debugWindows)
        {
            float windowWidth = ImGui.GetWindowWidth();
            
            foreach (ImGuiDebugWindow debugWindow in debugWindows)
            {
                if (ImGui.Button(debugWindow.Name, new Vector2(windowWidth, 20)))
                {
                    debugWindow.Toggle();
                }
            }
        }
    }
}