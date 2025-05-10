extends devil_items

var is_being_used:bool = false

func _ready():
	num_uses = 2
	is_being_used = false
	item_instance = preload("res://Source/items/devil_items/breakablewall/breakablewall_instance.tscn")
	is_top_down = true
	
func use_top_down(insert_position: Vector3, spawn_parent: Node):
	if num_uses > 0:
		num_uses -= 1
		var item_instance = item_instance.instantiate()
		spawn_parent.add_child(item_instance)
		item_instance.global_position = insert_position + Vector3(0,5,0)
		item_instance.rotation = get_tree().get_nodes_in_group("driver")[0].rotation
		if num_uses <= 0:
			SignalBus.emit_signal("clear_devil_item")
			self.queue_free()
