extends CanvasLayer
class_name MainMenu

@onready var game_container = get_parent()

@onready var versus_hovered = false
@onready var trials_hovered = false
@onready var tutorial_hovered = false
@onready var credits_hovered = false

@onready var button_y = 500
@onready var button_yh = 475

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
	Engine.time_scale = 1

func _physics_process(delta: float) -> void:
	if versus_hovered:
		var hover_tween = create_tween()
		hover_tween.tween_property($Versus, "global_position:y", button_yh, 0.2)
	else:
		var hover_tween = create_tween()
		hover_tween.tween_property($Versus, "global_position:y", button_y, 0.2)
		
	if trials_hovered:
		var hover_tween2 = create_tween()
		hover_tween2.tween_property($TimeTrials, "global_position:y", button_yh, 0.2)
	else:
		var hover_tween2 = create_tween()
		hover_tween2.tween_property($TimeTrials, "global_position:y", button_y, 0.2)
		
	if tutorial_hovered:
		var hover_tween3 = create_tween()
		hover_tween3.tween_property($Tutorial, "global_position:y", button_yh, 0.2)
	else:
		var hover_tween3 = create_tween()
		hover_tween3.tween_property($Tutorial, "global_position:y", button_y, 0.2)
		
	if credits_hovered:
		var hover_tween4 = create_tween()
		hover_tween4.tween_property($Credits, "global_position:y", button_yh, 0.2)
	else:
		var hover_tween4 = create_tween()
		hover_tween4.tween_property($Credits, "global_position:y", button_y, 0.2)

func _on_versus_pressed() -> void:
	# Variable within the function is is_versus which is set to true
	game_container.spawn_map_select_menu(true)
	queue_free()	

func _on_time_trials_pressed():
	# Variable within the function is is_versus which is set to false since this is time trials
	game_container.spawn_map_select_menu(false)
	queue_free()	

func _on_versus_mouse_entered() -> void:
	versus_hovered = true
func _on_versus_mouse_exited() -> void:
	versus_hovered = false
func _on_time_trials_mouse_entered() -> void:
	trials_hovered = true
func _on_time_trials_mouse_exited() -> void:
	trials_hovered = false
func _on_tutorial_mouse_entered() -> void:
	tutorial_hovered = true
func _on_tutorial_mouse_exited() -> void:
	tutorial_hovered = false
func _on_credits_mouse_entered() -> void:
	credits_hovered = true
func _on_credits_mouse_exited() -> void:
	credits_hovered = false
