extends devil_items
@export var speed: int = 90
@export var rotation_speed: int = 3
@export var explosion_force_amount: int = 5000
var is_being_used: bool = false
var missile_instance = preload("res://Source/items/devil_items/missile/missile_instance.tscn")
@onready var devil  # The devil's head or where you want the missile to spawn

func _ready():
	num_uses = 1
	is_being_used = false
	devil = get_tree().get_nodes_in_group("devil_head")[0]  # Find the devil's head in the group

func use():
	if !is_being_used:
		is_being_used = true
		num_uses -= 1
		
		# Get the devil's position (using global_position in Godot 4)
		var spawn_position = devil.global_position
		
		# Instantiate the missile
		var m_instance = missile_instance.instantiate()
		m_instance.speed = speed
		m_instance.rotation_speed = rotation_speed
		m_instance.explosion_force_amount = explosion_force_amount
		
		# Set the spawn position for the missile
		m_instance.global_position = spawn_position
		
		# Set the missile's rotation to match the devil's rotation
		m_instance.global_transform = devil.global_transform
		
		# Get the forward direction based on the devil's transform
		# In Godot 4, you can use -global_transform.basis.z for forward direction (negative Z is forward)
		var forward_direction = -devil.global_transform.basis.z.normalized()
		
		# Set the missile's velocity
		m_instance.velocity = forward_direction * speed
		
		# Add the missile instance to the scene
		var root_scene = get_tree().get_current_scene()
		root_scene.add_child(m_instance)
		
		SignalBus.emit_signal("clear_devil_item")
		self.queue_free()
