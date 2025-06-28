
using Godot;
using System;

public partial class GameLogic : Node2D
{
		
	//Accessible by GDScript
	public string TEST_VAR {get;set;} = "HELLO";	
	
	public string TestFunction(int x){
		return ("Test" + x);
	}
}
