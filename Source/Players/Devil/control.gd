extends Control
class_name DevilUI

func _ready():
	SignalBus.connect("devil_giving_item", update_item_label)
	SignalBus.connect("clear_devil_item", clear)
	
func update_item_label(item_name:String):
	$current_item.text = item_name

func clear():
	$current_item.text = ""
