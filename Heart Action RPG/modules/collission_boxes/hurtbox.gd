class_name Hurtbox extends Area2D

signal invincibility_started
signal invincibility_stopped

@onready var hit_audio := $AudioStreamPlayer as AudioStreamPlayer

@export var invincible: bool = false : set = set_invincibility

const HIT_EFFECT = preload("res://modules/effects/hit/hit_effect.tscn")

func create_hit_effect(pos: Vector2 = Vector2.ZERO):
	var effect: Effect = HIT_EFFECT.instantiate() as Effect
	add_child(effect)
	effect.position = pos
	hit_audio.play()

func set_invincibility(value):
	set_deferred("monitoring", !value)

func start_invincibility(duration: float):
	invincible = true
	$InvincibilityTimer.start(duration)
	invincibility_started.emit()

func _on_invincibility_timer_timeout():
	invincible = false
	invincibility_stopped.emit()
