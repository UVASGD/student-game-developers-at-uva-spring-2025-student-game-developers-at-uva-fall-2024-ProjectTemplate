class_name devil_items extends Node
@export var item_name: String
@export var animation_name: String
@export var is_top_down: bool = false
@export var item_instance: PackedScene
@export var num_uses: int
@export var is_controllable: bool = false


func use():
	pass

func use_top_down(insert_position: Vector3, spawn_parent: Node):
	pass

func play_animation(animation_name):
	SignalBus.emit_signal("devil_play_animation", animation_name)

func clear_item():
	SignalBus.emit_signal("clear_devil_item")
