extends Node3D


#func _on_area_3d_body_entered(body):
	#pass
	#if body.is_in_group("driver"):
		#SignalBus.emit_signal("out_of_bounds")
		#print("out of bounds!!!")


func _on_area_3d_body_entered(body):
	if body.is_in_group("driver"):
		SignalBus.emit_signal("out_of_bounds")
		print("out of bounds!!!")
