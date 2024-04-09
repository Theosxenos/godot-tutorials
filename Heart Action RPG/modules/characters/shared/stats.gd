class_name Stats extends Node

signal no_health

@export var max_health = 1

@onready var health: int = max_health : set = set_health

func set_health(value):
	health = value
	if health <= 0:
		no_health.emit()
