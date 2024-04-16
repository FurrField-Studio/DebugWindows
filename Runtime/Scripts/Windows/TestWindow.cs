using ImGuiNET;

namespace CheatWindows.Scripts.Windows
{
    public class TestWindow : ImGuiDebugWindow
    {
        protected override void DrawWindow()
        {
            ImGui.Text("Test");
        }
    }
}