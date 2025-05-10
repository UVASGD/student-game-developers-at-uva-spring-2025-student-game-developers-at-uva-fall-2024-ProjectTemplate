extends Node3D
class_name GameContainer

@onready var main_menu_scene = preload("res://Source/Menus/main_menu.tscn")
@onready var map_select_menu_scene = preload("res://Source/Menus/map_select_menu.tscn")

@onready var levels = {
	"V_Violence" : preload("res://Source/Levels/Versus/v_violence.tscn"),
	"V_Greed" : preload("res://Source/Levels/Versus/v_greed.tscn"),
	"V_Limbo" : 3,
	
	"T_Violence": preload("res://Source/Levels/Time Trials/t_violence.tscn"),
	"T_Greed":5,
	"T_Limbo":6,
}

func _ready():
	spawn_main_menu()
	
func spawn_main_menu():
	$AudioStreamPlayer.play()
	var main_menu_inst = main_menu_scene.instantiate()
	add_child(main_menu_inst)
	
func spawn_map_select_menu(is_versus:bool):
	var map_select_menu_inst = map_select_menu_scene.instantiate()
	map_select_menu_inst.is_versus = is_versus
	add_child(map_select_menu_inst)

func spawn_level(name : String):
	$AudioStreamPlayer.stop()
	var level_inst = levels.get(name).instantiate()
	#level_inst.global_position = Vector3(0,0,0)
	add_child(level_inst)
