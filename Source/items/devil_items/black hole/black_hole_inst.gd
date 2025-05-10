extends Area3D
class_name BlackHoleInst

@export var pull_force: int = 100
@export var horizontal_boost: float = 1.5  # Adjusts how much upward force is applied

@onready var setup : bool = false
@onready var kart_body = null

func _ready():
	var animation_tween = create_tween()
	animation_tween.tween_property($MeshInstance3D, "mesh:material:shader_parameter/FloatParameter", 0.4, 2)

func _physics_process(delta: float) -> void:
	self.rotation.y += 0.1
	self.rotation.z += 0.1
	if kart_body:
		var direction = (kart_body.global_transform.origin - global_transform.origin).normalized()
		#direction.x += horizontal_boost
		direction = direction.normalized()  # Normalize to prevent excessive force in any direction

		var force = direction * pull_force
		kart_body.apply_impulse(force * -1, Vector3.ZERO)

func _on_body_entered(body: Node3D) -> void:
	if body.is_in_group("driver"):
		kart_body = body

func _on_body_exited(body: Node3D) -> void:
	if body.is_in_group("driver"):
		kart_body = null

func _on_setup_timer_timeout() -> void:
	setup = true

func _on_deletion_timer_timeout() -> void:
	self.queue_free()
