using UnityEditor;

namespace Editor.BuildPipeline {
    public class iOSBuilder : BuildPipeline.GameBuilder {
        protected override BuildPlayerOptions SetBuildOptions(string[] scenes, bool buildAppBundle, BuildOptions buildOptions = BuildOptions.None) {
            var buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.target = BuildTarget.iOS;
            buildPlayerOptions.scenes = scenes;
            buildPlayerOptions.options = buildOptions;
            buildPlayerOptions.locationPathName = buildPathBase +"/iOS";
            
            PlayerSettings.SplashScreen.show = false;

            return buildPlayerOptions;
        }
    }
}