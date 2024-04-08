class_name Bullet extends Area2D

@export var speed: int = 1000

var velocity: Vector2 = Vector2.ZERO

func start(_transform : Transform2D):
	transform = _transform
	velocity = transform.x * speed

func _process(delta):
	position += velocity * delta

func _on_visible_on_screen_notifier_2d_screen_exited():
	queue_free()

func _on_body_entered(body):
	if body.is_in_group("rocks"):
		body.explode()
		queue_free()

func _on_area_entered(area):
	if area.is_in_group("enemies"):
		area.take_damage(1)
