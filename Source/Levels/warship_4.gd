extends Node3D

@onready var anim_player:AnimationPlayer = %w4_anim_player

func _ready():
	anim_player.play("moving_warship")
	



func _on_w_4_anim_player_animation_finished(anim_name):
	anim_player.play("moving_warship")
