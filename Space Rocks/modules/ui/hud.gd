class_name Hud extends CanvasLayer

signal start_game

@onready var lives_counter : Array = $MarginContainer/HBoxContainer/LivesCounter.get_children()
@onready var score_label : Label = $MarginContainer/HBoxContainer/ScoreLabel
@onready var message : Label = $VBoxContainer/Message
@onready var start_button : TextureButton = $VBoxContainer/StartButton

@onready var shield_bar = $MarginContainer/HBoxContainer/ShieldBar

var bar_textures : Dictionary = {
	"green" : preload("res://modules/ui/shield_bar/bar_green_200.png"),
	"yellow": preload("res://modules/ui/shield_bar/bar_yellow_200.png"),
	"red": preload("res://modules/ui/shield_bar/bar_red_200.png")
}

func show_message(text : String):
	message.text = text
	message.show()
	$Timer.start()

func update_score(value : int):
	score_label.text = str(value)

func update_lives(value : int):
	for item : int in 3:
		lives_counter[item].visible = value > item

func update_shield(value: float) -> void:
	shield_bar.texture_progress = bar_textures["green"]
	if value < 0.4:
		shield_bar.texture_progress = bar_textures["red"]
	elif value < 0.7:
		shield_bar.texture_progress = bar_textures["yellow"]
	shield_bar.value = value	

func game_over():
	show_message("Game Over")
	await $Timer.timeout
	start_button.show()

func _on_start_button_pressed():
	start_button.hide()
	start_game.emit()

func _on_timer_timeout():
	message.hide()
	message.text = ""
