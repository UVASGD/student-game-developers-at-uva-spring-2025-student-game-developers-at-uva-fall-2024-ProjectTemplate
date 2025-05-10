extends CharacterBody3D
class_name SharkInst

@onready var moving_up = true
@onready var spawn_position = self.global_position

func _ready():
	rotation = Vector3(0,0,0)
	$GoUpTimer.start()

func _physics_process(delta: float) -> void:
	if moving_up:
		global_position.y += 1
	else:
		global_position.y -= 1

func _on_go_up_timer_timeout() -> void:
	moving_up = false
	$GoDownTimer.start()

func _on_go_down_timer_timeout() -> void:
	self.queue_free()
