extends VehicleBody3D

@export var power_curve : Curve
@export var MAX_STEER: float = 0.9
@export var steering_curve: Curve

var STEER_SPEED = 5.0  # Increased for quicker steering
var STEER_LIMIT = 0.3  # More responsive turning
var ENGINE_POWER = 400

@export var default_engine_power: float = 300
@export var boost_multiplier: float = 2

@onready var animation_player = %AnimationPlayer
var is_boosting: bool = false

var acceleration_time: float = 0
var steering_time: float = 0

var invul_on = false

func _ready():
	ENGINE_POWER = default_engine_power
	SignalBus.connect("update_engine_force", update_engine_power)
	SignalBus.connect("driver_change_invul", invul_change)

var previous_speed := linear_velocity.length()

func _physics_process(delta: float):
	# Improved Steering Responsiveness
	var steer_input = Input.get_axis("turn_right", "turn_left")
	steering = move_toward(steering, steer_input * STEER_LIMIT, STEER_SPEED * delta)

	# Improved Acceleration & Braking Response
	var throttle_input = Input.get_axis("move_back", "move_forward")
	engine_force = throttle_input * ENGINE_POWER

	# Flip Car Mechanic
	if Input.is_action_just_pressed("flip_car"):
		flip_car()

	previous_speed = linear_velocity.length()

# Function to flip the car if it's upside down or on its side
func flip_car():
	if global_transform.basis.y.dot(Vector3.UP) < 0.3:  # Checks if car is flipped
		var upright_transform = global_transform
		upright_transform.basis = Basis()  # Reset rotation
		upright_transform.origin += Vector3(0, 2, 0)  # Slightly lift the car
		global_transform = upright_transform
		linear_velocity = Vector3.ZERO  # Reset movement
		angular_velocity = Vector3.ZERO  # Reset spin


func update_engine_power(modifier:String, amount:float):
	match modifier:
		"add":
			ENGINE_POWER += amount
		"multiply":
			ENGINE_POWER *= amount
		"restore":
			ENGINE_POWER = default_engine_power

func invul_change():
	invul_on = !invul_on
