extends track_settings
class_name LevelTemplate

@onready var checkpoints_node = %checkpoints

func _ready():
	super._ready()
	initialize_num_checkpoints(checkpoints_node)
