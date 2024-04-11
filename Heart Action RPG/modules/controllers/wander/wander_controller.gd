class_name WanderController
extends Node2D

@export var wander_range := 32

@onready var start_position := global_position
@onready var target_position := global_position


func get_time_left() -> float:
	return $Timer.time_left


func start_wander_timer(duration: float) -> void:
	$Timer.start(duration)


func update_target_position() -> void:
	var target_vecor = Vector2(randi_range(-wander_range, wander_range), randi_range(-wander_range, wander_range))
	target_position = start_position + target_vecor


func _on_timer_timeout():
	update_target_position()
