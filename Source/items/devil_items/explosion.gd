extends Area3D

@onready var anim_player = %AnimationPlayer
@export var explosion_force_amount: int = 5000
@export var upward_boost: float = 1.5  # Adjusts how much upward force is applied

func play_explosion_anim():
	anim_player.play("explosion")

func _on_animation_player_animation_finished(anim_name):
	queue_free()

func _on_body_entered(body):
	if body.is_in_group("driver") and not body.invul_on:
		var direction = (body.global_transform.origin - global_transform.origin).normalized()

		# Add upward force
		direction.y += upward_boost  
		direction = direction.normalized()  # Normalize to prevent excessive force in any direction

		# Apply impulse
		var force = direction * explosion_force_amount  
		body.apply_impulse(force, Vector3.ZERO)
