extends Node3D
class_name ItemBox

@onready var player_owner

@onready var idle_player:AnimationPlayer = %idle_player
@onready var despawn_player:AnimationPlayer = %despawn_player
@onready var regen_timer:Timer = $regen_timer

@export var regen_time_seconds:int = 5

var item_available:bool = true

func _ready():
	#idle_player.play("idle")
	item_available = true
	regen_timer.wait_time = regen_time_seconds

func _on_area_3d_body_entered(body):
	if item_available:
		if body.is_in_group("driver"):
			item_available = false
			despawn_player.play("despawn")
			SignalBus.emit_signal("driver_item_taken")
			regen_timer.start()
		if body.is_in_group("devil") and body is CharacterBody3D and body.item_manager.get_child_count() < 2:
			item_available = false
			despawn_player.play("despawn")
			SignalBus.emit_signal("devil_item_taken")
			regen_timer.start()

func _on_regen_timer_timeout():
	despawn_player.play_backwards("despawn")
	await despawn_player.animation_finished
	item_available = true
