extends CharacterBody3D
class_name UFOInst

@export var pull_force: int = 65
@onready var moving_up = true
@onready var spawn_position = self.global_position

@onready var kart_body = null

func _ready():
	rotation = Vector3(0,0,0)
	$GoUpTimer.start()

func _physics_process(delta: float) -> void:
	rotation.y += 0.1
	if moving_up:
		global_position.y += 1
	else:
		if kart_body:
			var direction = (kart_body.global_transform.origin - global_transform.origin).normalized()
			direction = direction.normalized()  # Normalize to prevent excessive force in any direction

			var force = direction * pull_force
			kart_body.apply_impulse(force * -1, Vector3.ZERO)

func _on_go_up_timer_timeout() -> void:
	moving_up = false
	$DespawnTimer.start()

func _on_despawn_timer_timeout() -> void:
	self.queue_free()

func _on_pull_area_body_entered(body: Node3D) -> void:
	if body.is_in_group("driver"):
		kart_body = body

func _on_pull_area_body_exited(body: Node3D) -> void:
	if body.is_in_group("driver"):
		kart_body = null
