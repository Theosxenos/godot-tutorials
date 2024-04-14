extends Node

@export var cactus_scene : PackedScene
@export var coin_scene : PackedScene
@export var powerup_scene : PackedScene

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
	
	$HUD.update_score(score)
	$HUD.update_timer(time_left)

func _process(delta):
	if playing and get_tree().get_nodes_in_group("coins").size() == 0:
		level += 1
		time_left += 5
		spawn_coins()
		spawn_cactusses()
		$LevelSound.play()
		$PowerUpTimer.wait_time = randf_range(5, 10)
		$PowerUpTimer.start()

func new_game():
	playing = true
	level = 1
	score = 0
	time_left = play_time
	$Player.start()
	$Player.show()
	$GameTimer.start()
	
	spawn_cactusses()
	spawn_coins()

func spawn_coins():
	for i in level + 4:
		var c = coin_scene.instantiate()
		add_child(c)
		c.screen_size = screen_size
		c.position = Vector2(randi_range(0, screen_size.x), randi_range(0, screen_size.y))

func spawn_cactusses():
	get_tree().call_group("obstacles","queue_free")
	for i in level + 2:
		var c = cactus_scene.instantiate()
		add_child(c)
		while true:
			c.position = Vector2(randi_range(0, screen_size.x), randi_range(0, screen_size.y))
			if not c.overlaps_area($Player):
				break

func _on_game_timer_timeout():
	time_left -= 1
	$HUD.update_timer(time_left)
	if (time_left <= 0):
		$EndSound.play()
		game_over()

func game_over():
	playing = false
	$GameTimer.stop()
	get_tree().call_group("coins","queue_free")
	get_tree().call_group("obstacles","queue_free")
	get_tree().call_group("powerups","queue_free")
	$HUD.show_game_over()
	$Player.die()

func _on_player_hurt():
	game_over()

func _on_player_pickup(type):
	match type:
		"coin":
			score += 1
			$HUD.update_score(score)
			$CoinSound.play()
		"powerup":
			time_left += 5
			$HUD.update_timer(time_left)
			$PowerUpSound.play()

func _on_hud_start_game():
	new_game()

func _on_power_up_timer_timeout():
	var p = powerup_scene.instantiate()
	add_child(p)
	p.screen_size = screen_size
	p.position = Vector2(randi_range(0, screen_size.x),randi_range(0, screen_size.y))