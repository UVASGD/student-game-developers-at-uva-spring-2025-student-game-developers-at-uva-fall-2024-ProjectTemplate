extends Node3D
class_name KartCamera

# credit: https://www.youtube.com/watch?v=6A6tp-rKy3Y

var direction = Vector3.LEFT
@export var smooth_speed : float = 3
@onready var car = get_parent()

func _physics_process(delta: float) -> void:
	var current_velocity = car.get_linear_velocity()
	$Camera3D/Control/Label.text = str(floor(current_velocity.length()))
	$Camera3D/Control/TextureProgressBar.value = (current_velocity.length() / 60) * 100
	current_velocity.y = 0
	
	if current_velocity.length_squared() > 1:
		direction = lerp(direction, -current_velocity.normalized(), smooth_speed*delta)
	
	global_transform.basis = get_rotation_from_direction(direction)

func get_rotation_from_direction(look_direction : Vector3):
	look_direction = look_direction.normalized()
	#print(look_direction)
	var z_axis = look_direction.cross(Vector3.UP)
	return Basis(-look_direction, Vector3.UP, - z_axis)
