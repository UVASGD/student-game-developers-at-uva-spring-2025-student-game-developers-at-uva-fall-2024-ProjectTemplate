class_name checkpoint extends Node3D
var is_active:bool = false

var has_passed:bool = false
@onready var animation_player:AnimationPlayer = $AnimationPlayer
@export var is_startpoint:bool = false
@onready var driver:VehicleBody3D


func _ready():
	#if is_startpoint:
		#is_active = true
	SignalBus.connect("out_of_bounds", tele_to_checkpoint)
	SignalBus.connect("update_checkpoint", turn_off_checkpoint)
	SignalBus.connect("revert_checkpoints", revert_state)
	driver = get_tree().get_nodes_in_group("driver")[0]


func _on_area_3d_body_entered(body):
	if !is_active and body.is_in_group("driver"):
		animation_player.play("checkpoint")
		is_active = true
		has_passed = true
		print("checkpoint activated!")
		SignalBus.emit_signal("update_checkpoint", name)
		
	
func tele_to_checkpoint():
	if is_active:
		driver.linear_velocity = Vector3.ZERO
		driver.angular_velocity = Vector3.ZERO
		var current_rotation:Vector3 = driver.rotation
		driver.rotation = Vector3(0, current_rotation.y, 0)
		driver.global_transform.origin = global_transform.origin
		
func turn_off_checkpoint(nm:String):
	if nm != name:
		is_active = false

func revert_state():
	animation_player.play_backwards("checkpoint")
	await animation_player.animation_finished
	if is_startpoint:
		animation_player.play("checkpoint")
