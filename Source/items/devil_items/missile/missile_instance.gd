extends Area3D

var explosion = preload("res://Source/items/devil_items/explosion.tscn")

@export var speed: int = 50
@export var rotation_speed: int = 3
@export var explosion_force_amount: int = 5000
var velocity = Vector3()



@onready var target: VehicleBody3D  # We can ignore this as it's no longer used

func _ready():
	# No need to set target anymore, missile doesn't track anything
	velocity = -global_transform.basis.z * speed  # Initial velocity in the direction it's facing

func _physics_process(delta):
	# Move the missile in a straight line
	global_translate(velocity * delta)

func _on_body_entered(body):
	# When the missile collides with a body
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
		
		if body.is_in_group("breakable"):
			body.break_object()
		await explosion_instance.anim_player.animation_finished
		queue_free()
