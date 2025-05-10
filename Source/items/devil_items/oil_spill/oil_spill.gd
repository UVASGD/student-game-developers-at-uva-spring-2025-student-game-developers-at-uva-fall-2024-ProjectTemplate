extends devil_items

@export var explosion_force_amount: int = 5000
@export var horizontal_boost: float = 1.5  
var is_being_used:bool = false


func _ready():
	num_uses = 3
	is_being_used = false
	#oil_spill
	item_instance = preload("res://Source/items/devil_items/oil_spill/oil_spill_inst.tscn")
	is_top_down = true
	
func use_top_down(insert_position: Vector3, spawn_parent: Node):
	if num_uses > 0:
		num_uses -= 1
		var item_instance = item_instance.instantiate()
		spawn_parent.add_child(item_instance)
		item_instance.global_position = insert_position
		item_instance.explosion_force_amount = explosion_force_amount
		item_instance.horizontal_boost = horizontal_boost
		
		if num_uses <= 0:
			SignalBus.emit_signal("clear_devil_item")
			self.queue_free()
