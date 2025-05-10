extends Node3D
class_name DevilItemManager

@onready var sigil_anim_player = %sigil_anim_player

@onready var trident = preload("res://Source/items/devil_items/trident/trident.tscn")
@onready var missile = preload("res://Source/items/devil_items/missile/missile.tscn")
@onready var oil_spill = preload("res://Source/items/devil_items/oil_spill/oil_spill.tscn")
@onready var black_hole = preload("res://Source/items/devil_items/black hole/black_hole.tscn")
@onready var shark = preload("res://Source/items/devil_items/shark/shark.tscn")
@onready var bagger = preload("res://Source/items/devil_items/Bagger/bagger.tscn")
@onready var breakable_wall = preload("res://Source/items/devil_items/breakablewall/breakable_wall.tscn")
@onready var ufo = preload("res://Source/items/devil_items/UFO/ufo.tscn")

var all_items:Array[String] = ["trident", "missile", "shark", "oil_spill", "bagger", "breakable_wall", "black_hole", "ufo"]

func _ready():
	SignalBus.connect("devil_giving_item", insert_item)

func insert_item(item_name:String):
	if item_name in all_items:
		var item
		match item_name:
			"trident":
				sigil_anim_player.play("sigil_spawn")
				item = trident.instantiate()
				add_child(item)
				item.global_transform.origin = self.global_transform.origin
			"missile":
				sigil_anim_player.play("sigil_spawn")
				item = missile.instantiate()
				add_child(item)
				item.global_transform.origin = self.global_transform.origin
			"oil_spill":
				sigil_anim_player.play("sigil_spawn")
				item = oil_spill.instantiate()
				add_child(item)
				item.global_transform.origin = self.global_transform.origin
			"bagger":
				sigil_anim_player.play("sigil_spawn")
				item = bagger.instantiate()
				add_child(item)
				item.global_transform.origin = self.global_transform.origin		
			"black_hole":
				sigil_anim_player.play("sigil_spawn")
				item = black_hole.instantiate()
				add_child(item)
				item.global_transform.origin = self.global_transform.origin
			"shark":
				sigil_anim_player.play("sigil_spawn")
				item = shark.instantiate()
				add_child(item)
				item.global_transform.origin = self.global_transform.origin
			"breakable_wall":
				sigil_anim_player.play("sigil_spawn")
				item = breakable_wall.instantiate()
				add_child(item)
				item.global_transform.origin = self.global_transform.origin
			"ufo":
				sigil_anim_player.play("sigil_spawn")
				item = ufo.instantiate()
				add_child(item)
				item.global_transform.origin = self.global_transform.origin

func _input(event):
	if event.is_action_pressed("use_devil_item"):
		use_item()

func use_item():
	if self.get_child_count() > 1:
		var child_node = self.get_child(1)
		child_node.use()
		if child_node.num_uses <= 0:
			
			sigil_anim_player.play_backwards("sigil_spawn")
		
