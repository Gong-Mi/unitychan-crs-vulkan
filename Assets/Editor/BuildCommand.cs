using UnityEditor;
using System.Linq;

public class BuildCommand
{
    public static void BuildAndroid()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        // 1. Force Vulkan primary + GLES3 fallback
        PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new UnityEngine.Rendering.GraphicsDeviceType[] {
            UnityEngine.Rendering.GraphicsDeviceType.Vulkan,
            UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3
        });

        // 2. Multi-core utilization: Graphics Jobs, MT Rendering, GPU Skinning
        PlayerSettings.graphicsJobs = true;
        PlayerSettings.graphicsJobMode = GraphicsJobMode.Native;
        PlayerSettings.MTRendering = true;
        PlayerSettings.gpuSkinning = true;

        // 3. Android target architecture: ARM64 IL2CPP Release
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Android, Il2CppCompilerConfiguration.Release);

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "build/Android/UnityChan_CRS_Vulkan.apk",
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        BuildPipeline.BuildPlayer(options);
    }
}
