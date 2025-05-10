extends GridContainer

@onready var left_side = %left_side
@onready var right_side = %right_side
@onready var end_lap = %end_lap
@onready var end_lap_sub_viewport = %end_lap_sub_viewport
@onready var anim_player = %AnimationPlayer


func _ready():
	SignalBus.connect("end_track", end_game)
	left_side.handle_input_locally = false
	right_side.handle_input_locally = true

	%SubViewportContainer.visible = true
	%SubViewportContainer2.visible = false
	end_lap.visible = false
	#end_lap.stretch = true

func end_game():
	Engine.time_scale = 0.05
	%SubViewportContainer.visible = false
	%SubViewportContainer2.visible = false
	end_lap.visible = true
	anim_player.play("end_camera_ang_1")
	await anim_player.animation_finished
	anim_player.play("end_camera_ang_2")
	await anim_player.animation_finished
	anim_player.play("end_camera_ang_3")
	
	SignalBus.emit_signal("back_to_menu")
	
	#TODO: Put in function for transitioning to the menu
