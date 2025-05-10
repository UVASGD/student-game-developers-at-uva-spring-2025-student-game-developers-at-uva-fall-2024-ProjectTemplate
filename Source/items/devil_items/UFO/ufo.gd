extends devil_items

var is_being_used:bool = false

func _ready():
	is_being_used = false
	item_instance = preload("res://Source/items/devil_items/UFO/ufo_inst.tscn")
	is_top_down = true

func use_top_down(insert_position: Vector3, spawn_parent: Node):
	var item_instance = item_instance.instantiate()
	spawn_parent.add_child(item_instance)
	item_instance.global_position = insert_position + Vector3(0,15,0)
	SignalBus.emit_signal("clear_devil_item")
	self.queue_free()
