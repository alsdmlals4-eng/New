extends Control


func _ready() -> void:
	_connect_mood_button(%CalmButton, "평온")
	_connect_mood_button(%TiredButton, "지침")
	_connect_mood_button(%LonelyButton, "외로움")
	_connect_mood_button(%ExcitedButton, "설렘")


func _connect_mood_button(button: Button, mood: String) -> void:
	button.pressed.connect(func() -> void:
		_start_voyage(mood)
	)


## Starts a new voyage with the selected mood.
func _start_voyage(mood: String) -> void:
	GameState.select_mood(mood)
	GameState.reset_session()
	get_tree().change_scene_to_file("res://scenes/game.tscn")
