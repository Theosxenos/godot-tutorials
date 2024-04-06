class_name Rock extends RigidBody2D

var screensize = Vector2.ZERO
var size : int
var radius
var scale_factor = 0.2

func start(_position : Vector2, _velocity : Vector2, _size : int):
	var sprite : Sprite2D = $Sprite2D as Sprite2D
	
	position = _position
	size = _size
	mass = 1.5 * size
	sprite.scale = Vector2.ONE * scale_factor * size
	radius = int(sprite.texture.get_size().x / 2 * sprite.scale.x)
	var shape = CircleShape2D.new()
	shape.radius = radius
	$CollisionShape2D.shape = shape
	linear_velocity = _velocity
	angular_velocity = randf_range(-PI, PI)

func _integrate_forces(physics_state):
	#var xform = physics_state.transform
	#xform.origin.x = wrapf(xform.origin.x, 0, screensize.x)
	#xform.origin.y = wrapf(xform.origin.y, 0, screensize.y)
	#physics_state.transform = xform
	
	var xform = physics_state.transform
	xform.origin.x = wrapf(xform.origin.x, 0 - radius, screensize.x + radius)
	xform.origin.y = wrapf(xform.origin.y, 0 - radius,	screensize.y + radius)
	physics_state.transform = xform