extends CollisionObject2D

# Circle properties
var center = Vector2(0, 0) # Position of the circle's center
var radius = 50 # Radius of the circle
@export var color = Color(1, 0, 0, 1) # Red color

func _ready():
	queue_redraw() # Request drawing to ensure the circle appears

func _draw():
	# Draw a filled circle with the specified center, radius, and color
	draw_circle(center, radius, color)

