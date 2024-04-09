extends CharacterBody2D

@export var knockback_friction = 200
@export var knockback_force = 110

@onready var stats = $Stats as Stats

const DEATH_SCENE: PackedScene = preload("res://modules/effects/enemy/enemy_death_effect.tscn")

func _physics_process(delta):
	velocity = velocity.move_toward(Vector2.ZERO, knockback_friction * delta)
	move_and_slide()

func _on_hurtbox_area_entered(area: Area2D):
	#velocity = area.knockback_vector * knockback_force
	stats.health -= (area as Hitbox).damage
	var knockback_direction = (position - area.owner.position) as Vector2
	velocity = knockback_direction.normalized() * knockback_force
	create_hit_effect()

func create_hit_effect():
	if stats.health > 0:
		($Hurtbox as Hurtbox).create_hit_effect()

func _on_stats_no_health():
	await create_effect()	
	queue_free()

func create_effect():
	$AnimatedSprite.hide()
	var death_instance = DEATH_SCENE.instantiate() as Effect
	add_child(death_instance)
	await death_instance.animation_finished
