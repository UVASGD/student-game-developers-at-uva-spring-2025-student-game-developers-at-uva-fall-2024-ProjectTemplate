extends Node

# Driver signals
signal driver_item_taken
signal driver_giving_item
signal update_engine_force(modifier:String, amount:float)
signal clear_driver_item
signal driver_play_animation(anim_name: String)
signal driver_play_animation_backwards(anim_name: String)
signal out_of_bounds
signal driver_change_invul

# Track signals
signal update_checkpoint(name:String)
signal revert_checkpoints
signal update_laps_left(laps_left:String)
signal end_track
signal map_has_controllable(name:String)

# Devil Signals
signal devil_item_taken
signal devil_giving_item
signal clear_devil_item
signal devil_play_animation
signal toggle_crosshair

# Item Signals
# Bagger
signal activate_controllable(c_name: String, num_uses: int)
signal devil_controllable_status()

# Menu Signals
signal back_to_menu

#Split screen signals
signal change_screen_visibility(name:String)
