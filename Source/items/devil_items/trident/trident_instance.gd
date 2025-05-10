extends Area3D

var explosion = preload("res://Source/items/devil_items/explosion.tscn")

@export var speed:int = 50
@export var rotation_speed:int = 3
@export var explosion_force_amount:int = 5000
var velocity = Vector3()
var rot = Vector3()

@onready var target:VehicleBody3D

func _ready():
	call_deferred("_set_target")

func _set_target():
	var drivers = get_tree().get_nodes_in_group("driver")
	if drivers.size() > 0:
		target = drivers[0]
	else:
		print("No nodes found in 'driver' group")
func _physics_process(delta):
	if target:
		var direction = (target.global_transform.origin - global_transform.origin).normalized()

		var forward = -global_transform.basis.z  
		var rotation_axis = forward.cross(direction).normalized()
		var angle = acos(forward.dot(direction)) 

		if angle > 0.01: 
			var rotation_amount = min(angle, rotation_speed * delta) 
			rotate(rotation_axis, rotation_amount)

		# Move forward
		global_translate(-global_transform.basis.z * speed * delta)

func _on_body_entered(body):
	if body.is_in_group("driver") or body.is_in_group("track") or body.is_in_group("breakable"):
		var collision_point = global_transform.origin
		if body is PhysicsBody3D:
			var space_state = get_world_3d().direct_space_state
			var query = PhysicsRayQueryParameters3D.create(global_transform.origin, global_transform.origin + velocity.normalized() * -1)
			var result = space_state.intersect_ray(query)
			if result:
				collision_point = result.position

		var explosion_instance = explosion.instantiate()
		explosion_instance.global_transform.origin = collision_point
		explosion_instance.explosion_force_amount = explosion_force_amount
		get_parent().add_child(explosion_instance)
		explosion_instance.play_explosion_anim()
		hide()
		await explosion_instance.anim_player.animation_finished
		queue_free()
