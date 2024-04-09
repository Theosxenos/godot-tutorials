extends CharacterBody2D

@export var knockback_friction = 200
@export var knockback_force = 110

@onready var stats = $Stats as Stats

func _ready():
	print(stats.health)
	print(stats.max_health)

func _physics_process(delta):
	velocity = velocity.move_toward(Vector2.ZERO, knockback_friction * delta)
	move_and_slide()

func _on_hurtbox_area_entered(area: Area2D):
	#velocity = area.knockback_vector * knockback_force
	stats.health -= (area as Hitbox).damage
	var knockback_direction = (position - area.owner.position) as Vector2
	velocity = knockback_direction.normalized() * knockback_force

func _on_stats_no_health():
	queue_free()
