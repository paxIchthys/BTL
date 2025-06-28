extends Node2D

func _ready() -> void:
	GameLogic.TEST_VAR = "2"
	
	print("GD SCRIPT RUNNING")
	print(GameLogic.TEST_VAR)
	print(GameLogic.TestFunction(9))
	
	$Loader.set_text("Loading...")
