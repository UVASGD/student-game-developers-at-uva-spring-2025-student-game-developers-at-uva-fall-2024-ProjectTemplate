extends Node3D

@onready var anim_player:AnimationPlayer = %w3_anim_player

func _ready():
	anim_player.play("water_movement")
