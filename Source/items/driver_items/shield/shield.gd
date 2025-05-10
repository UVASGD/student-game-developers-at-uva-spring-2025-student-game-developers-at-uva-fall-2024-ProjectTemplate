extends driver_items

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
		SignalBus.emit_signal("driver_change_invul")
		timer.start()


func _on_timer_timeout():
	play_animation_backwards(animation_name)
	await get_tree().create_timer(0.2).timeout
	SignalBus.emit_signal("driver_change_invul")
	SignalBus.emit_signal("clear_item")
	$".".queue_free()
