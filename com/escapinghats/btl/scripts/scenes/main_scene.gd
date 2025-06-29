extends Node2D

func _ready() -> void:
	print("Main Scene Loading: BTL Version-" + GameLogic.VERSION)
	print(GameLogic.LoadScene("res://assets/scenes/loader.tscn"))
