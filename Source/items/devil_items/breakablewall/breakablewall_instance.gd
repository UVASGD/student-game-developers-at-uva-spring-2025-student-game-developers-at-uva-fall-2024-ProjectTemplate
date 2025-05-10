extends Node3D
@onready var destr = %Destruction
@onready var blocker = %CollisionShape3D
func _on_area_3d_body_entered(body):
	# Checks if the body is the driver and if its invincible 
	if body.is_in_group("driver") and body.invul_on:
		blocker.disabled = true
		await destr.destroy()
		self.queue_free()
	pass # Replace with function body.

func _on_timer_timeout() -> void:
	self.queue_free()
