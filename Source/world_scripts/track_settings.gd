extends Node3D
class_name track_settings 

@onready var game_container = self.get_parent()

@export var is_versus:bool = true
@export var num_laps:int = 3
@export var driver_items_on_map:Array[String] = []
@export var devil_items_on_map:Array[String] = []
var start_point:Node3D
var num_laps_left:int
var num_checkpoints:int
var checkpoints_array:Array[String] = []
var check_manager:Node3D


func _ready():
	num_laps_left = num_laps
	SignalBus.emit_signal("update_laps_left", str(num_laps_left))
	SignalBus.connect("driver_item_taken", give_driver_item)
	SignalBus.connect("devil_item_taken", give_devil_item)
	SignalBus.connect("update_checkpoint", add_checkpoint)
	SignalBus.connect("back_to_menu", back_to_menu)
	

func _physics_process(delta: float) -> void:
	print(get_tree().get_root().get_children())

func initialize_num_checkpoints(checkpoints:Node3D):
	num_checkpoints = checkpoints.get_child_count()
func give_driver_item():
	if not driver_items_on_map.is_empty():
		var total_items:int = len(driver_items_on_map) - 1
		var random_item_number:int = randi_range(0, total_items)
		var random_item_name:String = driver_items_on_map[random_item_number]		
		SignalBus.emit_signal("driver_giving_item", random_item_name)
		print("giving item: " + random_item_name)

func add_checkpoint(nm):
	#if num_laps_left <= 0:
		#print("ending track")
		#SignalBus.emit_signal("end_track")
	if nm not in checkpoints_array and len(checkpoints_array) < num_checkpoints:
		checkpoints_array.append(nm)
		print(str(len(checkpoints_array)) + "checkpoints achieved!")
	elif len(checkpoints_array) == num_checkpoints and num_laps_left > 0:
		num_laps_left -= 1
		print(str(num_laps_left) +  "laps left!")
		SignalBus.emit_signal("update_laps_left", str(num_laps_left))
		SignalBus.emit_signal("revert_checkpoints")
		checkpoints_array = []
		
		if num_laps_left <= 0:
			print("ending track")
			SignalBus.emit_signal("end_track")

#func has_passed_all_checkpoints(checkpoints:Array[Node3D]):
	#for checkpoint in checkpoints:
		
	
	
	
	
	
func give_devil_item():
	if not driver_items_on_map.is_empty():
		var total_items:int = len(devil_items_on_map) - 1
		var random_item_number:int = randi_range(0, total_items)
		var random_item_name:String = devil_items_on_map[random_item_number]		
		SignalBus.emit_signal("devil_giving_item", random_item_name)
		print("giving item: " + random_item_name)

#func has_passed_all_checkpoints(checkpoints:Array[Node3D]):
	#for checkpoint in checkpoints:
		#
	#
	#
	#
	#
	#
 
func back_to_menu():
	match is_versus:
		true:
			get_tree().get_root().get_node("/root/GameContainer").spawn_main_menu()
			queue_free()

		false:
			pass
	
