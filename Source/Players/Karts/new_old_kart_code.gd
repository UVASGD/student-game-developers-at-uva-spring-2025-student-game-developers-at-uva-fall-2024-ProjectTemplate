extends VehicleBody3D

var STEER_SPEED = 5
var STEER_LIMIT = 0.4
var ENGINE_POWER = 400


@export var default_engine_power : float = 400
@export var boost_multiplier : float = 2

@onready var animation_player = %AnimationPlayer
var is_boosting : bool = false

var acceleration_time : float = 0
var steering_time : float = 0

func _ready():
	ENGINE_POWER = default_engine_power
	
var previous_speed := linear_velocity.length()

func _physics_process(delta: float):
	
	#if self.global_transform.origin.y < -180:
		#SignalBus.emit_signal("out_of_bounds")
	
	var fwd_mps := (linear_velocity * transform.basis).x

	steering = move_toward(steering, Input.get_axis("turn_right", "turn_left") * STEER_LIMIT, STEER_SPEED * delta)
	engine_force = Input.get_axis("move_back", "move_forward") * ENGINE_POWER

	previous_speed = linear_velocity.length()

	
	#if !is_boosting:
		#_boost_test()
	#else:
		#ENGINE_POWER -= 10
		#ENGINE_POWER = max(ENGINE_POWER, default_engine_power)
		#if ENGINE_POWER == default_engine_power:
			#is_boosting = false

func update_engine_power(modifier:String, amount:float):
	match modifier:
		"add":
			ENGINE_POWER += amount
		"multiply":
			ENGINE_POWER *= amount
		"restore":
			ENGINE_POWER = default_engine_power

#func _boost_test():
	#if Input.is_action_just_pressed("use_driver_item") and !is_boosting:
		#animation_player.play("flame")
		#ENGINE_POWER *= boost_multiplier
		#is_boosting = true
		
