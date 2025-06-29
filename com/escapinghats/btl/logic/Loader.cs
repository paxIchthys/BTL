using System;
using System.Collections.Generic;
using Godot;
namespace EscapingHats.BTL.Logic
{
	public class Loader
	{
		private static Loader _instance;
		private static readonly object _lock = new object();
		private static Queue<LoaderStep> Steps;
		private int TotalSteps = 0;
		private int StepsCompleted = 0;
		private string LoadingLog ="Loading...\n";
		private Node CurrentScene = null;

		public float Progress {
			get {
				if (TotalSteps == 0) return 0;
				return ((float)StepsCompleted / TotalSteps) * 100;
			}
		}


		// Private constructor to prevent instantiation
		private Loader() { 

		}

		// Public method to get the singleton instance
		public static Loader Instance
		{
			get
			{
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = new Loader();
						Steps = new Queue<LoaderStep>();
					}
				}
				return _instance;
			}
		}

		public static bool AddStep(LoaderStep step)
		{
			if(step != null){
			lock (_lock)
			{
				Steps.Enqueue(step);
				return true;
			}
			}
			return false;
		}

		private void UpdateLog(){
			CurrentScene.CallDeferred("set_text", LoadingLog);
		}

		private void AddLog(string NewLine){
			LoadingLog += NewLine + "\n";
			UpdateLog();
		}

		// Example method
		public void Load(Node scene = null)
		{
			CurrentScene = scene;
			UpdateLog();

			GD.Print("Loader is working...");
			AddStep(new LoaderStep { Name = "Example Step" });
			AddStep(new LoaderStep { Name = "Another Step" });
			AddStep(new LoaderStep { Name = "Final Step" });
			TotalSteps = Steps.Count;
			GD.Print("Steps added: " + Steps.Count);

			RunNextStep();
		}

		private void OnStepTimeout(string stepName)
		{
			GD.Print("Step timeout reached for: " + stepName);
			// Handle timeout logic here, e.g., log an error, retry the step, etc.

			StepsCompleted++;

			if (Steps.Count > 0){
				
				GD.Print("Steps completed: " + StepsCompleted + " of " + TotalSteps);
				GD.Print("Progress: " + Progress + "%");
				RunNextStep();
			}else{
				GD.Print("All steps completed or no steps left to process.");
				timer.Stop();
				timer.Dispose();
				timer = null;
			}

			CurrentScene.CallDeferred("set_progress", Progress);	
		}        

		public System.Timers.Timer timer;

		public void RunNextStep()
		{
			LoaderStep step = Steps.Dequeue();

			if (step != null) {
				GD.Print("Running step: " + step.Name);
				AddLog(step.Name);
				
				timer = new System.Timers.Timer(5000); // 1 second timeout  
				timer.Elapsed += (sender, e) => OnStepTimeout(step.Name);
				timer.AutoReset = false; // Only trigger once
				timer.Start();

				GD.Print("Processing step: " + step.Name);
				GD.Print("Step completed: " + step.Name);

			}  
		}

		
	}

	public class LoaderStep
	{
		public string Name { get; set; }
	}
}
