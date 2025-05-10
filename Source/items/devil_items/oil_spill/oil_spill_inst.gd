extends Area3D
class_name OilSpill

@export var explosion_force_amount: int = 5000
@export var horizontal_boost: float = 1.5  # Adjusts how much upward force is applied

@onready var setup : bool = false

func _on_body_entered(body: Node3D) -> void:
	if body.is_in_group("driver") and setup:
		var direction = (body.global_transform.origin - global_transform.origin).normalized()
		# Add upward force
		direction.x += horizontal_boost
		direction = direction.normalized()  # Normalize to prevent excessive force in any direction

		# Apply impulse
		var force = direction * explosion_force_amount  
		body.apply_impulse(force, Vector3.ZERO)

func _on_setup_timer_timeout() -> void:
	setup = true
