extends Node3D
class_name Spawner

enum PLAYER {DRIVER, DEVIL}

@export var player_owner : PLAYER

var has_item:bool = false
var item_box = preload("res://Source/items/item_box/itembox.tscn")

func _ready():
	spawn_item()
	has_item = true

func spawn_item():
	if !has_item:
		var item_instance = item_box.instantiate()
		item_instance.player_owner = self.player_owner
		$spawn_point.add_child(item_instance)
		has_item = true
