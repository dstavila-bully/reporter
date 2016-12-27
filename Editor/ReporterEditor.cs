using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class MyAssetModificationProcessor : UnityEditor.AssetModificationProcessor
{
    static string imagesPath;
    const int ReporterExecOrder = -12000;

    private static Texture2D LoadImage(string fileName)
    {
        return (Texture2D)AssetDatabase.LoadAssetAtPath(imagesPath + fileName, typeof(Texture2D));
    }
    
	[MenuItem("Bully/Reporter/Create")]
	public static void CreateReporter()
	{
		var reporterObj = new GameObject();
		reporterObj.name = "Reporter";
		Reporter reporter = reporterObj.AddComponent<Reporter>();
		reporterObj.AddComponent<ReporterMessageReceiver>();

        var script = MonoScript.FromMonoBehaviour( reporter );
        imagesPath = Path.GetDirectoryName( AssetDatabase.GetAssetPath( script )) + "/Images/";

        if (MonoImporter.GetExecutionOrder(script) != ReporterExecOrder)
        {
            MonoImporter.SetExecutionOrder(script, ReporterExecOrder);
        }


		reporter.images = new Images();
		reporter.images.clearImage 			= LoadImage("clear.png");
		reporter.images.collapseImage 		= LoadImage("collapse.png");
		reporter.images.clearOnNewSceneImage= LoadImage("clearOnSceneLoaded.png");
		reporter.images.showTimeImage 		= LoadImage("timer_1.png");
		reporter.images.showSceneImage 		= LoadImage("UnityIcon.png");
		reporter.images.userImage 			= LoadImage("user.png");
		reporter.images.showMemoryImage 	= LoadImage("memory.png");
		reporter.images.softwareImage 		= LoadImage("software.png");
		reporter.images.dateImage 			= LoadImage("date.png");
		reporter.images.showFpsImage 		= LoadImage("fps.png");
		reporter.images.showGraphImage 		= LoadImage(".png");
		reporter.images.graphImage 			= LoadImage("chart.png");
		reporter.images.infoImage 			= LoadImage("info.png");
		reporter.images.searchImage 		= LoadImage("search.png");
		reporter.images.closeImage 			= LoadImage("close.png");
		reporter.images.buildFromImage 		= LoadImage("buildFrom.png");
		reporter.images.systemInfoImage 	= LoadImage("ComputerIcon.png");
		reporter.images.graphicsInfoImage 	= LoadImage("graphicCard.png");
		reporter.images.backImage 			= LoadImage("back.png");
		reporter.images.cameraImage 		= LoadImage(".png");
		reporter.images.logImage 			= LoadImage("log_icon.png");
		reporter.images.warningImage 		= LoadImage("warning_icon.png");
		reporter.images.errorImage 			= LoadImage("error_icon.png");
		reporter.images.barImage 			= LoadImage("bar.png");
		reporter.images.button_activeImage 	= LoadImage("button_active.png");
		reporter.images.even_logImage 		= LoadImage("even_log.png");
		reporter.images.odd_logImage 		= LoadImage("odd_log.png");
		reporter.images.selectedImage 		= LoadImage("selected.png");

		reporter.images.reporterScrollerSkin = (GUISkin)AssetDatabase.LoadAssetAtPath<GUISkin>(imagesPath + "reporterScrollerSkin.guiskin");

	}

	[InitializeOnLoad]
	public class BuildInfo
	{
		static BuildInfo ()
	    {
	        EditorApplication.update += Update;
	    }
	 
		private static bool _isCompiling = true ; 

	    static void Update ()
	    {
			if( !EditorApplication.isCompiling && _isCompiling )
			{
	        	//Debug.Log("Finish Compile");
				if( !Directory.Exists( Application.dataPath + "/StreamingAssets"))
				{
					Directory.CreateDirectory( Application.dataPath + "/StreamingAssets");
				}

				var info_path = Application.dataPath + "/StreamingAssets/build_info.txt" ;
				StreamWriter build_info = new StreamWriter( info_path );
				build_info.Write(
                        "Build from " + SystemInfo.deviceName +
                        "(" + SystemInfo.operatingSystem + ")" +
                        " at " + System.DateTime.Now.ToString() );

				build_info.Close();
			}
			
			_isCompiling = EditorApplication.isCompiling ;
	    }
	}
}
