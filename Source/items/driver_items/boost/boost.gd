extends driver_items

@export var boost_multiplier:float = 2
@export var duration_length_seconds:int = 3
@onready var timer:Timer = $Timer
var is_being_used:bool = false

func _ready():
	is_being_used = false
	timer.wait_time = duration_length_seconds

func use():
	if !is_being_used:
		is_being_used = true
		play_animation(animation_name)
		SignalBus.emit_signal("update_engine_force", "multiply", boost_multiplier)
		timer.start()


func _on_timer_timeout():
	SignalBus.emit_signal("update_engine_force", "restore", 2)
	clear_item()
	$".".queue_free()
