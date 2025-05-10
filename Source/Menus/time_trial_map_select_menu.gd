extends CanvasLayer
class_name MapSelectMenu

@onready var game_container = get_parent()

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
	Engine.time_scale = 1

func _on_button_pressed() -> void:
	game_container.spawn_level(1)
	queue_free()


func _on_back_pressed():
	game_container.spawn_main_menu()
	queue_free()
