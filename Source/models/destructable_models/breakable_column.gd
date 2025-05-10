extends Node3D

var is_breaking:bool = false
@onready var anim_player = $AnimationPlayer

func _ready():
	anim_player.play("RESET")
	is_breaking = false



func _on_body_entered(body):
	if body.is_in_group("explosion") or body.is_in_group("") and !is_breaking:
		is_breaking = true
		anim_player.play("breaking")
		
func break_object():
	anim_player.play("breaking")
