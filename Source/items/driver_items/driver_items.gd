class_name driver_items extends Node
@export var item_name: String
@export var animation_name: String


func use():
	pass

func play_animation(animation_name):
	SignalBus.emit_signal("driver_play_animation", animation_name)

func play_animation_backwards(animation_name):
	SignalBus.emit_signal("driver_play_animation_backwards", animation_name)

func clear_item():
	SignalBus.emit_signal("clear_driver_item")
