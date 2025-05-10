extends CenterContainer
@export var crosshair_radius: float = 4.0
@export var crosshair_color: Color = Color.WHITE

var can_draw:bool = true

# Called when the node enters the scene tree for the first time.
func _ready():
	SignalBus.connect("toggle_crosshair", update_can_draw)
	queue_redraw()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	pass

func _draw():
	if can_draw:
		draw_circle(Vector2(20,20),crosshair_radius, crosshair_color)

func update_can_draw():
	can_draw = !can_draw
	queue_redraw()
