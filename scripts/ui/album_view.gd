extends Control


func _ready() -> void:
	%BackButton.pressed.connect(_back_to_sea)
	_refresh_summary()


func _refresh_summary() -> void:
	%SummaryLabel.text = "\n".join([
		"사진 앨범: %d장" % GameState.photos.size(),
		"풍경 앨범: %d개" % GameState.sceneries.size(),
		"편지 보관함: %d개" % GameState.letters.size(),
		"동반자 호감도: Lv %d" % GameState.companion_affection,
		"",
		"최근 사진: %s" % _last_or_empty(GameState.photos),
		"최근 풍경: %s" % _last_or_empty(GameState.sceneries),
		"최근 편지: %s" % _last_or_empty(GameState.letters)
	])


func _last_or_empty(items: Array[String]) -> String:
	if items.is_empty():
		return "아직 없음"
	return items.back()


func _back_to_sea() -> void:
	get_tree().change_scene_to_file("res://scenes/game.tscn")

