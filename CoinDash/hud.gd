extends CanvasLayer

signal start_game

func update_score(score):
	$MarginContainer/ScoreLabel.text = str(score)

func update_timer(time):
	$MarginContainer/TimeLabel.text = str(time)

func show_message(text):
	$Message.text = text
	$Message.show()
	$MessageTimer.start()

func show_game_over():
	$Retry.show()	
	show_message("Game Over!")
	
	await $MessageTimer.timeout
	
	$StartButton.show()
	
	$Message.text = "Coin Dash!"
	$Message.show()

func _on_start_button_pressed():
	$StartButton.hide()
	$Message.hide()
	$Retry.hide()
	start_game.emit()


func _on_message_timer_timeout():
	$Message.hide()
