extends Node

@export var coin_scene : PackedScene
@export var play_time = 30

var level = 1
var score = 0
var time_left = 0
var screen_size = Vector2.ZERO
var playing = false

func _ready():
	screen_size = get_viewport().get_visible_rect().size
	$Player.screen_size = screen_size
	$Player.hide()



func new_game():
	playing = true
	level = 1
	score = 0
	time_left = play_time
	$Player.start()
	$Player.show()
	$GameTimer.start()
	spawn_coins()

func spawn_coins():
	for i in level + 4:
		var c = coin_scene.instantiate()
		add_child(c)
		c.screen_size = screen_size
		c.position = Vector2(randi_range(0, screen_size.x), randi_range(0, screen_size.y))
		
