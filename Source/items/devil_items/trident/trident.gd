extends devil_items

@export var speed:int = 50
@export var rotation_speed:int = 3
@export var explosion_force_amount:int = 5000
var is_being_used:bool = false

var trident_instance = preload("res://Source/items/devil_items/trident/trident_instance.tscn")

func _ready():
	is_being_used = false
	is_top_down = false

func use():
	if !is_being_used:
		is_being_used = true
		num_uses -= 1
		var t_instance = trident_instance.instantiate()
		t_instance.speed = speed
		t_instance.rotation_speed = rotation_speed
		t_instance.explosion_force_amount = explosion_force_amount
		t_instance.global_transform.origin = self.global_transform.origin
		var spawn_pos = self.global_transform.origin
		spawn_pos.y += -1


		var root_scene = get_tree().root.get_child(get_tree().root.get_child_count() - 1)
		root_scene.add_child(t_instance)
		clear_item()
		self.queue_free()
		#get_parent().add_child(m_instance)
