
using Godot;
using System;

using EscapingHats.BTL.Logic;

public partial class GameLogic : Node2D
{
		
	//Accessible by GDScript
	public string VERSION {get;set;} = "MVP.001";	
	public PackedScene currentScene;
	
	public Node LoadScene(string packedSceneLocation){
		currentScene = GD.Load<PackedScene>(packedSceneLocation);
		Node instance = currentScene.Instantiate();
		if (currentScene == null) {
			GD.PrintErr("Failed to load scene: " + packedSceneLocation);
			return null;
		}else{
			AddChild(instance);
			Loader.Instance.Load(instance);
			GD.Print("Scene loaded successfully: " + packedSceneLocation);
			return instance;
		}		
	}
}
