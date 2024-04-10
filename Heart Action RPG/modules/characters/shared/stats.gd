class_name Stats extends Node

signal no_health
signal health_changed
signal max_health_changed

@export var max_health = 1 : set = set_max_health

@onready var health: int = max_health : set = set_health

func set_health(value):
	if value > max_health:
		return

	health = value
	health_changed.emit(value)
	if health <= 0:
		no_health.emit()

func set_max_health(value):
	max_health = value
	max_health_changed.emit(value)
