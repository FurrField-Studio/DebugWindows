using System;
using ImGuiNET;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace CheatWindows.Scripts
{
    
    //TODO add support for input system
    
    public class DebugWindowsManager : MonoBehaviour
    {
        [SerializeField]
        private UImGui.UImGui _imgui;

        [SerializeField]
        private KeyCode _keyToToggleImGui = KeyCode.F12;
        
        [SerializeField]
        private bool _resetMouseSprite;
        
        [SerializeField]
        private GameObject _mainWindowPrefab;

        [SerializeField]
        private GameObject[] _debugWindowsPrefabs;

        [SerializeField]
        private UnityEvent<bool> _onImGuiActive;

        private MainDebugWindow _mainDebugWindow;

        private void Awake()
        {
            if(_mainWindowPrefab == null) return;

            _mainDebugWindow = Instantiate(_mainWindowPrefab, transform).GetComponent<MainDebugWindow>();
            
            _mainDebugWindow.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            _mainDebugWindow.Initialize(_debugWindowsPrefabs);
        }

        private void Update()
        {
            if (Input.GetKeyUp(_keyToToggleImGui))
            {
                _imgui.enabled = !_imgui.enabled;

                if (!_imgui.enabled && _resetMouseSprite)
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
                
                _onImGuiActive?.Invoke(_imgui.enabled);
            }
        }

        private void OnValidate()
        {
            if (_imgui == null) _imgui = GetComponent<UImGui.UImGui>();
            if (_imgui == null) return;
            
            _imgui.enabled = false;
            
            if(EditorUtility.IsPersistent(this)) return;
            
            _imgui.SetCamera(Camera.main);
        }
    }
}